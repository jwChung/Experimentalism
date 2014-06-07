using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using Xunit;

namespace Jwc.Experiment
{
    public class AssemblyFixtureConfigurationAttributeTest
    {
        [Fact]
        public void SutIsAttribute()
        {
            var sut = new TssAssemblyFixtureConfigurationAttribute();
            Assert.IsAssignableFrom<Attribute>(sut);
        }

        [StaticFact]
        public void ConfigureSetsUpFixtureOnlyOnceWhenCalledManyTimes()
        {
            var attribute1 = new TssAssemblyFixtureConfigurationAttribute();
            var attribute2 = new TssAssemblyFixtureConfigurationAttribute();
            var assembly = new DelegatingAssembly
            {
                OnGetCustomAttributes = (t, i) =>
                {
                    Assert.Equal(typeof(AssemblyFixtureConfigurationAttribute), t);
                    Assert.False(i);
                    return new object[] { attribute1, attribute2 };
                }
            };

            AssemblyFixtureConfigurationAttribute.Configure(assembly);
            AssemblyFixtureConfigurationAttribute.Configure(assembly);

            Assert.Equal(assembly, attribute1.SetUpAssemblies.Single());
            Assert.Equal(assembly, attribute2.SetUpAssemblies.Single());
        }

        [StaticFact]
        public void ConfigureRegistersCorrectTearDownHandlerToDomainUnloadEvent()
        {
            var attribute = new TssAssemblyFixtureConfigurationAttribute();
            var assembly = new DelegatingAssembly
            {
                OnGetCustomAttributes = (t, i) => new object[] { attribute }
            };

            AssemblyFixtureConfigurationAttribute.Configure(assembly);

            attribute.RaiseDomainUnload();
            Assert.Equal(assembly, attribute.TearDownAssemblies.Single());
        }

        [StaticFact]
        public void ConfigureSetsUpFixtureOnlyOnceWhenAccessedByMultipleThreads()
        {
            // Fixture setup
            var attribute = new TssAssemblyFixtureConfigurationAttribute();
            var assembly = new DelegatingAssembly
            {
                OnGetCustomAttributes = (t, i) => new object[] { attribute }
            };

            var threads = new Thread[30];
            for (int i = 0; i < threads.Length; i++)
                threads[i] = new Thread(() => AssemblyFixtureConfigurationAttribute.Configure(assembly));

            // Exercise system
            foreach (var thread in threads)
                thread.Start();
            foreach (var thread in threads)
                thread.Join();

            // Verify outcome
            Assert.Equal(assembly, attribute.SetUpAssemblies.Single());
        }

        [Fact]
        public void ConfigureWithNullAssemblyThrows()
        {
            Assert.Throws<ArgumentNullException>(() => AssemblyFixtureConfigurationAttribute.Configure(null));
        }

        private class TssAssemblyFixtureConfigurationAttribute : AssemblyFixtureConfigurationAttribute
        {
            private readonly List<Assembly> _setUpAssemblies = new List<Assembly>();
            private readonly List<Assembly> _tearDownAssemblies = new List<Assembly>();

            public List<Assembly> SetUpAssemblies
            {
                get
                {
                    return _setUpAssemblies;
                }
            }

            public List<Assembly> TearDownAssemblies
            {
                get
                {
                    return _tearDownAssemblies;
                }
            }

            protected override void Setup(Assembly testAssembly)
            {
                SetUpAssemblies.Add(testAssembly);
            }

            protected override void Teardown(Assembly testAssembly)
            {
                TearDownAssemblies.Add(testAssembly);
            }

            public void RaiseDomainUnload()
            {
                if (DomainUnload != null)
                    DomainUnload(this, new EventArgs());
            }

            protected override event EventHandler DomainUnload;
        }

        private class DelegatingAssembly : Assembly
        {
            public Func<Type, bool, object[]> OnGetCustomAttributes { get; set; }

            public override object[] GetCustomAttributes(Type attributeType, bool inherit)
            {
                return OnGetCustomAttributes(attributeType, inherit);
            }
        }
    }
}
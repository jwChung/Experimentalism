using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using Xunit;

namespace Jwc.Experiment
{
    public class TestAssemblyConfigurationAttributeTest
    {
        [Fact]
        public void SutIsAttribute()
        {
            var sut = new TssTestAssemblyConfigurationAttribute();
            Assert.IsAssignableFrom<Attribute>(sut);
        }

        [NewAppDomainFact]
        public void ConfigureSetsUpFixtureOnlyOnceWhenCalledManyTimes()
        {
            var attribute1 = new TssTestAssemblyConfigurationAttribute();
            var attribute2 = new TssTestAssemblyConfigurationAttribute();
            var assembly = new DelegatingAssembly
            {
                OnGetCustomAttributesWithType = (t, i) =>
                {
                    Assert.Equal(typeof(TestAssemblyConfigurationAttribute), t);
                    Assert.False(i);
                    return new object[] { attribute1, attribute2 };
                }
            };

            TestAssemblyConfigurationAttribute.Configure(assembly);
            TestAssemblyConfigurationAttribute.Configure(assembly);

            Assert.Equal(assembly, attribute1.SetUpAssemblies.Single());
            Assert.Equal(assembly, attribute2.SetUpAssemblies.Single());
        }

        [NewAppDomainFact]
        public void ConfigureRegistersCorrectTearDownHandlerToDomainUnloadEvent()
        {
            var attribute = new TssTestAssemblyConfigurationAttribute();
            var assembly = new DelegatingAssembly
            {
                OnGetCustomAttributesWithType = (t, i) => new object[] { attribute }
            };

            TestAssemblyConfigurationAttribute.Configure(assembly);

            attribute.RaiseDomainUnload();
            Assert.Equal(assembly, attribute.TearDownAssemblies.Single());
        }

        [NewAppDomainFact]
        public void ConfigureSetsUpFixtureOnlyOnceWhenAccessedByMultipleThreads()
        {
            // Fixture setup
            var attribute = new TssTestAssemblyConfigurationAttribute();
            var assembly = new DelegatingAssembly
            {
                OnGetCustomAttributesWithType = (t, i) => new object[] { attribute }
            };

            var threads = new Thread[30];
            for (int i = 0; i < threads.Length; i++)
                threads[i] = new Thread(() => TestAssemblyConfigurationAttribute.Configure(assembly));

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
            Assert.Throws<ArgumentNullException>(() => TestAssemblyConfigurationAttribute.Configure(null));
        }

        private class TssTestAssemblyConfigurationAttribute : TestAssemblyConfigurationAttribute
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
    }
}
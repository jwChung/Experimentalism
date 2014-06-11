using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using Xunit;

namespace Jwc.Experiment
{
    public class AssemblyCustomizationAttributeTest
    {
        [Fact]
        public void SutIsAttribute()
        {
            var sut = new TssAssemblyCustomizationAttribute();
            Assert.IsAssignableFrom<Attribute>(sut);
        }

        [NewAppDomainFact]
        public void CustomizeSetsUpFixtureOnlyOnceWhenCalledManyTimes()
        {
            var attribute1 = new TssAssemblyCustomizationAttribute();
            var attribute2 = new TssAssemblyCustomizationAttribute();
            var assembly = new DelegatingAssembly
            {
                OnGetCustomAttributes = (t, i) =>
                {
                    Assert.Equal(typeof(AssemblyCustomizationAttribute), t);
                    Assert.False(i);
                    return new object[] { attribute1, attribute2 };
                }
            };

            AssemblyCustomizationAttribute.Customize(assembly);
            AssemblyCustomizationAttribute.Customize(assembly);

            Assert.Equal(assembly, attribute1.SetUpAssemblies.Single());
            Assert.Equal(assembly, attribute2.SetUpAssemblies.Single());
        }

        [NewAppDomainFact]
        public void CustomizeRegistersCorrectTearDownHandlerToDomainUnloadEvent()
        {
            var attribute = new TssAssemblyCustomizationAttribute();
            var assembly = new DelegatingAssembly
            {
                OnGetCustomAttributes = (t, i) => new object[] { attribute }
            };

            AssemblyCustomizationAttribute.Customize(assembly);

            attribute.RaiseDomainUnload();
            Assert.Equal(assembly, attribute.TearDownAssemblies.Single());
        }

        [NewAppDomainFact]
        public void CustomizeSetsUpFixtureOnlyOnceWhenAccessedByMultipleThreads()
        {
            // Fixture setup
            var attribute = new TssAssemblyCustomizationAttribute();
            var assembly = new DelegatingAssembly
            {
                OnGetCustomAttributes = (t, i) => new object[] { attribute }
            };

            var threads = new Thread[30];
            for (int i = 0; i < threads.Length; i++)
                threads[i] = new Thread(() => AssemblyCustomizationAttribute.Customize(assembly));

            // Exercise system
            foreach (var thread in threads)
                thread.Start();
            foreach (var thread in threads)
                thread.Join();

            // Verify outcome
            Assert.Equal(assembly, attribute.SetUpAssemblies.Single());
        }

        [Fact]
        public void CustomizeWithNullAssemblyThrows()
        {
            Assert.Throws<ArgumentNullException>(() => AssemblyCustomizationAttribute.Customize(null));
        }

        private class TssAssemblyCustomizationAttribute : AssemblyCustomizationAttribute
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
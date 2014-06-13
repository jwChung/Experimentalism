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

        [Fact]
        public void ConfigureSetsUpFixtureOnlyOnceWhenCalledManyTimes()
        {
            try
            {
                
            }
            finally
            {
                ResetConfigured();
            }
        }

        [Fact]
        public void ConfigureRegistersCorrectTearDownHandlerToDomainUnloadEvent()
        {
            try
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
            finally
            {
                ResetConfigured();
            }
        }

        [Fact]
        public void ConfigureSetsUpFixtureOnlyOnceWhenAccessedByMultipleThreads()
        {
            try
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
            finally
            {
                // Fixture teardown
                ResetConfigured();
            }
        }

        [Fact]
        public void ConfigureWithNullAssemblyThrows()
        {
            Assert.Throws<ArgumentNullException>(() => TestAssemblyConfigurationAttribute.Configure(null));
        }

        public void ResetConfigured()
        {
            typeof(TestAssemblyConfigurationAttribute)
                .GetField("_configured", BindingFlags.NonPublic | BindingFlags.Static)
                .SetValue(null, false);
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
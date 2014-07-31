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
                // Fixture setup
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

                // Exercise system
                TestAssemblyConfigurationAttribute.Configure(assembly);
                TestAssemblyConfigurationAttribute.Configure(assembly);

                // Verify outcome
                Assert.Equal(assembly, attribute1.SetUpAssemblies.Single());
                Assert.Equal(assembly, attribute2.SetUpAssemblies.Single());
            }
            finally
            {
                // Fixture teardown
                this.ResetConfigured();
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
                this.ResetConfigured();
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
                this.ResetConfigured();
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
                .GetField("configured", BindingFlags.NonPublic | BindingFlags.Static)
                .SetValue(null, false);
        }

        private class TssTestAssemblyConfigurationAttribute : TestAssemblyConfigurationAttribute
        {
            private readonly List<Assembly> setUpAssemblies = new List<Assembly>();
            private readonly List<Assembly> tearDownAssemblies = new List<Assembly>();
            
            protected override event EventHandler DomainUnload;

            public List<Assembly> SetUpAssemblies
            {
                get
                {
                    return this.setUpAssemblies;
                }
            }

            public List<Assembly> TearDownAssemblies
            {
                get
                {
                    return this.tearDownAssemblies;
                }
            }

            public void RaiseDomainUnload()
            {
                if (this.DomainUnload != null)
                    this.DomainUnload(this, new EventArgs());
            }

            protected override void Setup(Assembly testAssembly)
            {
                this.SetUpAssemblies.Add(testAssembly);
            }

            protected override void Teardown(Assembly testAssembly)
            {
                this.TearDownAssemblies.Add(testAssembly);
            }
        }
    }
}
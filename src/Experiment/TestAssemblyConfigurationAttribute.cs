namespace Jwc.Experiment
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Attribute to set up or tear down all fixtures in a test assembly.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public abstract class TestAssemblyConfigurationAttribute : Attribute
    {
        private static readonly object SyncRoot = new object();
        private static bool configured;

        /// <summary>
        /// Occurs when [domain unload].
        /// </summary>
        protected virtual event EventHandler DomainUnload
        {
            add
            {
                AppDomain.CurrentDomain.DomainUnload += value;
            }

            remove
            {
                AppDomain.CurrentDomain.DomainUnload -= value;
            }
        }

        /// <summary>
        /// Sets up or tears down all fixtures in a test assembly.
        /// </summary>
        /// <param name="testAssembly">
        /// The test assembly.
        /// </param>
        public static void Configure(Assembly testAssembly)
        {
            if (testAssembly == null)
                throw new ArgumentNullException("testAssembly");

            if (TestAssemblyConfigurationAttribute.configured)
                return;

            lock (TestAssemblyConfigurationAttribute.SyncRoot)
                TestAssemblyConfigurationAttribute.ConfigureImpl(testAssembly);
        }

        /// <summary>
        /// Sets up all fixtures in a test assembly.
        /// </summary>
        /// <param name="testAssembly">
        /// The test assembly.
        /// </param>
        protected virtual void Setup(Assembly testAssembly)
        {
        }

        /// <summary>
        /// Tears down all fixtures in a test assembly.
        /// </summary>
        /// <param name="testAssembly">
        /// The test assembly.
        /// </param>
        protected virtual void Teardown(Assembly testAssembly)
        {
        }

        private static void ConfigureImpl(Assembly testAssembly)
        {
            if (TestAssemblyConfigurationAttribute.configured)
                return;

            TestAssemblyConfigurationAttribute.ConfigureAttributes(testAssembly);
            TestAssemblyConfigurationAttribute.configured = true;
        }

        private static void ConfigureAttributes(Assembly testAssembly)
        {
            foreach (var attribute in TestAssemblyConfigurationAttribute.GetAttributes(testAssembly))
                attribute.ConfigureAttribute(testAssembly);
        }

        private static IEnumerable<TestAssemblyConfigurationAttribute> GetAttributes(Assembly testAssembly)
        {
            return testAssembly.GetCustomAttributes(typeof(TestAssemblyConfigurationAttribute), false)
                .Cast<TestAssemblyConfigurationAttribute>();
        }

        private void ConfigureAttribute(Assembly testAssembly)
        {
            this.Setup(testAssembly);
            this.DomainUnload += (s, e) => this.Teardown(testAssembly);
        }
    }
}
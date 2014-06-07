using System;
using System.Reflection;

namespace Jwc.Experiment.Xunit
{
    /// <summary>
    ///     Attribute to set up or tear down all fixtures in a test assembly.
    /// </summary>
    public abstract class AssemblyFixtureConfigurationAttribute : Attribute
    {
        private static readonly object SyncLock = new object();
        private static bool _configured;

        /// <summary>
        ///     Sets up all fixtures in a test assembly.
        /// </summary>
        /// <param name="testAssembly">
        ///     The test assembly.
        /// </param>
        public virtual void SetUp(Assembly testAssembly)
        {
        }

        /// <summary>
        ///     Tears down all fixtures in a test assembly.
        /// </summary>
        /// <param name="testAssembly">
        ///     The test assembly.
        /// </param>
        public virtual void TearDown(Assembly testAssembly)
        {
        }

        internal static void Configure(Assembly testAssembly)
        {
            if (_configured)
                return;

            lock (SyncLock)
            {
                if (_configured)
                    return;

                ConfigureImpl(testAssembly);

                _configured = true;
            }
        }

        private static void ConfigureImpl(Assembly testAssembly)
        {
            var attributes = testAssembly.GetCustomAttributes(typeof(AssemblyFixtureConfigurationAttribute), false);

            foreach (AssemblyFixtureConfigurationAttribute attribute in attributes)
                Configure(testAssembly, attribute);
        }

        private static void Configure(Assembly testAssembly, AssemblyFixtureConfigurationAttribute attribute)
        {
            attribute.SetUp(testAssembly);
            AppDomain.CurrentDomain.DomainUnload += (s, e) => attribute.TearDown(testAssembly);
        }
    }
}
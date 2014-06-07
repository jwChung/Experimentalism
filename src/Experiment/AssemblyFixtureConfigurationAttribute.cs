using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Jwc.Experiment
{
    /// <summary>
    ///     Attribute to set up or tear down all fixtures in a test assembly.
    /// </summary>
    public class AssemblyFixtureConfigurationAttribute : Attribute
    {
        private static readonly object SyncLock = new object();
        private static bool _configured;

        /// <summary>
        ///     Sets up or tears down all fixtures in a test assembly.
        /// </summary>
        /// <param name="testAssembly">
        ///     The test assembly.
        /// </param>
        public static void Configure(Assembly testAssembly)
        {
            if (testAssembly == null)
                throw new ArgumentNullException("testAssembly");

            if (_configured)
                return;

            lock (SyncLock)
            {
                if (_configured)
                    return;

                ConfigureAttributes(testAssembly);
                _configured = true;
            }
        }

        /// <summary>
        ///     Sets up all fixtures in a test assembly.
        /// </summary>
        /// <param name="testAssembly">
        ///     The test assembly.
        /// </param>
        protected virtual void SetUp(Assembly testAssembly)
        {
        }

        /// <summary>
        ///     Tears down all fixtures in a test assembly.
        /// </summary>
        /// <param name="testAssembly">
        ///     The test assembly.
        /// </param>
        protected virtual void TearDown(Assembly testAssembly)
        {
        }

        private static void ConfigureAttributes(Assembly testAssembly)
        {
            foreach (var attribute in GetAttributes(testAssembly))
                attribute.ConfigureAttribute(testAssembly);
        }

        private static IEnumerable<AssemblyFixtureConfigurationAttribute> GetAttributes(Assembly testAssembly)
        {
            return testAssembly.GetCustomAttributes(typeof(AssemblyFixtureConfigurationAttribute), false)
                .Cast<AssemblyFixtureConfigurationAttribute>();
        }

        private void ConfigureAttribute(Assembly testAssembly)
        {
            SetUp(testAssembly);
            DomainUnload += (s, e) => TearDown(testAssembly);
        }

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
    }
}
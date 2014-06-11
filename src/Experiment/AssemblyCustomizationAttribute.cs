using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Jwc.Experiment
{
    /// <summary>
    ///     Attribute to set up or tear down all fixtures in a test assembly.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public abstract class AssemblyCustomizationAttribute : Attribute
    {
        private static readonly object SyncLock = new object();
        private static bool _customized;

        /// <summary>
        ///     Sets up or tears down all fixtures in a test assembly.
        /// </summary>
        /// <param name="testAssembly">
        ///     The test assembly.
        /// </param>
        public static void Customize(Assembly testAssembly)
        {
            if (testAssembly == null)
                throw new ArgumentNullException("testAssembly");

            if (_customized)
                return;

            lock (SyncLock)
            {
                if (_customized)
                    return;

                CustomizeAttributes(testAssembly);
                _customized = true;
            }
        }

        /// <summary>
        ///     Sets up all fixtures in a test assembly.
        /// </summary>
        /// <param name="testAssembly">
        ///     The test assembly.
        /// </param>
        protected virtual void Setup(Assembly testAssembly)
        {
        }

        /// <summary>
        ///     Tears down all fixtures in a test assembly.
        /// </summary>
        /// <param name="testAssembly">
        ///     The test assembly.
        /// </param>
        protected virtual void Teardown(Assembly testAssembly)
        {
        }

        private static void CustomizeAttributes(Assembly testAssembly)
        {
            foreach (var attribute in GetAttributes(testAssembly))
                attribute.CustomizeAttribute(testAssembly);
        }

        private static IEnumerable<AssemblyCustomizationAttribute> GetAttributes(Assembly testAssembly)
        {
            return testAssembly.GetCustomAttributes(typeof(AssemblyCustomizationAttribute), false)
                .Cast<AssemblyCustomizationAttribute>();
        }

        private void CustomizeAttribute(Assembly testAssembly)
        {
            Setup(testAssembly);
            DomainUnload += (s, e) => Teardown(testAssembly);
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
using System;
using System.Reflection;

namespace Jwc.Experiment.Xunit
{
    /// <summary>
    /// Attribute to supply a type of which instance will be used to set up or
    /// to tear down a test fixture only once on assembly level.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class AssemblyFixtureConfigAttribute : Attribute
    {
        private static readonly object _syncLock = new object();
        private static bool _initialized;

        private readonly Type _configClass;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="AssemblyFixtureConfigAttribute" /> class.
        /// </summary>
        /// <param name="configClass">
        /// A type to be used to set up or to tear down a test fixture only once
        /// on assembly level.
        /// </param>
        public AssemblyFixtureConfigAttribute(Type configClass)
        {
            if (configClass == null)
                throw new ArgumentNullException("configClass");

            _configClass = configClass;
        }

        /// <summary>
        /// Gets a type to be used to set up or to tear down a test fixture only
        /// once on assembly level.
        /// </summary>
        public Type ConfigClass
        {
            get
            {
                return _configClass;
            }
        }

        internal static void Initialize(Assembly testAssembly)
        {
            if (_initialized)
                return;

            lock (_syncLock)
            {
                if (_initialized)
                    return;

                InitializeImpl(testAssembly);
                _initialized = true;    
            }
        }

        private static void InitializeImpl(Assembly testAssembly)
        {
            var attribures = testAssembly.GetCustomAttributes(typeof(AssemblyFixtureConfigAttribute), false);
            foreach (AssemblyFixtureConfigAttribute attribure in attribures)
                InitializeImpl(attribure.ConfigClass);
        }

        private static void InitializeImpl(Type type)
        {
            var teardown = Activator.CreateInstance(type) as IDisposable;
            if (teardown != null)
                AppDomain.CurrentDomain.DomainUnload += (s, e) => teardown.Dispose();
        }
    }
}
using System;
using System.Reflection;

namespace Jwc.Experiment.Xunit
{
    /// <summary>
    /// Attribute to supply a type of which instance will be used to set up or
    /// to tear down a test fixture only once on assembly level.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class AssemblyInitializeAttribute : Attribute
    {
        private static readonly object _syncLock = new object();
        private static bool _initialized;

        private readonly Type _initializer;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="AssemblyInitializeAttribute" /> class.
        /// </summary>
        /// <param name="initializer">
        /// A type to be used to set up or to tear down a test fixture only once
        /// on assembly level.
        /// </param>
        public AssemblyInitializeAttribute(Type initializer)
        {
            if (initializer == null)
                throw new ArgumentNullException("initializer");

            _initializer = initializer;
        }

        /// <summary>
        /// Gets a type to be used to set up or to tear down a test fixture only
        /// once on assembly level.
        /// </summary>
        public Type Initializer
        {
            get
            {
                return _initializer;
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
            var attribures = testAssembly.GetCustomAttributes(typeof(AssemblyInitializeAttribute), false);
            foreach (AssemblyInitializeAttribute attribure in attribures)
                InitializeImpl(attribure.Initializer);
        }

        private static void InitializeImpl(Type type)
        {
            var teardown = Activator.CreateInstance(type) as IDisposable;
            if (teardown != null)
                AppDomain.CurrentDomain.DomainUnload += (s, e) => teardown.Dispose();
        }
    }
}
using System;
using System.Reflection;

namespace Jwc.Experiment.Xunit
{
    /// <summary>
    ///     Attribute to supply a type of which instance will be used to set up or to tear down
    ///     a test fixture only once on assembly level.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class AssemblyCustomizationAttribute : Attribute
    {
        private static readonly object _syncLock = new object();
        private static bool _configured;

        private readonly Type _customizationType;

        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="AssemblyCustomizationAttribute" /> class.
        /// </summary>
        /// <param name="customizationType">
        ///     A type to be used to set up or to tear down a test fixture only once on assembly
        ///     level.
        /// </param>
        public AssemblyCustomizationAttribute(Type customizationType)
        {
            if (customizationType == null)
                throw new ArgumentNullException("customizationType");

            _customizationType = customizationType;
        }

        /// <summary>
        ///     Gets a type to be used to set up or to tear down a test fixture only once on
        ///     assembly level.
        /// </summary>
        public Type CustomizationType
        {
            get
            {
                return _customizationType;
            }
        }

        internal static void Customize(Assembly testAssembly)
        {
            if (_configured)
                return;

            lock (_syncLock)
            {
                if (_configured)
                    return;

                CustomizeSync(testAssembly);
                _configured = true;
            }
        }

        private static void CustomizeSync(Assembly testAssembly)
        {
            var attribures = testAssembly.GetCustomAttributes(typeof(AssemblyCustomizationAttribute), false);
            foreach (AssemblyCustomizationAttribute attribure in attribures)
                CustomizeSync(attribure.CustomizationType);
        }

        private static void CustomizeSync(Type customizationType)
        {
            var customization = Activator.CreateInstance(customizationType) as IDisposable;
            if (customization != null)
                AppDomain.CurrentDomain.DomainUnload += (s, e) => customization.Dispose();
        }
    }
}
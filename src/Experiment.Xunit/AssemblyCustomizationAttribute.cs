using System;

namespace Jwc.Experiment.Xunit
{
    /// <summary>
    ///     Attribute to supply a type of which instance will be used to set up or to tear down
    ///     a test fixture only once on assembly level.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class AssemblyCustomizationAttribute : Attribute
    {
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
    }
}
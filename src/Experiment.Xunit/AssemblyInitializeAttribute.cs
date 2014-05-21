using System;

namespace Jwc.Experiment.Xunit
{
    /// <summary>
    /// Attribute to supply a type of which instance will be used to set up or
    /// to tear down a test fixture only once on assembly level.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class AssemblyInitializeAttribute : Attribute
    {
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
    }
}
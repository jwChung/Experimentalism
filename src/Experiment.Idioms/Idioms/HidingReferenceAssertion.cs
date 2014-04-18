using System.Collections.Generic;
using System.Reflection;
using Ploeh.Albedo;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents assertions to verify that specified assemblies are not
    /// directly referenced.
    /// </summary>
    public class HidingReferenceAssertion : ReflectionVisitor<object>
    {
        private readonly Assembly[] _assemblies;

        /// <summary>
        /// Initializes a new instance of the <see cref="HidingReferenceAssertion"/> class.
        /// </summary>
        /// <param name="assemblies">
        /// The assemblies which are not referenced from elements.
        /// </param>
        public HidingReferenceAssertion(params Assembly[] assemblies)
        {
            _assemblies = assemblies;
        }

        /// <summary>
        /// Gets the observation or value produced by this instance.
        /// </summary>
        public override object Value
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        /// <summary>
        /// Gets a value indicating the assemblies which are not referenced from
        /// elements.
        /// </summary>
        public IEnumerable<Assembly> Assemblies
        {
            get
            {
                return _assemblies;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Reflection;
using Ploeh.Albedo;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents assertions to verify that references of a assembly are same
    /// with specified assemblies.
    /// </summary>
    public class RestrictingReferenceAssertion : ReflectionVisitor<object>
    {
        private readonly Assembly[] _assemblies;

        /// <summary>
        /// Initializes a new instance of the <see cref="RestrictingReferenceAssertion"/> class.
        /// </summary>
        /// <param name="assemblies">The assemblies which elements only reference.</param>
        public RestrictingReferenceAssertion(params Assembly[] assemblies)
        {
            if (assemblies == null)
            {
                throw new ArgumentNullException("assemblies");
            }

            _assemblies = assemblies;
        }

        /// <summary>
        /// Gets the observation or value produced by this instance.
        /// </summary>
        public override object Value
        {
            get
            {
                //throw new NotSupportedException(
                //    "This Value property isn't supported because the main purpose of " +
                //    "this class is to verify whether only specified assemblies are referenced.");
                return null;
            }
        }

        /// <summary>
        /// Gets a value indicating the assemblies.
        /// </summary>
        public IEnumerable<Assembly> Assemblies
        {
            get
            {
                return _assemblies;
            }
        }

        /// <summary>
        /// Allows an <see cref="AssemblyElement" /> to be visited.
        /// This method is called when the element accepts this visitor
        /// instance.
        /// </summary>
        /// <param name="assemblyElement">The <see cref="AssemblyElement" /> being visited.
        /// </param>
        /// <returns>
        /// A <see cref="IReflectionVisitor{T}" /> instance which can be used
        /// to continue the visiting process with potentially updated
        /// observations.
        /// </returns>
        public override IReflectionVisitor<object> Visit(AssemblyElement assemblyElement)
        {
            throw new NotImplementedException();
        }
    }
}
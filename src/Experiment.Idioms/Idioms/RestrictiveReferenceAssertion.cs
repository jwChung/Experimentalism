using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Ploeh.Albedo;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Encapsulates a unit test that verifies that all references of an assembly are specified.
    /// </summary>
    public class RestrictiveReferenceAssertion : IIdiomaticAssemblyAssertion
    {
        private readonly Assembly[] restrictiveReferences;

        /// <summary>
        /// Initializes a new instance of the <see cref="RestrictiveReferenceAssertion" /> class.
        /// </summary>
        /// <param name="restrictiveReferences">
        /// The restrictive references to specify all references of a certain assembly.
        /// </param>
        public RestrictiveReferenceAssertion(params Assembly[] restrictiveReferences)
        {
            this.restrictiveReferences = restrictiveReferences;
        }

        /// <summary>
        /// Gets a value indicating the restrictive references.
        /// </summary>
        public IEnumerable<Assembly> RestrictiveReferences
        {
            get
            {
                return this.restrictiveReferences;
            }
        }

        /// <summary>
        /// Verifies that all references of an assembly are specified through the restrictive
        /// references.
        /// </summary>
        /// <param name="assembly">
        /// The assembly.
        /// </param>
        public void Verify(Assembly assembly)
        {
            var references = assembly.ToElement()
                .Accept(new ReferenceCollector()).Value.Except(new[] { assembly })
                .ToArray();

            foreach (var reference in references)
                this.Verify(assembly, reference);

            foreach (var restrictiveReference in this.RestrictiveReferences)
                this.Verify(assembly, references, restrictiveReference);
        }

        private void Verify(Assembly assembly, Assembly reference)
        {
            if (this.RestrictiveReferences.Contains(reference))
                return;

            var messageFormat = @"The reference of the assembly is not specified in the restricted references.
Reference: {0}
Assembly : {1}
Restrictive references:
{2}";

            throw new RestrictiveReferenceException(
                string.Format(
                    CultureInfo.CurrentCulture,
                    messageFormat,
                    reference,
                    assembly,
                    this.GetRestrictiveReferenceString()));
        }

        private void Verify(Assembly assembly, IEnumerable<Assembly> references, Assembly restrictiveReference)
        {
            if (references.Contains(restrictiveReference))
                return;

            var messageFormat = @"The unused reference of the assembly should not be specified in the restricted references.
Unused Reference: {0}
Assembly : {1}
Restrictive references:
{2}";

            throw new RestrictiveReferenceException(
                string.Format(
                    CultureInfo.CurrentCulture,
                    messageFormat,
                    restrictiveReference,
                    assembly,
                    this.GetRestrictiveReferenceString()));
        }

        private string GetRestrictiveReferenceString()
        {
            return string.Join(
                "," + Environment.NewLine,
                this.RestrictiveReferences.Select(r => new string(' ', 11) + r));
        }
    }
}
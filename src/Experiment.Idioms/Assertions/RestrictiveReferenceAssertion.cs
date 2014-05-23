using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Ploeh.Albedo;

namespace Jwc.Experiment.Idioms.Assertions
{
    /// <summary>
    ///     Encapsulates a unit test that verifies that all references of an assembly are
    ///     specified.
    /// </summary>
    public class RestrictiveReferenceAssertion : IIdiomaticAssemblyAssertion
    {
        private readonly Assembly[] _restrictiveReferences;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RestrictiveReferenceAssertion" />
        ///     class.
        /// </summary>
        /// <param name="restrictiveReferences">
        ///     The restrictive references to specify all references of a certain assembly.
        /// </param>
        public RestrictiveReferenceAssertion(params Assembly[] restrictiveReferences)
        {
            _restrictiveReferences = restrictiveReferences;
        }

        /// <summary>
        ///     Gets a value indicating the restrictive references.
        /// </summary>
        public IEnumerable<Assembly> RestrictiveReferences
        {
            get
            {
                return _restrictiveReferences;
            }
        }

        /// <summary>
        ///     Verifies that all references of an assembly are specified through the restrictive
        ///     references.
        /// </summary>
        /// <param name="assembly">
        ///     The assembly.
        /// </param>
        public void Verify(Assembly assembly)
        {
            var references = assembly.ToElement().Accept(new ReferenceCollector()).Value.Except(new[] { assembly });
            foreach (var reference in references)
            {
                if (RestrictiveReferences.Contains(reference))
                    continue;

                var messageFormat = @"The reference of the assembly is not specified through the restrictive references.
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
                        GetRestrictiveReferenceString()));
            }
        }

        private string GetRestrictiveReferenceString()
        {
            return string.Join(
                "," + Environment.NewLine,
                RestrictiveReferences.Select(r => new string(' ', 11) + r));
        }
    }
}
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
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
            if (assemblyElement == null)
            {
                throw new ArgumentNullException("assemblyElement");
            }

            var assembly = assemblyElement.Assembly;
            var assemblies = GetReferencedAssemblies(assembly);
            if (AreEquivalent(assemblies))
            {
                return this;
            }

            throw new RestrictingReferenceException(
                string.Format(
                    CultureInfo.CurrentCulture,
                    "The actual referenced assemblies are different from the specified assemblies:{0}" +
                    "Actual   : {1}{0}" +
                    "Specified: {2}",
                    Environment.NewLine,
                    GetAssembyJoinedString(assemblies),
                    GetAssembyJoinedString(Assemblies)));
        }

        private static Assembly[] GetReferencedAssemblies(Assembly assembly)
        {
            var assembliesFromTypes = GetAssembliesFromTypes(assembly);
            return assembly.GetReferencedAssemblies()
                .Select(Assembly.Load).Concat(assembliesFromTypes)
                .Distinct().Except(new[] { assembly }).ToArray();
        }

        private bool AreEquivalent(ICollection<Assembly> assemblies)
        {
            return assemblies.Count == _assemblies.Length
                && !assemblies.Except(_assemblies).Any();
        }

        private static IEnumerable<Assembly> GetAssembliesFromTypes(Assembly assembly)
        {
            return assembly.GetExportedTypes()
                .SelectMany(t => t.GetReferencedAssemblies())
                .Aggregate(new HashSet<Assembly>(), (s, a) => { s.Add(a); return s; });
        }

        private static string GetAssembyJoinedString(IEnumerable<Assembly> assemblies)
        {
            return "{" + string.Join(", ", assemblies.Select(a => a.GetName().Name).OrderBy(n => n)) + " }";
        }
    }
}
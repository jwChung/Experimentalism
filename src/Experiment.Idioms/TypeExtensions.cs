using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents type extensions.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Gets the referenced assemblies.
        /// </summary>
        /// <param name="type">The target type.</param>
        /// <returns>The result assemblies.</returns>
        public static IEnumerable<Assembly> GetReferencedAssemblies(this Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            return ReferencedAssembliesFromAncestors(type)
                .Concat(GetReferencedAssembliesFromInterfaces(type))
                .Distinct();
        }

        private static IEnumerable<Assembly> ReferencedAssembliesFromAncestors(Type type)
        {
            var assemblies = new[] { type.Assembly };

            return type.BaseType == null 
                ? assemblies
                : assemblies.Concat(ReferencedAssembliesFromAncestors(type.BaseType));
        }

        private static IEnumerable<Assembly> GetReferencedAssembliesFromInterfaces(Type type)
        {
            return type.GetInterfaces().Select(i => i.Assembly);
        }
    }
}
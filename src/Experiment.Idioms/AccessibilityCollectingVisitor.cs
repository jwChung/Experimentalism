using System;
using System.Collections.Generic;
using System.Reflection;
using Ploeh.Albedo;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents a class to collect <see cref="Accessibilities"/>.
    /// </summary>
    public class AccessibilityCollectingVisitor : ReflectionVisitor<IEnumerable<Accessibilities>>
    {
        /// <summary>
        /// Gets the observation or value produced by this instance.
        /// </summary>
        public override IEnumerable<Accessibilities> Value
        {
            get
            {
                return new Accessibilities[0];
            }
        }

        /// <summary>
        /// Allows an <see cref="AssemblyElement"/> to be visited. 
        /// This method is called when the element accepts this visitor
        /// instance.
        /// </summary>
        /// <param name="assemblyElement">
        /// The <see cref="AssemblyElement"/> being visited.
        /// </param>
        /// <returns>
        /// A <see cref="IReflectionVisitor{T}" /> instance which can be used
        /// to continue the visiting process with potentially updated
        /// observations.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<Accessibilities>> Visit(
            AssemblyElement assemblyElement)
        {
            throw new NotSupportedException("An assembly does not has any accessibilities.");
        }
    }
}
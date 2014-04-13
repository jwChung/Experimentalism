using System.Collections.Generic;
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
    }
}
using System.Collections.Generic;
using Ploeh.Albedo;

namespace Jwc.Experiment
{
    /// <summary>
    /// Represetns a display name of a reflection member.
    /// </summary>
    public class DisplayNameVisitor : ReflectionVisitor<IEnumerable<string>>
    {
        /// <summary>
        /// Gets the observation or value produced by this instance.
        /// </summary>
        public override IEnumerable<string> Value
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
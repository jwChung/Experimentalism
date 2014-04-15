using System.Collections.Generic;
using Ploeh.Albedo;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represent comaprer to determine that a field value equals to
    /// a parameter value.
    /// </summary>
    public class FieldToParameterComparer : InverseEqualityComparer<IReflectionElement>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FieldToParameterComparer"/> class.
        /// </summary>
        /// <param name="testFixture">The test fixture.</param>
        public FieldToParameterComparer(ITestFixture testFixture)
            : base(EqualityComparer<IReflectionElement>.Default)
        {
        }
    }
}
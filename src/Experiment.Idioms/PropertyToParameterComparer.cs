using Ploeh.Albedo;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represent comaprer to determine that a property value equals to
    /// a parameter value.
    /// </summary>
    public class PropertyToParameterComparer : InverseEqualityComparer<IReflectionElement>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyToParameterComparer" /> class.
        /// </summary>
        /// <param name="testFixture">The test fixture.</param>
        public PropertyToParameterComparer(ITestFixture testFixture)
            : base(new ParameterToPropertyComparer(testFixture))
        {
        }
    }
}
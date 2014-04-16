using Ploeh.Albedo;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represent comaprer to determine that a field value equals to
    /// a parameter value.
    /// </summary>
    public class FieldToParameterComparer : InverseEqualityComparer<IReflectionElement>
    {
        private readonly ITestFixture _testFixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldToParameterComparer"/> class.
        /// </summary>
        /// <param name="testFixture">The test fixture.</param>
        public FieldToParameterComparer(ITestFixture testFixture)
            : base(new ParameterToFieldComparer(testFixture))
        {
            _testFixture = testFixture;
        }

        /// <summary>
        /// Gets a value indicating the test fixture.
        /// </summary>
        public ITestFixture TestFixture
        {
            get
            {
                return _testFixture;
            }
        }
    }
}
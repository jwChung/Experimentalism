using Ploeh.Albedo;

namespace Jwc.Experiment
{
    /// <summary>
    ///     Represent comaprer to determine that a property value equals to a parameter value.
    /// </summary>
    public class PropertyToParameterComparer : InverseEqualityComparer<IReflectionElement>
    {
        private readonly ITestFixture _testFixture;

        /// <summary>
        ///     Initializes a new instance of the <see cref="PropertyToParameterComparer" /> class.
        /// </summary>
        /// <param name="testFixture">
        ///     The test fixture.
        /// </param>
        public PropertyToParameterComparer(ITestFixture testFixture)
            : base(new ParameterToPropertyComparer(testFixture))
        {
            _testFixture = testFixture;
        }

        /// <summary>
        ///     Gets a value indicating the test fixture.
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
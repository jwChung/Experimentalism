namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Encapsulates a unit test that verifies that a method or constructor has
    /// appropriate Guard Clauses in place.
    /// </summary>
    public class GuardClauseAssertion : AssertionAdapter
    {
        private readonly ITestFixture _testFixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="GuardClauseAssertion" /> class.
        /// </summary>
        /// <param name="testFixture">
        /// The test fixture.
        /// </param>
        public GuardClauseAssertion(ITestFixture testFixture)
            : base(new Ploeh.AutoFixture.Idioms.GuardClauseAssertion(new SpecimenBuilderAdapter(testFixture)))
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
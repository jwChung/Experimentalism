using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Kernel;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Encapsulates a unit test that verifies that a method or constructor has
    /// appropriate Guard Clauses in place.
    /// </summary>
    public class GuardClauseAssertion : AssertionAdapter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GuardClauseAssertion" /> class.
        /// </summary>
        /// <param name="testFixture">
        /// The test fixture.
        /// </param>
        public GuardClauseAssertion(ITestFixture testFixture)
            : base(new WritablePropertyAssertion(new ArrayRelay()))
        {
        }
    }
}
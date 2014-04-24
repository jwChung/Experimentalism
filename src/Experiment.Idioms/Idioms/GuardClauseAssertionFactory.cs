using Ploeh.Albedo;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents a factory to create <see cref="GuardClauseAssertion"/>
    /// </summary>
    public class GuardClauseAssertionFactory : IAssertionFactory
    {
        /// <summary>
        /// Creates an assertion.
        /// </summary>
        /// <param name="testFixture">The test fixture.</param>
        /// <returns>
        /// The created assertion.
        /// </returns>
        public IReflectionVisitor<object> Create(ITestFixture testFixture)
        {
            return new GuardClauseAssertion(testFixture);
        }
    }
}
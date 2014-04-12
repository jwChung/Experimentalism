using Ploeh.Albedo;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents an assertion factory.
    /// </summary>
    public interface IAssertionFactory
    {
        /// <summary>
        /// Creates an assertion.
        /// </summary>
        /// <param name="testFixture">The test fixture.</param>
        /// <returns>The created assertion.</returns>
        IReflectionVisitor<object> Create(ITestFixture testFixture);
    }
}
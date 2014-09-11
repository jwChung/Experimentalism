namespace Jwc.Experiment.Xunit
{
    using System.Reflection;

    /// <summary>
    /// Represents a factory to create an instance of <see cref="ITestFixture" />.
    /// </summary>
    public interface ITestFixtureFactory
    {
        /// <summary>
        /// Creates a test fixture.
        /// </summary>
        /// <param name="context">
        /// The test information about a test method.
        /// </param>
        /// <returns>
        /// The test fixture.
        /// </returns>
        ITestFixture Create(ITestMethodContext context);
    }
}
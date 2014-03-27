using System.Reflection;

namespace Jwc.Experiment
{
    /// <summary>
    /// Represents a fixture facotry to create an instance of
    /// the <see cref="ITestFixture"/> type.
    /// </summary>
    public interface ITestFixtureFactory
    {
        /// <summary>
        /// Creates an instance of <see cref="ITestFixture"/>.
        /// </summary>
        /// <param name="method">
        /// The test method
        /// </param>
        /// <returns>
        /// The created fixture.
        /// </returns>
        ITestFixture Create(MethodInfo method);
    }
}
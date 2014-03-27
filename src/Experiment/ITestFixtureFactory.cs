using System.Reflection;

namespace Jwc.Experiment
{
    /// <summary>
    /// Represents a facotry to create an instance of
    /// the <see cref="ITestFixture"/> type.
    /// </summary>
    public interface ITestFixtureFactory
    {
        /// <summary>
        /// Creates an instance of <see cref="ITestFixture"/>.
        /// </summary>
        /// <param name="method">
        /// The method to be called when a test is executed.
        /// </param>
        /// <returns>
        /// The created fixture.
        /// </returns>
        ITestFixture Create(MethodInfo method);
    }
}
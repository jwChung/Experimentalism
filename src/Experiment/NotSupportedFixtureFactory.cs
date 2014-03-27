using System.Reflection;

namespace Jwc.Experiment
{
    /// <summary>
    /// Represents a default fixture factory to create an instance of
    /// <see cref="NotSupportedFixture"/>.
    /// </summary>
    public class NotSupportedFixtureFactory : ITestFixtureFactory
    {
        /// <summary>
        /// Creates an instance of <see cref="NotSupportedFixture" />.
        /// </summary>
        /// <param name="method">
        /// The method to be called when a test is executed.
        /// </param>
        /// <returns>
        /// The created fixture.
        /// </returns>
        public ITestFixture Create(MethodInfo method)
        {
            return new NotSupportedFixture();
        }
    }
}
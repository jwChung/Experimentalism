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
        /// <param name="testMethod">
        /// The test method
        /// </param>
        /// <returns>
        /// The created fixture.
        /// </returns>
        public ITestFixture Create(MethodInfo testMethod)
        {
            return new NotSupportedFixture();
        }
    }
}
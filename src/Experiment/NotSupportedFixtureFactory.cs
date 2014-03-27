using System.Reflection;

namespace Jwc.Experiment
{
    /// <summary>
    /// Represents a default factory to create an instance of
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
        /// The result instance.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public ITestFixture Create(MethodInfo method)
        {
            throw new System.NotImplementedException();
        }
    }
}
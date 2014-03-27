using System.Reflection;
using Ploeh.AutoFixture;

namespace Jwc.Experiment
{
    /// <summary>
    /// Represents a fixture factory to create an instance of
    /// <see cref="Fixture"/>.
    /// </summary>
    public class AutoFixtureFactory : ITestFixtureFactory
    {
        /// <summary>
        /// Creates an instance of <see cref="Fixture" />.
        /// </summary>
        /// <param name="testMethod">The test method</param>
        /// <returns>
        /// The created fixture.
        /// </returns>
        public ITestFixture Create(MethodInfo testMethod)
        {
            throw new System.NotImplementedException();
        }
    }
}
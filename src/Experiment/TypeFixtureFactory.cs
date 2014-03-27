using System.Reflection;

namespace Jwc.Experiment
{
    /// <summary>
    /// Represents a test factory to create an instance base on its type.
    /// </summary>
    public class TypeFixtureFactory : ITestFixtureFactory
    {
        /// <summary>
        /// Creates an instance of <see cref="ITestFixture" />. This uses
        /// a default constructor of supplied test fixture.
        /// </summary>
        /// <param name="method">
        /// The method to be called when a test is executed.
        /// </param>
        /// <returns>
        /// The result instance.
        /// </returns>
        public ITestFixture Create(MethodInfo method)
        {
            throw new System.NotImplementedException();
        }
    }
}
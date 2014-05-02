using System.Reflection;
using Ploeh.AutoFixture;

namespace NuGet.Jwc.Experiment
{
    /// <summary>
    /// Represents a factory to create <see cref="IFixture"/>
    /// </summary>
    public class FixtureFactory
    {
        /// <summary>
        /// Creates a fixture.
        /// </summary>
        /// <param name="testMethod">The test method.</param>
        /// <returns>The fixture.</returns>
        public static IFixture Create(MethodInfo testMethod)
        {
            return new Fixture();
        }
    }
}
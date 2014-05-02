using System.Reflection;
using Ploeh.AutoFixture;

namespace NuGet.Jwc.Experiment
{
    /// <summary>
    /// Represents a factory to create <see cref="IFixture"/>
    /// </summary>
    public static class FixtureFactory
    {
        /// <summary>
        /// Creates a fixture.
        /// </summary>
        /// <param name="testMethod">The test method.</param>
        /// <returns>The fixture.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "testMethod", Justification = "The testMethod parameter can be used in future.")]
        public static IFixture Create(MethodInfo testMethod)
        {
            return new Fixture();
        }
    }
}
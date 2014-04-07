using Xunit.Sdk;

namespace Jwc.Experiment
{
    /// <summary>
    /// Represents a test-case that can be turned into an xUnit.net
    /// ITestCommand when returned from a test method adorned with the
    /// <see cref="BaseFirstClassTheoremAttribute" />.
    /// </summary>
    public interface ITestCase
    {
        /// <summary>
        /// Converts the instance to an xUnit.net ITestCommand instance.
        /// </summary>
        /// <param name="method">
        /// The method adorned by a <see cref="BaseFirstClassTheoremAttribute" />.
        /// </param>
        /// <param name="fixtureFactory">
        /// A test fixture factory to provide auto data.
        /// </param>
        /// <returns>
        /// An xUnit.net ITestCommand that represents the executable test case.
        /// </returns>
        ITestCommand ConvertToTestCommand(IMethodInfo method, ITestFixtureFactory fixtureFactory);
    }
}
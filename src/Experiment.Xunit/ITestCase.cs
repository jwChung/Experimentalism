namespace Jwc.Experiment.Xunit
{
    using global::Xunit.Sdk;
    using ITestFixtureFactory2 = Experiment.ITestFixtureFactory;

    /// <summary>
    /// Represents a test-case that can be turned into an xUnit.net ITestCommand when returned from
    /// a test method adorned with the first-class test attribute.
    /// </summary>
    public interface ITestCase
    {
        /// <summary>
        /// Converts the instance to an xUnit.net ITestCommand instance.
        /// </summary>
        /// <param name="method">
        /// The method adorned by a first-class test attribute.
        /// </param>
        /// <param name="testFixtureFactory">
        /// A test fixture factory to provide auto data.
        /// </param>
        /// <returns>
        /// An xUnit.net ITestCommand that represents the executable test case.
        /// </returns>
        ITestCommand ConvertToTestCommand(IMethodInfo method, ITestFixtureFactory2 testFixtureFactory);
    }
}
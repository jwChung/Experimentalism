namespace Jwc.Experiment.Xunit
{
    using global::Xunit;
    using global::Xunit.Sdk;

    public class StaticTestCaseCommandContextTest
    {
        [Fact]
        public void SutIsTestCommandContext()
        {
            var sut = new StaticTestCaseCommandContext(
                Mocked.Of<IMethodInfo>(),
                Mocked.Of<IMethodInfo>(),
                Mocked.Of<ITestFixtureFactory>(),
                new object[0]);
            Assert.IsAssignableFrom<TestCommandContext2>(sut);
        }
    }
}
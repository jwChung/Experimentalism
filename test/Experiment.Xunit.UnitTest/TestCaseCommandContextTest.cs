namespace Jwc.Experiment.Xunit
{
    using global::Xunit;
    using global::Xunit.Sdk;

    public class TestCaseCommandContextTest
    {
        [Fact]
        public void SutIsTestCommandContext()
        {
            var sut = new TestCaseCommandContext(
                Mocked.Of<IMethodInfo>(),
                Mocked.Of<IMethodInfo>(),
                new object(),
                Mocked.Of<ITestFixtureFactory>(),
                new object[0]);
            Assert.IsAssignableFrom<TestCommandContext2>(sut);
        }

    }
}
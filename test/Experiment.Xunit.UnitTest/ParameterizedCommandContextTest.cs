namespace Jwc.Experiment.Xunit
{
    using System.Collections.Generic;
    using global::Xunit;

    public class ParameterizedCommandContextTest
    {
        [Fact]
        public void SutIsTestCommandContext()
        {
            var sut = new ParameterizedCommandContext(
                Mocked.Of<ITestFixtureFactory>(),
                Mocked.Of<IEnumerable<object>>());
            Assert.IsAssignableFrom<TestCommandContext2>(sut);
        }
    }
}
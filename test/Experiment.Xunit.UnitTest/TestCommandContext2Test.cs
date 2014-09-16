namespace Jwc.Experiment.Xunit
{
    using System;
    using global::Xunit;

    public class TestCommandContext2Test
    {
        [Fact]
        public void SutIsTestCommandContext()
        {
            var sut = Mocked.Of<TestCommandContext2>();
            Assert.IsAssignableFrom<ITestCommandContext>(sut);
        }
    }
}

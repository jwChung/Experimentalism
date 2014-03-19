using Xunit;

namespace Jwc.Experiment
{
    public class NotSupportedFixtureTest
    {
        [Fact]
        public void SutIsTestFixture()
        {
            var sut = new NotSupportedFixture();
            Assert.IsAssignableFrom<ITestFixture>(sut);
        }
    }
}
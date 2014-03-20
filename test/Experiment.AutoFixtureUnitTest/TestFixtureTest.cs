using Jwc.Experiment;
using Xunit;

namespace Experiment.AutoFixtureUnitTest
{
    public class TestFixtureTest
    {
        [Fact]
        public void SutIsTestFixture()
        {
            var sut = new TestFixture();
            Assert.IsAssignableFrom<ITestFixture>(sut);
        } 
    }
}
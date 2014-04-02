using Xunit;

namespace Jwc.Experiment
{
    public class TestCaseTest
    {
        [Fact]
        public void SutIsTestCase()
        {
            var sut = new TestCase(() => { });
            Assert.IsAssignableFrom<ITestCase>(sut);
        }
    }
}
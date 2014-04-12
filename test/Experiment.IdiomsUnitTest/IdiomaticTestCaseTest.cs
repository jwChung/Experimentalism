using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class IdiomaticTestCaseTest
    {
        [Fact]
        public void SutIsTestCase()
        {
            var sut = new IdiomaticTestCase();
            Assert.IsAssignableFrom<ITestCase>(sut);
        }
    }
}
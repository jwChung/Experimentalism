using Xunit;

namespace Jwc.Experiment
{
    public class NotSupportedFixtureFactoryTest
    {
        [Fact]
        public void SutIsTestFixtureFactory()
        {
            var sut = new NotSupportedFixtureFactory();
            Assert.IsAssignableFrom<ITestFixtureFactory>(sut);
        }
    }
}
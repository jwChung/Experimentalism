using Xunit;

namespace Jwc.Experiment
{
    public class AutoFixtureFactoryTest
    {
        [Fact]
        public void SutIsTestFixtureFactory()
        {
            var sut = new AutoFixtureFactory();
            Assert.IsAssignableFrom<ITestFixtureFactory>(sut);
        }
    }
}
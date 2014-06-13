using Xunit;

namespace Jwc.Experiment
{
    public class FuncTestFixtureFactoryTest
    {
        [Fact]
        public void SutIsTestFixtureFactory()
        {
            var sut = new FuncTestFixtureFactory();
            Assert.IsAssignableFrom<ITestFixtureFactory>(sut);
        } 
    }
}
using Xunit;

namespace Jwc.Experiment
{
    public class TypeFixtureFactoryTest
    {
        [Fact]
        public void SutIsTestFixtureFactory()
        {
            var sut = new TypeFixtureFactory();
            Assert.IsAssignableFrom<ITestFixtureFactory>(sut);
        } 
    }
}
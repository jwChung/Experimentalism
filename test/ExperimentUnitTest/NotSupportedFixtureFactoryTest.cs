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

        [Fact]
        public void CreateReturnsNotSupportedFixture()
        {
            var sut = new NotSupportedFixtureFactory();
            var actual = sut.Create(null);
            Assert.IsType<NotSupportedFixture>(actual);
        }
    }
}
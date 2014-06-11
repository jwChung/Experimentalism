using Xunit;

namespace Jwc.Experiment.AutoFixture
{
    public class TestFixtureFactoryConfigurationAttributeTest
    {
        [Fact]
        public void SutIsDefaultFixtureFactoryConfigurationAttribute()
        {
            var sut = new TestFixtureFactoryConfigurationAttribute();
            Assert.IsAssignableFrom<DefaultFixtureFactoryConfigurationAttribute>(sut);
        }

        [Fact]
        public void FactoryIsCorrect()
        {
            var sut = new TestFixtureFactoryConfigurationAttribute();

            var actual = sut.Factory;

            Assert.IsAssignableFrom<TestFixtureFactory>(actual);
            Assert.Same(sut.Factory, actual);
        }
    }
}
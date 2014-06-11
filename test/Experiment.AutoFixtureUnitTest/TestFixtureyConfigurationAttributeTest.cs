using Xunit;

namespace Jwc.Experiment.AutoFixture
{
    public class TestFixtureyConfigurationAttributeTest
    {
        [Fact]
        public void SutIsDefaultFixtureConfigurationAttribute()
        {
            var sut = new TestFixtureyConfigurationAttribute();
            Assert.IsAssignableFrom<DefaultFixtureConfigurationAttribute>(sut);
        }

        [Fact]
        public void FactoryIsCorrect()
        {
            var sut = new TestFixtureyConfigurationAttribute();

            var actual = sut.Factory;

            Assert.IsAssignableFrom<TestFixtureFactory>(actual);
            Assert.Same(sut.Factory, actual);
        }
    }
}
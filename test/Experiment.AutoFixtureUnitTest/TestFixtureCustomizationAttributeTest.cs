using Xunit;

namespace Jwc.Experiment.AutoFixture
{
    public class TestFixtureCustomizationAttributeTest
    {
        [Fact]
        public void SutIsDefaultFixtureCustomizationAttribute()
        {
            var sut = new TestFixtureCustomizationAttribute();
            Assert.IsAssignableFrom<DefaultFixtureCustomizationAttribute>(sut);
        }

        [Fact]
        public void FactoryIsCorrect()
        {
            var sut = new TestFixtureCustomizationAttribute();

            var actual = sut.Factory;

            Assert.IsAssignableFrom<TestFixtureFactory>(actual);
            Assert.Same(sut.Factory, actual);
        }
    }
}
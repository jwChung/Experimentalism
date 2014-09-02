namespace Jwc.Experiment.AutoFixture
{
    using global::Xunit;

    public class TestFixtureConfigurationAttributeTest
    {
        [Fact]
        public void SutIsDefaultFixtureConfigurationAttribute()
        {
            var sut = new TestFixtureConfigurationAttribute();
            Assert.IsAssignableFrom<DefaultFixtureConfigurationAttribute>(sut);
        }

        [Fact]
        public void FactoryIsCorrect()
        {
            var sut = new TestFixtureConfigurationAttribute();

            var actual = sut.Factory;

            Assert.IsAssignableFrom<TestFixtureFactory>(actual);
            Assert.Same(sut.Factory, actual);
        }
    }
}
namespace Jwc.Experiment.AutoFixture
{
    using global::Xunit;

    public class AutoFixtureConfigurationAttributeTest
    {
        [Fact]
        public void SutIsDefaultFixtureConfigurationAttribute()
        {
            var sut = new AutoFixtureConfigurationAttribute();
            Assert.IsAssignableFrom<TestFixtureConfigurationAttribute>(sut);
        }

        [Fact]
        public void FactoryIsCorrect()
        {
            var sut = new AutoFixtureConfigurationAttribute();

            var actual = sut.Factory;

            Assert.IsAssignableFrom<TestFixtureFactory>(actual);
            Assert.Same(sut.Factory, actual);
        }
    }
}
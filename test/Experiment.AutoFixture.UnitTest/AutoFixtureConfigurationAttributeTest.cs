namespace Jwc.Experiment.AutoFixture
{
    using System;
    using global::Xunit;

    [Obsolete]
    public class AutoFixtureConfigurationAttributeTest
    {
        [Fact]
        public void SutIsTestFixtureConfigurationAttribute()
        {
            var sut = new AutoFixtureConfigurationAttribute();
            Assert.IsAssignableFrom<Experiment.TestFixtureConfigurationAttribute>(sut);
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
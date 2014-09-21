namespace Jwc.Experiment.AutoFixture
{
    using System;
    using global::Xunit;
    using Ploeh.AutoFixture;

    public class OmitAutoPropertiesCustomizationTest
    {
        [Fact]
        public void SutIsCustomization()
        {
            var sut = new OmitAutoPropertiesCustomization();
            Assert.IsAssignableFrom<ICustomization>(sut);
        }

        [Fact]
        public void CustomizeWitNullFixtureThrows()
        {
            var sut = new OmitAutoPropertiesCustomization();
            Assert.Throws<ArgumentNullException>(() => sut.Customize(null));
        }

        [Fact]
        public void CustomizeCorrectlySetsOmitAutproperties()
        {
            var sut = new OmitAutoPropertiesCustomization();
            var fixture = new Fixture();
            Assert.False(fixture.OmitAutoProperties);

            sut.Customize(fixture);

            Assert.True(fixture.OmitAutoProperties);
        }
    }
}
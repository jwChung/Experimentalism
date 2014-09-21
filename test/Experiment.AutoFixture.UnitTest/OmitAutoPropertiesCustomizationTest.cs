namespace Jwc.Experiment.AutoFixture
{
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
    }
}
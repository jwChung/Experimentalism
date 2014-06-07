using Xunit;

namespace Jwc.Experiment
{
    public class DefaultFixtureFactoryConfigurationAttributeTest
    {
        [Fact]
        public void SutIsAssemblyFixtureConfigurationAttribute()
        {
            var sut = new DefaultFixtureFactoryConfigurationAttribute();
            Assert.IsAssignableFrom<AssemblyFixtureConfigurationAttribute>(sut);
        }
    }
}
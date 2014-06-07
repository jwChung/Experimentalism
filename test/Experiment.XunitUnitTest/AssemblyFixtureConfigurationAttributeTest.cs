using System;
using Xunit;

namespace Jwc.Experiment.Xunit
{
    public class AssemblyFixtureConfigurationAttributeTest
    {
        [Fact]
        public void SutIsAttribute()
        {
            var sut = new TssAssemblyFixtureConfigurationAttribute();
            Assert.IsAssignableFrom<Attribute>(sut);
        }
        
        private class TssAssemblyFixtureConfigurationAttribute : AssemblyFixtureConfigurationAttribute
        {
        }
    }
}
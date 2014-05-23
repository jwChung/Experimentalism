using System;
using Xunit;

namespace Jwc.Experiment.Xunit
{
    public class AssemblyFixtureCustomizationAttributeTest
    {
        [Fact]
        public void SutIsAttribute()
        {
            var sut = new AssemblyFixtureCustomizationAttribute(typeof(object));
            Assert.IsAssignableFrom<Attribute>(sut);
        }

        [Fact]
        public void InitializeWithNullInitializerThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new AssemblyFixtureCustomizationAttribute(null));
        }

        [Fact]
        public void CustomizationTypeIsCorrect()
        {
            var expected = GetType();
            var sut = new AssemblyFixtureCustomizationAttribute(expected);

            var actual = sut.CustomizationType;

            Assert.Equal(expected, actual);
        }
    }
}
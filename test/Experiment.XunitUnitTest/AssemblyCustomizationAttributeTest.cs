using System;
using Xunit;

namespace Jwc.Experiment.Xunit
{
    public class AssemblyCustomizationAttributeTest
    {
        [Fact]
        public void SutIsAttribute()
        {
            var sut = new AssemblyCustomizationAttribute(typeof(object));
            Assert.IsAssignableFrom<Attribute>(sut);
        }

        [Fact]
        public void InitializeWithNullInitializerThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new AssemblyCustomizationAttribute(null));
        }

        [Fact]
        public void CustomizationTypeIsCorrect()
        {
            var expected = GetType();
            var sut = new AssemblyCustomizationAttribute(expected);

            var actual = sut.CustomizationType;

            Assert.Equal(expected, actual);
        }
    }
}
using System;
using Xunit;

namespace Jwc.Experiment.Xunit
{
    public class AssemblyFixtureConfigAttributeTest
    {
        [Fact]
        public void SutIsAttribute()
        {
            var sut = new AssemblyFixtureConfigAttribute(typeof(object));
            Assert.IsAssignableFrom<Attribute>(sut);
        }

        [Fact]
        public void InitializeWithNullInitializerThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new AssemblyFixtureConfigAttribute(null));
        }

        [Fact]
        public void ConfigClassIsCorrect()
        {
            var expected = GetType();
            var sut = new AssemblyFixtureConfigAttribute(expected);

            var actual = sut.ConfigClass;

            Assert.Equal(expected, actual);
        }
    }
}

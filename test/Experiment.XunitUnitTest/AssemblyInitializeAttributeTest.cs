using System;
using Xunit;

namespace Jwc.Experiment.Xunit
{
    public class AssemblyInitializeAttributeTest
    {
        [Fact]
        public void SutIsAttribute()
        {
            var sut = new AssemblyInitializeAttribute(typeof(object));
            Assert.IsAssignableFrom<Attribute>(sut);
        }

        [Fact]
        public void InitializeWithNullInitializerThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new AssemblyInitializeAttribute(null));
        }

        [Fact]
        public void InitializerIsCorrect()
        {
            var expected = GetType();
            var sut = new AssemblyInitializeAttribute(expected);

            var actual = sut.Initializer;

            Assert.Equal(expected, actual);
        }
    }
}

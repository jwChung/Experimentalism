using System;
using System.Linq;
using Xunit;

namespace Jwc.Experiment
{
    public class TestFixtureFactoryAttributeTest
    {
        [Fact]
        public void SutIsAttribute()
        {
            var sut = new TestFixtureFactoryAttribute(typeof(DelegatingTestFixtureFactory));
            Assert.IsAssignableFrom<Attribute>(sut);
        }

        [Fact]
        public void SutHasCorrectAttributeUsage()
        {
            var attributeUsage = typeof(TestFixtureFactoryAttribute)
                .GetCustomAttributes(typeof(AttributeUsageAttribute), false)
                .Cast<AttributeUsageAttribute>().Single();
            Assert.False(attributeUsage.AllowMultiple);
            Assert.Equal(AttributeTargets.Assembly, attributeUsage.ValidOn);
        }

        [Fact]
        public void InitializeWithNullTypeThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new TestFixtureFactoryAttribute(null));
        }

        [Fact]
        public void InitializeWithInvalidTypeThrows()
        {
            Assert.Throws<ArgumentException>(() => new TestFixtureFactoryAttribute(typeof(object)));
        }

        [Fact]
        public void TypeIsCorrect()
        {
            var type = typeof(DelegatingTestFixtureFactory);
            var sut = new TestFixtureFactoryAttribute(type);

            var actual = sut.Type;

            Assert.Equal(type, actual);
        }
    }
}
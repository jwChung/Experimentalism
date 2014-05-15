using System;
using System.Linq;
using Xunit;

namespace Jwc.Experiment.Xunit
{
    public class TestFixtureFactoryTypeAttributeTest
    {
        [Fact]
        public void SutIsAttribute()
        {
            var sut = new TestFixtureFactoryTypeAttribute(typeof(DelegatingTestFixtureFactory));
            Assert.IsAssignableFrom<Attribute>(sut);
        }

        [Fact]
        public void SutHasCorrectAttributeUsage()
        {
            var attributeUsage = typeof(TestFixtureFactoryTypeAttribute)
                .GetCustomAttributes(typeof(AttributeUsageAttribute), false)
                .Cast<AttributeUsageAttribute>().Single();
            Assert.False(attributeUsage.AllowMultiple);
            Assert.Equal(AttributeTargets.Assembly, attributeUsage.ValidOn);
        }

        [Fact]
        public void InitializeWithNullTypeThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new TestFixtureFactoryTypeAttribute(null));
        }

        [Fact]
        public void InitializeWithInvalidTypeThrows()
        {
            Assert.Throws<ArgumentException>(() => new TestFixtureFactoryTypeAttribute(typeof(object)));
        }

        [Fact]
        public void TypeIsCorrect()
        {
            var type = typeof(DelegatingTestFixtureFactory);
            var sut = new TestFixtureFactoryTypeAttribute(type);

            var actual = sut.Type;

            Assert.Equal(type, actual);
        }
    }
}
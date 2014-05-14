using System;
using System.Linq;
using Xunit;

namespace Jwc.Experiment
{
    public class TestFixtureDeclarationAttributeTest
    {
        [Fact]
        public void SutIsAttribute()
        {
            var sut = new TestFixtureDeclarationAttribute(typeof(DelegatingTestFixtureFactory));
            Assert.IsAssignableFrom<Attribute>(sut);
        }

        [Fact]
        public void SutHasCorrectAttributeUsage()
        {
            var attributeUsage = typeof(TestFixtureDeclarationAttribute)
                .GetCustomAttributes(typeof(AttributeUsageAttribute), false)
                .Cast<AttributeUsageAttribute>().Single();
            Assert.False(attributeUsage.AllowMultiple);
            Assert.Equal(AttributeTargets.Assembly, attributeUsage.ValidOn);
        }

        [Fact]
        public void InitializeWithNullTypeThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new TestFixtureDeclarationAttribute(null));
        }

        [Fact]
        public void InitializeWithInvalidTypeThrows()
        {
            Assert.Throws<ArgumentException>(() => new TestFixtureDeclarationAttribute(typeof(object)));
        }

        [Fact]
        public void TypeIsCorrect()
        {
            var type = typeof(DelegatingTestFixtureFactory);
            var sut = new TestFixtureDeclarationAttribute(type);

            var actual = sut.Type;

            Assert.Equal(type, actual);
        }
    }
}
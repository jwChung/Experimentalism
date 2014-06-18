using System.Collections.Generic;
using System.Linq;
using Ploeh.Albedo;
using Xunit;

namespace Jwc.Experiment
{
    public class PropertyToParameterComparerTest
    {
        [Fact]
        public void SutIsEqualityComparer()
        {
            var sut = new PropertyToParameterComparer(new DelegatingTestFixture());
            Assert.IsAssignableFrom<IEqualityComparer<IReflectionElement>>(sut);
        }

        [Fact]
        public void TestFixtureIsCorrect()
        {
            var testFixture = new DelegatingTestFixture();
            var sut = new PropertyToParameterComparer(testFixture);

            var actual = sut.TestFixture;

            Assert.Equal(testFixture, actual);
        }

        [Fact]
        public void EqualsPropertyToParameterWithSameValueReturnsTrue()
        {
            var sut = new PropertyToParameterComparer(new FakeTestFixture());
            var propertyInfoElement = new Properties<ClassForPropertyEqualToParameter>()
                .Select(x => x.Value).ToElement();
            var parameterInfoElement = Constructors.Select(() => new ClassForPropertyEqualToParameter(null))
                .GetParameters().First().ToElement();

            var actual = sut.Equals(propertyInfoElement, parameterInfoElement);

            Assert.True(actual);
        }

        [Fact]
        public void EqualsPropertyToParameterWithNotSameValueReturnsFalse()
        {
            var sut = new PropertyToParameterComparer(new FakeTestFixture());
            var propertyInfoElement = new Properties<ClassForPropertyEqualToParameter>()
                .Select(x => x.Other).ToElement();
            var parameterInfoElement = Constructors.Select(() => new ClassForPropertyEqualToParameter(null))
                .GetParameters().First().ToElement();

            var actual = sut.Equals(propertyInfoElement, parameterInfoElement);

            Assert.False(actual);
        }

        [Fact]
        public void GetHashCodeReturnsZero()
        {
            var sut = new PropertyToParameterComparer(new DelegatingTestFixture());
            var actual = sut.GetHashCode(null);
            Assert.Equal(0, actual);
        }

        private class ClassForPropertyEqualToParameter
        {
            private readonly string _value;

            public ClassForPropertyEqualToParameter(string value)
            {
                _value = value;
            }

            public string Value
            {
                get { return _value; }
            }

            public string Other
            {
                get { return null; }
            }
        }
    }
}
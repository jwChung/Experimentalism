namespace Jwc.Experiment
{
    using System.Collections.Generic;
    using System.Linq;
    using Ploeh.Albedo; 
    using Ploeh.AutoFixture;
    using global::Xunit;

    public class PropertyToParameterComparerTest
    {
        [Fact]
        public void SutIsEqualityComparer()
        {
            var sut = new PropertyToParameterComparer(new Fixture());
            Assert.IsAssignableFrom<IEqualityComparer<IReflectionElement>>(sut);
        }

        [Fact]
        public void FixtureIsCorrect()
        {
            var fixture = new Fixture();
            var sut = new PropertyToParameterComparer(fixture);

            var actual = sut.Fixture;

            Assert.Same(fixture, actual);
        }

        [Fact]
        public void EqualsPropertyToParameterWithSameValueReturnsTrue()
        {
            var sut = new PropertyToParameterComparer(new Fixture());
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
            var sut = new PropertyToParameterComparer(new Fixture());
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
            var sut = new PropertyToParameterComparer(new Fixture());
            var actual = sut.GetHashCode(null);
            Assert.Equal(0, actual);
        }

        private class ClassForPropertyEqualToParameter
        {
            private readonly string value;

            public ClassForPropertyEqualToParameter(string value)
            {
                this.value = value;
            }

            public string Value
            {
                get { return this.value; }
            }

            public string Other
            {
                get { return null; }
            }
        }
    }
}
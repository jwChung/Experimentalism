namespace Jwc.Experiment
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Ploeh.Albedo;
    using Ploeh.AutoFixture;
    using global::Xunit;

    public class FieldToParameterComparerTest
    {
        [Fact]
        public void SutIsEqualityComparer()
        {
            var sut = new FieldToParameterComparer(new Fixture());
            Assert.IsAssignableFrom<IEqualityComparer<IReflectionElement>>(sut);
        }

        [Fact]
        public void BuilderIsCorrect()
        {
            var builder = new Fixture();
            var sut = new FieldToParameterComparer(builder);

            var actual = sut.Builder;

            Assert.Same(builder, actual);
        }

        [Fact]
        public void EqualsFieldToParameterWithSameValueReturnsTrue()
        {
            var sut = new FieldToParameterComparer(new Fixture());
            var fieldInfoElement = new Fields<ClassForFieldEqualToParameter>()
                .Select(x => x.Value).ToElement();
            var parameterInfoElement = Constructors.Select(() => new ClassForFieldEqualToParameter(null))
                .GetParameters().First().ToElement();

            var actual = sut.Equals(fieldInfoElement, parameterInfoElement);

            Assert.True(actual);
        }

        [Fact]
        public void EqualsFieldToParameterWithNotSameValueReturnsFalse()
        {
            var sut = new FieldToParameterComparer(new Fixture());
            var fieldInfoElement = new Fields<ClassForFieldEqualToParameter>()
                .Select(x => x.Other).ToElement();
            var parameterInfoElement = Constructors.Select(() => new ClassForFieldEqualToParameter(null))
                .GetParameters().First().ToElement();

            var actual = sut.Equals(fieldInfoElement, parameterInfoElement);

            Assert.False(actual);
        }

        [Fact]
        public void GetHashCodeReturnsZero()
        {
            var sut = new FieldToParameterComparer(new Fixture());
            var actual = sut.GetHashCode(null);
            Assert.Equal(0, actual);
        }

        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "The field is to test.")]
        private class ClassForFieldEqualToParameter
        {
            public string Value;
            public string Other = null;

            public ClassForFieldEqualToParameter(string value)
            {
                this.Value = value;
            }
        }
    }
}
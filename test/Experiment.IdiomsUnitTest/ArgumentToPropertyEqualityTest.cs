using System;
using System.Collections.Generic;
using System.Linq;
using Ploeh.Albedo;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class ArgumentToPropertyEqualityTest
    {
        [Fact]
        public void SutIsEqualityComparer()
        {
            var sut = new ArgumentToPropertyEquality(new DelegatingTestFixture());
            Assert.IsAssignableFrom<IEqualityComparer<IReflectionElement>>(sut);
        }

        [Fact]
        public void TestFixtureIsCorrect()
        {
            var testFixture = new DelegatingTestFixture();
            var sut = new ArgumentToPropertyEquality(testFixture);

            var actual = sut.TestFixture;

            Assert.Equal(testFixture, actual);
        }

        [Fact]
        public void GetHashCodeReturnsAlwaysSameValue()
        {
            var sut = new ArgumentToPropertyEquality(new DelegatingTestFixture());
            var actual = sut.GetHashCode(null);
            Assert.Equal(0, actual);
        }

        [Fact]
        public void EqualsNonParametertToPropertyReturnsFalse()
        {
            var sut = new ArgumentToPropertyEquality(new DelegatingTestFixture());
            var nonParameterInfoElement = GetType().ToElement();
            var propertyInfoElement = new Properties<TypeWithMembers>()
                .Select(x => x.PublicProperty)
                .ToElement();

            var actual = sut.Equals(nonParameterInfoElement, propertyInfoElement);

            Assert.False(actual, "Not Equals.");
        }

        [Fact]
        public void EqualsParameterToNonPropertyReturnsFalse()
        {
            var sut = new ArgumentToPropertyEquality(new DelegatingTestFixture());
            var parameterInfoElement = Constructors.Select(() => new TypeWithMembers(0))
                .GetParameters().First().ToElement();
            var nonPropertyInfoElement = GetType().ToElement();

            var actual = sut.Equals(parameterInfoElement, nonPropertyInfoElement);

            Assert.False(actual, "Not Equals.");
        }

        [Fact]
        public void EqualsParameterToPropetyReturnsTrueWhenTheyHaveEqualValue()
        {
            var testFixture = new DelegatingTestFixture
            {
                OnCreate = x =>
                {
                    Assert.Equal(typeof(int), x);
                    return 123;
                }
            };
            var sut = new ArgumentToPropertyEquality(testFixture);
            var parameterInfoElement = Constructors.Select(() => new TypeForPropertyEqualValue(0))
                .GetParameters().First().ToElement();
            var propetyInfoElement = new Properties<TypeForPropertyEqualValue>()
                .Select(x => x.Value).ToElement();

            var actual = sut.Equals(parameterInfoElement, propetyInfoElement);

            Assert.True(actual, "Equals.");
        }

        [Fact]
        public void EqualsParameterToPropetyReturnsFalseWhenThayRepresentDifferentReflectedTypes()
        {
            var sut = new ArgumentToPropertyEquality(new DelegatingTestFixture());
            var parameterInfoElement = Constructors.Select(() => new TypeForPropertyEqualValue(0))
                .GetParameters().First().ToElement();
            var propetyInfoElement = new Properties<Version>()
                .Select(x => x.Major).ToElement();

            var actual = sut.Equals(parameterInfoElement, propetyInfoElement);

            Assert.False(actual, "Not Equals.");
        }

        [Fact]
        public void EqualsParameterToPropertyReturnsFalseWhenParameterIsFromNonConstructor()
        {
            var sut = new ArgumentToPropertyEquality(new DelegatingTestFixture());
            var parameterInfoElement = new Methods<TypeForPropertyEqualValue>()
                .Select(x => x.Mehtod(null))
                .GetParameters().First().ToElement();
            var propertyInfoElement = new Properties<TypeForPropertyEqualValue>()
                .Select(x => x.Value).ToElement();

            var actual = sut.Equals(parameterInfoElement, propertyInfoElement);

            Assert.False(actual, "Not Equals.");
        }
    
        private class TypeForPropertyEqualValue
        {
            private readonly int _value;

            public TypeForPropertyEqualValue(int value)
            {
                _value = value;
            }

            public object Value
            {
                get
                {
                    return _value;
                }
            }

            public void Mehtod(object arg)
            {
            }
        }
    }
}
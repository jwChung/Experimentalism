using System;
using System.Collections.Generic;
using System.Linq;
using Ploeh.Albedo;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class ParameterToFieldComparerTest
    {
        [Fact]
        public void SutIsEqualityComparer()
        {
            var sut = new ParameterToFieldComparer(new DelegatingTestFixture());
            Assert.IsAssignableFrom<IEqualityComparer<IReflectionElement>>(sut);
        }

        [Fact]
        public void TestFixtureIsCorrect()
        {
            var testFixture = new DelegatingTestFixture();
            var sut = new ParameterToFieldComparer(testFixture);

            var actual = sut.TestFixture;

            Assert.Equal(testFixture, actual);
        }

        [Fact]
        public void InitializeWithNullTestFixtureThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new ParameterToFieldComparer(null));
        }

        [Fact]
        public void GetHashCodeReturnsAlwaysZero()
        {
            var sut = new ParameterToFieldComparer(new DelegatingTestFixture());
            var actual = sut.GetHashCode(null);
            Assert.Equal(0, actual);
        }

        [Fact]
        public void EqualsNonParametertToFieldReturnsFalse()
        {
            var sut = new ParameterToFieldComparer(new DelegatingTestFixture());
            var nonParameterInfoElement = GetType().ToElement();
            var fieldInfoElement = new Fields<ClassWithMembers>()
                .Select(x => x.PublicField)
                .ToElement();

            var actual = sut.Equals(nonParameterInfoElement, fieldInfoElement);

            Assert.False(actual, "Not Equals.");
        }

        [Fact]
        public void EqualsParameterToNonFieldReturnsFalse()
        {
            var sut = new ParameterToFieldComparer(new DelegatingTestFixture());
            var parameterInfoElement = Constructors.Select(() => new ClassWithMembers(0))
                .GetParameters().First().ToElement();
            var nonFieldInfoElement = GetType().ToElement();

            var actual = sut.Equals(parameterInfoElement, nonFieldInfoElement);

            Assert.False(actual, "Not Equals.");
        }

        [Fact]
        public void EqualsParameterToFieldReturnsTrueWhenTheyHaveEqualValue()
        {
            var testFixture = new DelegatingTestFixture
            {
                OnCreate = x =>
                {
                    Assert.Equal(typeof(int), x);
                    return 123;
                }
            };
            var sut = new ParameterToFieldComparer(testFixture);
            var parameterInfoElement = Constructors.Select(() => new TypeForFieldEqualValue(0))
                .GetParameters().First().ToElement();
            var fieldInfoElement = new Fields<TypeForFieldEqualValue>()
                .Select(x => x.Value).ToElement();

            var actual = sut.Equals(parameterInfoElement, fieldInfoElement);

            Assert.True(actual, "Equals.");
        }

        [Fact]
        public void EqualsParameterToFieldReturnsFalseWhenTheyRepresentDifferentReflectedTypes()
        {
            var sut = new ParameterToFieldComparer(new DelegatingTestFixture());
            var parameterInfoElement = Constructors.Select(() => new TypeForFieldEqualValue(0))
                .GetParameters().First().ToElement();
            var fieldInfoElement = new Fields<ClassWithMembers>()
                .Select(x => x.PublicField).ToElement();

            var actual = sut.Equals(parameterInfoElement, fieldInfoElement);

            Assert.False(actual, "Not Equals.");
        }

        [Fact]
        public void EqualsParameterToFieldReturnsFalseWhenParameterIsFromNonConstructor()
        {
            var sut = new ParameterToFieldComparer(new DelegatingTestFixture());
            var parameterInfoElement = new Methods<TypeForFieldEqualValue>()
                .Select(x => x.Mehtod(null))
                .GetParameters().First().ToElement();
            var fieldInfoElement = new Fields<ClassWithMembers>()
                .Select(x => x.PublicField).ToElement();

            var actual = sut.Equals(parameterInfoElement, fieldInfoElement);

            Assert.False(actual, "Not Equals.");
        }

        [Fact]
        public void EqualsParameterToFieldReturnsTrueWhenTheyHaveEqualEnumerable()
        {
            var testFixture = new DelegatingTestFixture
            {
                OnCreate = x =>
                {
                    Assert.Equal(typeof(int[]), x);
                    return new[] { 0, 1, 2, 3, 4 };
                }
            };
            var sut = new ParameterToFieldComparer(testFixture);
            var parameterInfoElement = Constructors.Select(() => new TypeForFieldEqualValue(new int[0]))
                .GetParameters().First().ToElement();
            var fieldInfoElement = new Fields<TypeForFieldEqualValue>()
                .Select(x => x.Values).ToElement();

            var actual = sut.Equals(parameterInfoElement, fieldInfoElement);

            Assert.True(actual, "Equals.");
        }

        private class TypeForFieldEqualValue
        {
            public readonly IEnumerable<int> Values;
#pragma warning disable 649
            private readonly int _value;
#pragma warning restore 649
            public object Value;

            public TypeForFieldEqualValue(int value)
            {
                Value = value;
            }

            public TypeForFieldEqualValue(int[] values)
            {
                Values = values.ToArray();
            }

            public object WritableOnlyProperty
            {
                set
                {
                }
            }

            public object PrivateGetProperty
            {
                private get
                {
                    return _value;
                }
                set
                {
                }
            }

            public void Mehtod(object arg)
            {
            }
        }
    }
}
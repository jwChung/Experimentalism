using System;
using System.Collections.Generic;
using System.Linq;
using Ploeh.Albedo;
using Xunit;

namespace Jwc.Experiment
{
    public class ParameterToPropertyComparerTest
    {
        [Fact]
        public void SutIsEqualityComparer()
        {
            var sut = new ParameterToPropertyComparer(new DelegatingTestFixture());
            Assert.IsAssignableFrom<IEqualityComparer<IReflectionElement>>(sut);
        }

        [Fact]
        public void TestFixtureIsCorrect()
        {
            var testFixture = new DelegatingTestFixture();
            var sut = new ParameterToPropertyComparer(testFixture);

            var actual = sut.TestFixture;

            Assert.Equal(testFixture, actual);
        }

        [Fact]
        public void GetHashCodeReturnsZero()
        {
            var sut = new ParameterToPropertyComparer(new DelegatingTestFixture());
            var actual = sut.GetHashCode(null);
            Assert.Equal(0, actual);
        }

        [Fact]
        public void EqualsNonParametertToPropertyReturnsFalse()
        {
            var sut = new ParameterToPropertyComparer(new DelegatingTestFixture());
            var nonParameterInfoElement = GetType().ToElement();
            var propertyInfoElement = new Properties<ClassWithMembers>()
                .Select(x => x.PublicProperty)
                .ToElement();

            var actual = sut.Equals(nonParameterInfoElement, propertyInfoElement);

            Assert.False(actual, "Not Equals.");
        }

        [Fact]
        public void EqualsParameterToNonPropertyReturnsFalse()
        {
            var sut = new ParameterToPropertyComparer(new DelegatingTestFixture());
            var parameterInfoElement = Constructors.Select(() => new ClassWithMembers(0))
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
            var sut = new ParameterToPropertyComparer(testFixture);
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
            var sut = new ParameterToPropertyComparer(new DelegatingTestFixture());
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
            var sut = new ParameterToPropertyComparer(new DelegatingTestFixture());
            var parameterInfoElement = new Methods<TypeForPropertyEqualValue>()
                .Select(x => x.Mehtod(null))
                .GetParameters().First().ToElement();
            var propertyInfoElement = new Properties<TypeForPropertyEqualValue>()
                .Select(x => x.Value).ToElement();

            var actual = sut.Equals(parameterInfoElement, propertyInfoElement);

            Assert.False(actual, "Not Equals.");
        }

        [Fact]
        public void InitializeWithNullTestFixtureThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new ParameterToPropertyComparer(null));
        }

        [Fact]
        public void EqualsParameterToWritableOnlyPropertyRetrunsFalse()
        {
            var testFixture = new DelegatingTestFixture
            {
                OnCreate = x =>
                {
                    Assert.Equal(typeof(int), x);
                    return 123;
                }
            };
            var sut = new ParameterToPropertyComparer(testFixture);
            var parameterInfoElement = Constructors.Select(() => new TypeForPropertyEqualValue(0))
                .GetParameters().First().ToElement();
            var propetyInfoElement = typeof(TypeForPropertyEqualValue)
                .GetProperty("WritableOnlyProperty").ToElement();

            var actual = sut.Equals(parameterInfoElement, propetyInfoElement);

            Assert.False(actual, "Not Equals.");
        }

        [Fact]
        public void EqualsParameterToPrivateGetPropetyReturnsTrueWhenTheyHaveEqualValue()
        {
            var testFixture = new DelegatingTestFixture
            {
                OnCreate = x =>
                {
                    Assert.Equal(typeof(int), x);
                    return 123;
                }
            };
            var sut = new ParameterToPropertyComparer(testFixture);
            var parameterInfoElement = Constructors.Select(() => new TypeForPropertyEqualValue(0))
                .GetParameters().First().ToElement();
            var propetyInfoElement = typeof(TypeForPropertyEqualValue)
                .GetProperty("PrivateGetProperty").ToElement();

            var actual = sut.Equals(parameterInfoElement, propetyInfoElement);

            Assert.True(actual, "Equals.");
        }

        [Fact]
        public void EqualsParameterToPrivateGetPropetyReturnsTrueWhenTheyHaveEqualEnumerable()
        {
            var testFixture = new DelegatingTestFixture
            {
                OnCreate = x =>
                {
                    Assert.Equal(typeof(int[]), x);
                    return new[] { 1, 2, 3, 4 };
                }
            };
            var sut = new ParameterToPropertyComparer(testFixture);
            var parameterInfoElement = Constructors.Select(() => new TypeForPropertyEqualValue(new int[0]))
                .GetParameters().First().ToElement();
            var propertyInfoElement = new Properties<TypeForPropertyEqualValue>()
                .Select(x => x.Values).ToElement();

            var actual = sut.Equals(parameterInfoElement, propertyInfoElement);

            Assert.True(actual, "Equals.");
        }

        private class TypeForPropertyEqualValue
        {
            private readonly int value;
            private readonly int[] values;

            public TypeForPropertyEqualValue(int value)
            {
                this.value = value;
            }

            public TypeForPropertyEqualValue(int[] values)
            {
                this.values = values.ToArray();
            }

            public IEnumerable<int> Values
            {
                get
                {
                    return this.values;
                }
            }

            public object Value
            {
                get
                {
                    return this.value;
                }
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
                    return this.value;
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
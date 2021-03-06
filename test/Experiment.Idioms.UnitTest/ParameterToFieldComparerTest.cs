﻿namespace Jwc.Experiment
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Ploeh.Albedo;
    using Ploeh.AutoFixture;
    using global::Xunit;

    public class ParameterToFieldComparerTest
    {
        [Fact]
        public void SutIsEqualityComparer()
        {
            var sut = new ParameterToFieldComparer(new Fixture());
            Assert.IsAssignableFrom<IEqualityComparer<IReflectionElement>>(sut);
        }

        [Fact]
        public void BuilderIsCorrect()
        {
            var builder = new Fixture();
            var sut = new ParameterToFieldComparer(builder);

            var actual = sut.Builder;

            Assert.Same(builder, actual);
        }

        [Fact]
        public void InitializeWithNullTestFixtureThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new ParameterToFieldComparer((IFixture)null));
        }

        [Fact]
        public void GetHashCodeReturnsAlwaysZero()
        {
            var sut = new ParameterToFieldComparer(new Fixture());
            var actual = sut.GetHashCode(null);
            Assert.Equal(0, actual);
        }

        [Fact]
        public void EqualsNonParameterToFieldReturnsFalse()
        {
            var sut = new ParameterToFieldComparer(new Fixture());
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
            var sut = new ParameterToFieldComparer(new Fixture());
            var parameterInfoElement = Constructors.Select(() => new ClassWithMembers(0))
                .GetParameters().First().ToElement();
            var nonFieldInfoElement = GetType().ToElement();

            var actual = sut.Equals(parameterInfoElement, nonFieldInfoElement);

            Assert.False(actual, "Not Equals.");
        }

        [Fact]
        public void EqualsParameterToFieldReturnsTrueWhenTheyHaveEqualValue()
        {
            var parameterInfoElement = Constructors.Select(() => new TypeForFieldEqualValue(0))
                .GetParameters().First().ToElement();
            var sut = new ParameterToFieldComparer(new Fixture());
            var fieldInfoElement = new Fields<TypeForFieldEqualValue>()
                .Select(x => x.Value).ToElement();

            var actual = sut.Equals(parameterInfoElement, fieldInfoElement);

            Assert.True(actual, "Equals.");
        }

        [Fact]
        public void EqualsParameterToFieldReturnsFalseWhenTheyRepresentDifferentReflectedTypes()
        {
            var sut = new ParameterToFieldComparer(new Fixture());
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
            var sut = new ParameterToFieldComparer(new Fixture());
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
            var parameterInfoElement = Constructors.Select(() => new TypeForFieldEqualValue(new int[0]))
                .GetParameters().First().ToElement();
            var sut = new ParameterToFieldComparer(new Fixture());
            var fieldInfoElement = new Fields<TypeForFieldEqualValue>()
                .Select(x => x.Values).ToElement();

            var actual = sut.Equals(parameterInfoElement, fieldInfoElement);

            Assert.True(actual, "Equals.");
        }

        [Fact]
        public void EqualsParameterToFieldAlwaysReturnsFalseWhenConstructorThrows()
        {
            var parameterInfoElement = Constructors
                .Select(() => new TypeForFieldEqualValue(default(object)))
                .GetParameters().Single().ToElement();
            var sut = new ParameterToFieldComparer(new Fixture());
            var fieldInfoElement = new Fields<TypeForFieldEqualValue>()
                .Select(x => x.Value).ToElement();

            var actual = sut.Equals(parameterInfoElement, fieldInfoElement);

            Assert.False(actual);
        }

        private class TypeForFieldEqualValue
        {
            public readonly IEnumerable<int> Values;
            public readonly object Value;
#pragma warning disable 649
            private readonly int value;
#pragma warning restore 649

            public TypeForFieldEqualValue(int value)
            {
                this.Value = value;
            }

            public TypeForFieldEqualValue(int[] values)
            {
                this.Values = values.ToArray();
            }

            public TypeForFieldEqualValue(object value)
            {
                throw new NotSupportedException();
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
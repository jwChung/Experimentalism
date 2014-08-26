﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Ploeh.Albedo;
using Xunit;

namespace Jwc.Experiment
{
    public class FieldToParameterComparerTest
    {
        [Fact]
        public void SutIsEqualityComparer()
        {
            var sut = new FieldToParameterComparer(new DelegatingTestFixture());
            Assert.IsAssignableFrom<IEqualityComparer<IReflectionElement>>(sut);
        }

        [Fact]
        public void TestFixtureIsCorrect()
        {
            var testFixture = new DelegatingTestFixture();
            var sut = new FieldToParameterComparer(testFixture);

            var actual = sut.TestFixture;

            Assert.Equal(testFixture, actual);
        }

        [Fact]
        public void EqualsFieldToParameterWithSameValueReturnsTrue()
        {
            var sut = new FieldToParameterComparer(new FakeTestFixture());
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
            var sut = new FieldToParameterComparer(new FakeTestFixture());
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
            var sut = new FieldToParameterComparer(new DelegatingTestFixture());
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
﻿using System.Collections.Generic;
using Xunit;

namespace Jwc.Experiment
{
    public class ConstantEqualityComparerTest
    {
        [Fact]
        public void SutIsEqualityComparer()
        {
            var sut = new ConstantEqualityComparer<object>(false);
            Assert.IsAssignableFrom<IEqualityComparer<object>>(sut);
        }

        [Fact]
        public void ValueIsCorrect()
        {
            bool value = true;
            var sut = new ConstantEqualityComparer<object>(value);

            var actual = sut.Value;

            Assert.Equal(value, actual);
        }

        [Fact]
        public void GetHashCodeAlwaysReturnsZero()
        {
            var sut = new ConstantEqualityComparer<object>(false);
            var actual = sut.GetHashCode(null);
            Assert.Equal(0, actual);
        }

        [Fact]
        public void EqualsReturnsCorrectConstantValue()
        {
            bool value = true;
            var sut = new ConstantEqualityComparer<object>(value);
            var actual = sut.Equals(null, null);
            Assert.Equal(value, actual);
        }
    }
}
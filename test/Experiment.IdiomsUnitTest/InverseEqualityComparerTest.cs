﻿using System;
using System.Collections.Generic;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class InverseEqualityComparerTest
    {
        [Fact]
        public void SutIsEqualityComaprer()
        {
            var sut = new InverseEqualityComparer<object>(
                EqualityComparer<object>.Default);
            Assert.IsAssignableFrom<IEqualityComparer<object>>(sut);
        }

        [Fact]
        public void EqualityComparerIsCorrect()
        {
            var equalityComparer = new DelegatingEqualityComparer<object>();
            var sut = new InverseEqualityComparer<object>(equalityComparer);

            var actual = sut.EqualityComparer;

            Assert.Equal(equalityComparer, actual);
        }

        [Fact]
        public void InitializeWithNullComparerThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new InverseEqualityComparer<object>(null));
        }

        [Fact]
        public void GetHashCodeReturnsHashCodeFromGivenComparer()
        {
            const int hashCode = 123;
            var value = new object();
            var equalityComparer = new DelegatingEqualityComparer<object>
            {
                OnGetHashCode = obj =>
                {
                    Assert.Equal(value, obj);
                    return hashCode;
                }
            };
            var sut = new InverseEqualityComparer<object>(equalityComparer);

            var actual = sut.GetHashCode(value);

            Assert.Equal(hashCode, actual);
        }
    }
}
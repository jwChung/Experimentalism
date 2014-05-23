using System;
using System.Collections.Generic;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class OrEqualityComparerTest
    {
        [Fact]
        public void SutIsEqualityComparer()
        {
            var sut = new OrEqualityComparer<object>();
            Assert.IsAssignableFrom<IEqualityComparer<object>>(sut);
        }

        [Fact]
        public void EqualityComparersIsCorrect()
        {
            var equalityComparers = new IEqualityComparer<object>[]
            {
                new DelegatingEqualityComparer<object>(),
                new DelegatingEqualityComparer<object>(),
                new DelegatingEqualityComparer<object>()
            };
            var sut = new OrEqualityComparer<object>(equalityComparers);

            var actual = sut.EqualityComparers;

            Assert.Equal(equalityComparers, actual);
        }

        [Fact]
        public void InitializeWithNullEqualityComparersThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new OrEqualityComparer<object>(null));
        }

        [Fact]
        public void GetHashCodeAlwaysReturnsZero()
        {
            var sut = new OrEqualityComparer<object>();
            var actual = sut.GetHashCode(null);
            Assert.Equal(0, actual);
        }

        [Fact]
        public void EqualsReturnsFalseWhenAllComparersReturnsFalse()
        {
            // Fixture setup
            var left = new object();
            var right = new object();
            var verify = new List<int>();

            var equalityComparers = new IEqualityComparer<object>[]
            {
                new DelegatingEqualityComparer<object>
                {
                    OnEquals = (x, y) =>
                    {
                        Assert.Equal(left, x);
                        Assert.Equal(right, y);
                        verify.Add(0);
                        return false;
                    }
                },
                new DelegatingEqualityComparer<object>
                {
                    OnEquals = (x, y) =>
                    {
                        Assert.Equal(left, x);
                        Assert.Equal(right, y);
                        verify.Add(1);
                        return false;
                    }
                }
            };

            var sut = new OrEqualityComparer<object>(equalityComparers);

            // Exercise system
            var actual = sut.Equals(left, right);

            // Verify outcome
            Assert.False(actual);
            Assert.Equal(new[] { 0, 1 }, verify);
        }

        [Fact]
        public void EqualsReturnsTrueWhenAnyComparerReturnsTrue()
        {
            // Fixture setup
            var left = new object();
            var right = new object();
            var verify = new List<int>();

            var equalityComparers = new IEqualityComparer<object>[]
            {
                new DelegatingEqualityComparer<object>
                {
                    OnEquals = (x, y) =>
                    {
                        Assert.Equal(left, x);
                        Assert.Equal(right, y);
                        verify.Add(0);
                        return false;
                    }
                },
                new DelegatingEqualityComparer<object>
                {
                    OnEquals = (x, y) =>
                    {
                        Assert.Equal(left, x);
                        Assert.Equal(right, y);
                        verify.Add(1);
                        return true;
                    }
                },
                new DelegatingEqualityComparer<object>
                {
                    OnEquals = (x, y) => { throw new InvalidOperationException(); }
                }
            };

            var sut = new OrEqualityComparer<object>(equalityComparers);

            // Exercise system
            var actual = sut.Equals(left, right);

            // Verify outcome
            Assert.True(actual);
            Assert.Equal(new[] { 0, 1 }, verify);
        }
    }
}
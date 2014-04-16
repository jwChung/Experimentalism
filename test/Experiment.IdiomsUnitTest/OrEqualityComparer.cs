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
    }
}
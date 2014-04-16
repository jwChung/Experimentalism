﻿using System.Collections.Generic;
using Xunit;

namespace Jwc.Experiment.Idioms
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
    }
}
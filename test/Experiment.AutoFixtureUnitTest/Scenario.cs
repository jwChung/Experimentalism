﻿using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;
using Xunit.Extensions;

namespace Jwc.Experiment
{
    public class Scenario
    {
        [Theorem]
        public void TheoremSupportsNonParameterizedTest()
        {
            Assert.True(true, "excuted.");
        }

        [Theorem]
        [InlineData("expected", 1234)]
        [ParameterizedTestData]
        public void TheoremSupportsParameterizedTest(string arg1, int arg2)
        {
            Assert.Equal("expected", arg1);
            Assert.Equal(1234, arg2);
        }

        [Theorem]
        public void TheoremSupportsParameterizedTestWithAutoData(
            string arg1, Type arg2)
        {
            Assert.NotNull(arg1);
            Assert.NotNull(arg2);
        }

        [Theorem]
        [InlineData("expected")]
        public void TheoremSupportsParameterizedTestWithMixedData(
            string arg1, object arg2)
        {
            Assert.Equal("expected", arg1);
            Assert.NotNull(arg2);
        }

        private class ParameterizedTestDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
            {
                yield return new object[] { "expected", 1234 };
            }
        }
    }
}
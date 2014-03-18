using System;
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

        public class ParameterizedTestDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
            {
                yield return new object[] { "expected", 1234 };
            }
        }
    }
}
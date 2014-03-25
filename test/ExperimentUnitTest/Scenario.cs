using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Extensions;

namespace Jwc.Experiment
{
    public class Scenario
    {
        [DefaultTheorem]
        public void NaiveTheoremSupportsNonParameterizedTest()
        {
            Assert.True(true, "excuted.");
        }

        [DefaultTheorem]
        [InlineData("expected", 1234)]
        [ParameterizedTestData]
        public void NaiveTheoremSupportsParameterizedTest(string arg1, int arg2)
        {
            Assert.Equal("expected", arg1);
            Assert.Equal(1234, arg2);
        }

        [DefaultFirstClassTheorem]
        public IEnumerable<ITestCase> NaiveFirstClassTheoremSupportsYieldReturnedTestCases()
        {
            yield return TestCase.New(() => Assert.Equal(3, 2 + 1));

            yield return TestCase.New(
                3, 7, 10,
                (x, y, z) => Assert.Equal(z, x + y));
        }

        [DefaultFirstClassTheorem]
        public ITestCase[] NaiveFirstClassTheoremSupportsArrayTestCases()
        {
            var testCases = new[]
            {
                new { X = 1, Y = 2, Z = 3 },
                new { X = 3, Y = 7, Z = 10 },
                new { X = 100, Y = 23, Z = 123 }
            };

            return testCases.Select(tc =>
                TestCase.New(
                    tc,
                    ptc => Assert.Equal(ptc.Z, ptc.X + ptc.Y)))
                .ToArray();
        }

        [DefaultFirstClassTheorem]
        public IEnumerable<ITestCase> NaiveFirstClassTheoremSupportsTestMethodCases()
        {
            var testCases = new[]
            {
                new { X = "expected", Y = 1234 },
                new { X = "expected", Y = 1234 }
            };

            return testCases.Select(tc =>
                TestCase.New(
                    new Scenario(), tc,
                    (ps, ptc) => ps.NaiveTheoremSupportsParameterizedTest(ptc.X, ptc.Y)));
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
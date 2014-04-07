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
        [BaseTheorem]
        public void DefaultTheoremSupportsNonParameterizedTest()
        {
            Assert.True(true, "excuted.");
        }

        [BaseTheorem]
        [InlineData("expected", 1234)]
        [ParameterizedTestData]
        public void DefaultTheoremSupportsParameterizedTest(string arg1, int arg2)
        {
            Assert.Equal("expected", arg1);
            Assert.Equal(1234, arg2);
        }

        [BaseTheorem(typeof(CustomTestFixture))]
        public void DefaultTheoremWithCustomFixtureSupportsParameterizedTestWithAutoData(
            string arg1, int arg2)
        {
            Assert.Equal("custom string", arg1);
            Assert.Equal(5678, arg2);
        }

        [BaseTheorem(typeof(CustomTestFixture))]
        [InlineData("expected")]
        public void DefaultTheoremWithCustomFixtureSupportsParameterizedTestWithMixedData(
            string arg1, int arg2)
        {
            Assert.Equal("expected", arg1);
            Assert.Equal(5678, arg2);
        }

        [DefaultFirstClassTheorem]
        public IEnumerable<ITestCase> DefaultFirstClassTheoremSupportsFirstClassTestsForYieldReturn()
        {
            yield return new TestCase(() => Assert.Equal(3, 2 + 1));
            yield return new TestCase(() => Assert.Equal(10, 3 + 7));
        }

        [DefaultFirstClassTheorem]
        public ITestCase[] DefaultFirstClassTheoremSupportsFirstClassTestsForArray()
        {
            var testCases = new[]
            {
                new { X = 1, Y = 2, Z = 3 },
                new { X = 3, Y = 7, Z = 10 },
                new { X = 100, Y = 23, Z = 123 }
            };

            return testCases.Select(
                tc => new TestCase(
                    () => Assert.Equal(tc.Z, tc.X + tc.Y)))
                .Cast<ITestCase>()
                .ToArray();
        }

        [DefaultFirstClassTheorem]
        public IEnumerable<ITestCase> DefaultFirstClassTheoremSupportsFirstClassTestsForEnumerable()
        {
            var testCases = new[]
            {
                new { X = "expected", Y = 1234 },
                new { X = "expected", Y = 1234 }
            };

            return testCases.Select(
                tc => new TestCase(
                    () => new Scenario().DefaultTheoremSupportsParameterizedTest(tc.X, tc.Y)));
        }

        [DefaultFirstClassTheorem(typeof(CustomTestFixture))]
        public IEnumerable<ITestCase> DefaultFirstClassTheoremWithCustomFixtureSupportsFirstClassTestsWithAutoData()
        {
            yield return new TestCase<string, int>(
                (x, y) =>
                {
                    Assert.Equal("custom string", x);
                    Assert.Equal(5678, y);
                });
        }

        private class ParameterizedTestDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(
                MethodInfo methodUnderTest, Type[] parameterTypes)
            {
                yield return new object[] { "expected", 1234 };
            }
        }

        private class CustomTestFixture : ITestFixture
        {
            public object Create(object request)
            {
                var type = request as Type;
                if (type != null)
                {
                    if (type == typeof(string))
                    {
                        return "custom string";
                    }
                    if (type == typeof(int))
                    {
                        return 5678;
                    }
                }

                throw new NotSupportedException();
            }
        }
    }
}
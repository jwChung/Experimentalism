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
        [ExamWithCustomFixture]
        public void TheoremSupportsNonParameterizedTest()
        {
            Assert.True(true, "executed.");
        }

        [ExamWithCustomFixture]
        [InlineData("expected", 1234)]
        [ParameterizedTestData]
        public void TheoremSupportsParameterizedTest(string arg1, int arg2)
        {
            Assert.Equal("expected", arg1);
            Assert.Equal(1234, arg2);
        }

        [ExamWithCustomFixture]
        public void TheoremWithCustomFixtureSupportsParameterizedTestWithAutoData(
            string arg1, int arg2)
        {
            Assert.Equal("custom string", arg1);
            Assert.Equal(5678, arg2);
        }

        [ExamWithCustomFixture]
        [InlineData("expected")]
        public void TheoremWithCustomFixtureSupportsParameterizedTestWithMixedData(
            string arg1, int arg2)
        {
            Assert.Equal("expected", arg1);
            Assert.Equal(5678, arg2);
        }

        [FirstClassExamWithCustomFixture]
        public IEnumerable<ITestCase> FirstClassTheoremSupportsTestCasesForYieldReturn()
        {
            yield return new TestCase(() => Assert.Equal(3, 2 + 1));
            yield return new TestCase(() => Assert.Equal(10, 3 + 7));
        }

        [FirstClassExamWithCustomFixture]
        public ITestCase[] FirstClassTheoremSupportsTestCasesForArray()
        {
            var testCases = new[]
            {
                new { X = 1, Y = 2, Z = 3 },
                new { X = 3, Y = 7, Z = 10 },
                new { X = 100, Y = 23, Z = 123 }
            };

            return testCases.Select(c => new TestCase(() => Assert.Equal(c.Z, c.X + c.Y)))
                .Cast<ITestCase>().ToArray();
        }

        [FirstClassExamWithCustomFixture]
        public IEnumerable<ITestCase> FirstClassTheoremSupportsTestCasesForEnumerable()
        {
            var testCases = new[]
            {
                new { X = "expected", Y = 1234 },
                new { X = "expected", Y = 1234 }
            };

            return testCases.Select(
                c => new TestCase(() => new Scenario().TheoremSupportsParameterizedTest(c.X, c.Y)));
        }

        [FirstClassExamWithCustomFixture]
        public IEnumerable<ITestCase> FirstClassTheoremWithCustomFixtureSupportsTestCasesWithAutoData()
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

        private class ExamWithCustomFixtureAttribute : ExamAttribute
        {
            protected override ITestFixture CreateTestFixture(MethodInfo testMethod)
            {
                return new CustomTestFixture();
            }
        }

        private class FirstClassExamWithCustomFixtureAttribute : FirstClassExamAttribute
        {
            protected override ITestFixture CreateTestFixture(MethodInfo testMethod)
            {
                return new CustomTestFixture();
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
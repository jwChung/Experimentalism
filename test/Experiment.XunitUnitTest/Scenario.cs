using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Extensions;

namespace Jwc.Experiment.Xunit
{
    public class Scenario
    {
        [ScenarioTest]
        public void TestAttributeSupportsNonParameterizedTest()
        {
            Assert.True(true, "executed.");
        }

        [ScenarioTest]
        [InlineData("expected", 1234)]
        [ParameterizedTestData]
        public void TestAttributeSupportsParameterizedTest(string arg1, int arg2)
        {
            Assert.Equal("expected", arg1);
            Assert.Equal(1234, arg2);
        }

        [ScenarioTest]
        public void TestAttributeSupportsParameterizedTestWithAutoData(
            string arg1, int arg2)
        {
            Assert.Equal("custom string", arg1);
            Assert.Equal(5678, arg2);
        }

        [ScenarioTest]
        [InlineData("expected")]
        public void TestAttributeSupportsParameterizedTestWithMixedData(
            string arg1, int arg2)
        {
            Assert.Equal("expected", arg1);
            Assert.Equal(5678, arg2);
        }

        [FirstClassScenarioTest]
        public IEnumerable<ITestCase> FirstClassTestAttributeSupportsTestCasesForYieldReturn()
        {
            yield return TestCase.New(() => Assert.Equal(3, 2 + 1));
            yield return TestCase.New(() => Assert.Equal(10, 3 + 7));
        }

        [FirstClassScenarioTest]
        public ITestCase[] FirstClassTestAttributeSupportsTestCasesForArray()
        {
            var testCases = new[]
            {
                new { X = 1, Y = 2, Z = 3 },
                new { X = 3, Y = 7, Z = 10 },
                new { X = 100, Y = 23, Z = 123 }
            };

            return testCases.Select(c => TestCase.New(() => Assert.Equal(c.Z, c.X + c.Y)))
                .Cast<ITestCase>().ToArray();
        }

        [FirstClassScenarioTest]
        public IEnumerable<ITestCase> FirstClassTestAttributeSupportsTestCasesForEnumerable()
        {
            var testCases = new[]
            {
                new { X = "expected", Y = 1234 },
                new { X = "expected", Y = 1234 }
            };

            return testCases.Select(
                c => TestCase.New(() => new Scenario().TestAttributeSupportsParameterizedTest(c.X, c.Y)));
        }

        [FirstClassScenarioTest]
        public IEnumerable<ITestCase> FirstClassTestAttributeSupportsTestCasesWithAutoData()
        {
            yield return new TestCase(
                new Action<string, int>(
                    (x, y) =>
                    {
                        Assert.Equal("custom string", x);
                        Assert.Equal(5678, y);
                    }));
        }

        private class ParameterizedTestDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(
                MethodInfo methodUnderTest, Type[] parameterTypes)
            {
                yield return new object[] { "expected", 1234 };
            }
        }

        private class ScenarioTestAttribute : TestAttribute
        {
            protected override ITestFixture CreateTestFixture(MethodInfo testMethod)
            {
                return new CustomTestFixture();
            }
        }

        private class FirstClassScenarioTestAttribute : FirstClassTestAttribute
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
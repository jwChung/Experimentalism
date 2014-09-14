﻿namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using global::Xunit;
    using global::Xunit.Extensions;

    public class Scenario
    {
        [CustomTest]
        public void TestAttributeSupportsNonParameterizedTest()
        {
            Assert.True(true, "executed.");
        }

        [CustomTest]
        [InlineData("expected", 1234)]
        [ParameterizedTestData]
        public void TestAttributeSupportsParameterizedTest(string arg1, int arg2)
        {
            Assert.Equal("expected", arg1);
            Assert.Equal(1234, arg2);
        }

        [CustomTest]
        public void TestAttributeSupportsParameterizedTestWithAutoData(
            string arg1, int arg2)
        {
            Assert.Equal("custom string", arg1);
            Assert.Equal(5678, arg2);
        }

        [CustomTest]
        [InlineData("expected")]
        public void TestAttributeSupportsParameterizedTestWithMixedData(
            string arg1, int arg2)
        {
            Assert.Equal("expected", arg1);
            Assert.Equal(5678, arg2);
        }

        [CustomTest]
        public IEnumerable<ITestCase> TestBaseAttributeSupportsTestCasesForYieldReturn()
        {
            yield return TestCase.Create(() => Assert.Equal(3, 2 + 1));
            yield return TestCase.Create(() => Assert.Equal(10, 3 + 7));
        }

        [CustomTest]
        public ITestCase[] TestBaseAttributeSupportsTestCasesForArray()
        {
            var testCases = new[]
            {
                new { X = 1, Y = 2, Z = 3 },
                new { X = 3, Y = 7, Z = 10 },
                new { X = 100, Y = 23, Z = 123 }
            };

            return testCases.Select(c => TestCase.Create(() => Assert.Equal(c.Z, c.X + c.Y)))
                .Cast<ITestCase>().ToArray();
        }

        [CustomTest]
        public IEnumerable<ITestCase> TestBaseAttributeSupportsTestCasesForEnumerable()
        {
            var testCases = new[]
            {
                new { X = "expected", Y = 1234 },
                new { X = "expected", Y = 1234 }
            };

            return testCases.Select(
                c => TestCase.Create(() => new Scenario().TestAttributeSupportsParameterizedTest(c.X, c.Y)));
        }

        [CustomTest]
        public IEnumerable<ITestCase> TestBaseAttributeSupportsTestCasesWithAutoData()
        {
            yield return TestCase.WithAuto<string, int>().Create((x, y) =>
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

        private class CustomTestAttribute : TestBaseAttribute
        {
            protected override ITestFixture Create(ITestMethodContext context)
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
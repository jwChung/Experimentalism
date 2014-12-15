namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Ploeh.AutoFixture;
    using global::Xunit;
    using global::Xunit.Extensions;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1204:StaticElementsMustAppearBeforeInstanceElements", Justification = "Semantically to order the test methods.")]
    public class Scenario
    {
        [Attribute]
        public void TestBaseAttributeSupportsNonParameterizedTest()
        {
            Assert.True(true, "executed.");
        }

        [Attribute]
        [InlineData("expected", 1234)]
        [ParameterizedTestData]
        public void TestBaseAttributeSupportsParameterizedTest(string arg1, int arg2)
        {
            Assert.Equal("expected", arg1);
            Assert.Equal(1234, arg2);
        }

        [Attribute]
        public void TestBaseAttributeSupportsParameterizedTestWithAutoData(
            string arg1, int arg2)
        {
            Assert.Equal("custom string", arg1);
            Assert.Equal(5678, arg2);
        }

        [Attribute]
        [InlineData("expected")]
        public void TestBaseAttributeSupportsParameterizedTestWithMixedData(
            string arg1, int arg2)
        {
            Assert.Equal("expected", arg1);
            Assert.Equal(5678, arg2);
        }

        [Attribute]
        public IEnumerable<ITestCase> TestBaseAttributeSupportsTestCasesForYieldReturn()
        {
            yield return TestCase.Create(() => Assert.Equal(3, 2 + 1));
            yield return TestCase.Create(() => Assert.Equal(10, 3 + 7));
        }

        [Attribute]
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

        [Attribute]
        public IEnumerable<ITestCase> TestBaseAttributeSupportsTestCasesForEnumerable()
        {
            var testCases = new[]
            {
                new { X = "expected", Y = 1234 },
                new { X = "expected", Y = 1234 }
            };

            return testCases.Select(
                c => TestCase.Create(() => new Scenario().TestBaseAttributeSupportsParameterizedTest(c.X, c.Y)));
        }

        [Attribute]
        public IEnumerable<ITestCase> TestBaseAttributeSupportsStaticTestCasesWithAutoData()
        {
            yield return TestCase.WithAuto<string, int>().Create((x, y) =>
            {
                Assert.Equal("custom string", x);
                Assert.Equal(5678, y);
            });
        }

        [Attribute]
        public IEnumerable<ITestCase> TestBaseAttributeSupportsInstanceTestCasesWithAutoData()
        {
            var expected = "custom string";
            yield return TestCase.WithAuto<string, int>().Create((x, y) =>
            {
                Assert.Equal(expected, x);
                Assert.Equal(5678, y);
            });
        }

        [Attribute]
        public static void TestBaseAttributeSupportsStaticParameterizedTestWithAutoData(
            string arg1, int arg2)
        {
            Assert.Equal("custom string", arg1);
            Assert.Equal(5678, arg2);
        }

        [Attribute]
        public static IEnumerable<ITestCase> TestBaseAttributeSupportsStaticTestCasesWithAutoDataAdornedWithStaticMethod()
        {
            yield return TestCase.WithAuto<string, int>().Create((x, y) =>
            {
                Assert.Equal("custom string", x);
                Assert.Equal(5678, y);
            });
        }

        [Attribute]
        public static IEnumerable<ITestCase> TestBaseAttributeSupportsInstanceTestCasesWithAutoDataAdornedWithStaticMethod()
        {
            var expected = "custom string";
            yield return TestCase.WithAuto<string, int>().Create((x, y) =>
            {
                Assert.Equal(expected, x);
                Assert.Equal(5678, y);
            });
        }

        [Attribute]
        public IEnumerable<ITestCase> TestBaseAttributePassesAutoDataToMethodOfFirstClassTests(
            IFixture fixture)
        {
            yield return TestCase.Create(() =>
            {
                Assert.NotNull(fixture);
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

        private class Attribute : TestBaseAttribute
        {
            protected override ITestFixture Create(ITestMethodContext context)
            {
                var fixture = new Fixture();
                fixture.Inject("custom string");
                fixture.Inject(5678);
                return new FakeTestFixture(fixture);
            }
        }
    }
}
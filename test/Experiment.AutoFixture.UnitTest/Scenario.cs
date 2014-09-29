namespace Jwc.Experiment.AutoFixture
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Jwc.Experiment.Xunit;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Xunit;
    using global::Xunit;
    using global::Xunit.Extensions;

    public class Scenario
    {
        [Test]
        public void TestAttributeSupportsNonParameterizedTest()
        {
            Assert.True(true, "executed.");
        }

        [Test]
        [InlineData("expected", 1234)]
        [ParameterizedTestData]
        public void TestAttributeSupportsParameterizedTest(string arg1, int arg2)
        {
            Assert.Equal("expected", arg1);
            Assert.Equal(1234, arg2);
        }

        [Test]
        public void TestAttributeSupportsParameterizedTestWithAutoData(
            string arg1,
            Type arg2)
        {
            Assert.NotNull(arg1);
            Assert.NotNull(arg2);
        }

        [Test]
        [InlineData("expected")]
        public void TestAttributeSupportsParameterizedTestWithMixedData(
            string arg1,
            object arg2)
        {
            Assert.Equal("expected", arg1);
            Assert.NotNull(arg2);
        }

        [Test]
        public IEnumerable<ITestCase> TestAttributeSupportsManyTestCases()
        {
            var testCases = new[]
            {
                new { X = 1, Y = 2, Z = 3 },
                new { X = 3, Y = 7, Z = 10 },
                new { X = 100, Y = 23, Z = 123 }
            };
            return TestCases.WithArgs(testCases).Create(
                c => Assert.Equal(c.Z, c.X + c.Y));
        }

        [Test]
        public IEnumerable<ITestCase> TestAttributeWithCustomFixtureSupportsTestCasesWithAutoData()
        {
            yield return TestCase.WithAuto<int>().Create(x => Assert.True(x > 0, "x > 0"));
            yield return TestCase.WithAuto<string>().Create(x => Assert.NotNull(x));
            yield return TestCase.WithAuto<object>().Create(x => Assert.NotNull(x));
        }

        [Test]
        public void TestAttributeCorrectlyCreatesFrozenMockedInstance(
            [Frozen] IDisposable instance)
        {
            Assert.NotNull(instance);
        }

        private class TestAttribute : TestBaseAttribute
        {
            protected override ITestFixture Create(ITestMethodContext context)
            {
                return new TestFixtureFactory().Create(context);
            }
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
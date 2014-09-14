[assembly: Jwc.Experiment.AutoFixture.Scenario.ScenarioFixtureConfiguration]

namespace Jwc.Experiment.AutoFixture
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Jwc.Experiment.Xunit;
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
            string arg1,
            Type arg2)
        {
            Assert.NotNull(arg1);
            Assert.NotNull(arg2);
        }

        [CustomTest]
        [InlineData("expected")]
        public void TestAttributeSupportsParameterizedTestWithMixedData(
            string arg1,
            object arg2)
        {
            Assert.Equal("expected", arg1);
            Assert.NotNull(arg2);
        }

        [CustomTest]
        public void FrozenAttributeFreezesInstanceOfCertainType(
            [Frozen] string arg1,
            string arg2)
        {
            Assert.Same(arg1, arg2);
        }

        [CustomTest]
        public void ModestAttributeUsesModestCtorToConstructInstanceOfCertainType(
            [Modest] Person person)
        {
            Assert.Null(person.Name);
            Assert.Equal(0, person.Age);
        }

        [CustomTest]
        public void GreedyAttributeUsesGreedyCtorToConstructInstanceOfCertainType(
            [Frozen] string name,
            [Frozen] int age,
            [Greedy] Person person)
        {
            Assert.Same(name, person.Name);
            Assert.Equal(age, person.Age);
        }

        [CustomTest]
        public IEnumerable<ITestCase2> FirstClassTestAttributeSupportsManyTestCases()
        {
            var testCases = new[]
            {
                new { X = 1, Y = 2, Z = 3 },
                new { X = 3, Y = 7, Z = 10 },
                new { X = 100, Y = 23, Z = 123 }
            };

            return testCases.Select(
                c => TestCase2.Create(() => Assert.Equal(c.Z, c.X + c.Y)));
        }

        [CustomTest]
        public IEnumerable<ITestCase2> FirstClassTestAttributeWithCustomFixtureSupportsTestCasesWithAutoData()
        {
            yield return TestCase2.WithAuto<int>().Create(x => Assert.True(x > 0, "x > 0"));
            yield return TestCase2.WithAuto<string>().Create(x => Assert.NotNull(x));
            yield return TestCase2.WithAuto<object>().Create(x => Assert.NotNull(x));
        }

        public class ScenarioFixtureConfigurationAttribute : TestAssemblyConfigurationAttribute
        {
            protected override void Setup(Assembly testAssembly)
            {
                DefaultFixtureFactory.SetCurrent(new TestFixtureFactory());
            }
        }

        private class CustomTestAttribute : TestBaseAttribute
        {
            protected override ITestFixture Create(ITestMethodContext context)
            {
                return new TestFixtureFactory().Create(context.ActualMethod);
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
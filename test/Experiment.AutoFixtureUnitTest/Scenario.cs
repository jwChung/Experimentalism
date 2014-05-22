using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jwc.Experiment.AutoFixture;
using Jwc.Experiment.Xunit;
using Xunit;
using Xunit.Extensions;

[assembly: AssemblyFixtureConfig(typeof(Scenario.DefaultFixtureFactoryInitializer))]

namespace Jwc.Experiment.AutoFixture
{
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
            string arg1, Type arg2)
        {
            Assert.NotNull(arg1);
            Assert.NotNull(arg2);
        }

        [Test]
        [InlineData("expected")]
        public void TestAttributeSupportsParameterizedTestWithMixedData(
            string arg1, object arg2)
        {
            Assert.Equal("expected", arg1);
            Assert.NotNull(arg2);
        }

        [Test]
        public void FrozenAttributeFreezesInstanceOfCertainType(
            [Frozen] string arg1, string arg2)
        {
            Assert.Same(arg1, arg2);
        }

        [Test]
        public void ModestAttributeUsesModestCtorToConstructInstanceOfCertainType(
            [Modest] Person person)
        {
            Assert.Null(person.Name);
            Assert.Equal(0, person.Age);
        }

        [Test]
        public void GreedyAttributeUsesGreedyCtorToConstructInstanceOfCertainType(
            [Frozen] string name,
            [Frozen] int age,
            [Greedy] Person person)
        {
            Assert.Same(name, person.Name);
            Assert.Equal(age, person.Age);
        }

        [FirstClassTest]
        public IEnumerable<ITestCase> FirstClassTestAttributeSupportsManyTestCases()
        {
            var testCases = new[]
            {
                new { X = 1, Y = 2, Z = 3 },
                new { X = 3, Y = 7, Z = 10 },
                new { X = 100, Y = 23, Z = 123 }
            };

            return testCases.Select(
                c => new TestCase(() => Assert.Equal(c.Z, c.X + c.Y)));
        }

        [FirstClassTest]
        public IEnumerable<ITestCase> FirstClassTestAttributeWithCustomFixtureSupportsTestCasesWithAutoData()
        {
            yield return new TestCase(new Action<int>(x => Assert.True(x > 0, "x > 0")));
            yield return new TestCase(new Action<string>(x => Assert.NotNull(x)));
            yield return new TestCase(new Action<object>(x => Assert.NotNull(x)));
        }

        private class ParameterizedTestDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
            {
                yield return new object[] { "expected", 1234 };
            }
        }

        internal class DefaultFixtureFactoryInitializer
        {
            public DefaultFixtureFactoryInitializer()
            {
                DefaultFixtureFactory.SetCurrent(new TestFixtureFactory());
            }
        }
    }
}
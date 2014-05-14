using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jwc.Experiment;
using Ploeh.AutoFixture.Xunit;
using Xunit;
using Xunit.Extensions;

namespace NuGet.Jwc.Experiment
{
    public class Scenario
    {
        [Test]
        public void TheoremSupportsNonParameterizedTest()
        {
            Assert.True(true, "executed.");
        }

        [Test]
        [InlineData("expected", 1234)]
        [ParameterizedTestData]
        public void TheoremSupportsParameterizedTest(string arg1, int arg2)
        {
            Assert.Equal("expected", arg1);
            Assert.Equal(1234, arg2);
        }

        [Test]
        public void TheoremSupportsParameterizedTestWithAutoData(
            string arg1, Type arg2)
        {
            Assert.NotNull(arg1);
            Assert.NotNull(arg2);
        }

        [Test]
        [InlineData("expected")]
        public void TheoremSupportsParameterizedTestWithMixedData(
            string arg1, object arg2)
        {
            Assert.Equal("expected", arg1);
            Assert.NotNull(arg2);
        }

        [Test]
        public void TheoremSupportsFrozenAttributeOfAutoFixtureXunit(
            [Frozen] string arg1, string arg2)
        {
            Assert.Same(arg1, arg2);
        }

        [Test]
        public void TheoremSupportsModestAttributeOfAutoFixtureXunit(
            [Modest] Person person)
        {
            Assert.Null(person.Name);
            Assert.Equal(0, person.Age);
        }

        [Test]
        public void TheoremSupportsGreedyAttributeOfAutoFixtureXunit(
            [Frozen] string name,
            [Frozen] int age,
            [Greedy] Person person)
        {
            Assert.Same(name, person.Name);
            Assert.Equal(age, person.Age);
        }

        [FirstClassTest]
        public IEnumerable<ITestCase> FirstClassTheoremSupportsManyTestCases()
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
        public IEnumerable<ITestCase> FirstClassTheoremWithCustomFixtureSupportsTestCasesWithAutoData()
        {
            yield return new TestCase<string>(x => Assert.NotNull(x));
            yield return new TestCase<int>(x => Assert.True(x > 0, "x > 0"));
            yield return new TestCase<object>(x => Assert.NotNull(x));
        }

        private class ParameterizedTestDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
            {
                yield return new object[] { "expected", 1234 };
            }
        }

        public class Person
        {
            private readonly string _name;
            private readonly int _age;

            public Person()
            {
            }

            public Person(string name, int age)
            {
                _name = name;
                _age = age;
            }

            public string Name
            {
                get
                {
                    return _name;
                }
            }

            public int Age
            {
                get
                {
                    return _age;
                }
            }
        }
    }
}
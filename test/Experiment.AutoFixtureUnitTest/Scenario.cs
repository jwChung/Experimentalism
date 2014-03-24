using System;
using System.Collections.Generic;
using System.Reflection;
using Ploeh.AutoFixture.Xunit;
using Xunit;
using Xunit.Extensions;

namespace Jwc.Experiment
{
    public class Scenario
    {
        [Theorem]
        public void TheoremSupportsNonParameterizedTest()
        {
            Assert.True(true, "excuted.");
        }

        [Theorem]
        [InlineData("expected", 1234)]
        [ParameterizedTestData]
        public void TheoremSupportsParameterizedTest(string arg1, int arg2)
        {
            Assert.Equal("expected", arg1);
            Assert.Equal(1234, arg2);
        }

        [Theorem]
        public void TheoremSupportsParameterizedTestWithAutoData(
            string arg1, Type arg2)
        {
            Assert.NotNull(arg1);
            Assert.NotNull(arg2);
        }

        [Theorem]
        [InlineData("expected")]
        public void TheoremSupportsParameterizedTestWithMixedData(
            string arg1, object arg2)
        {
            Assert.Equal("expected", arg1);
            Assert.NotNull(arg2);
        }

        [Theorem]
        public void TeoremSupportsFrozenAttributeOfAutoFixtureXunit(
            [Frozen] string arg1, string arg2)
        {
            Assert.Same(arg1, arg2);
        }

        [Theorem]
        public void TeoremSupportsModestAttributeOfAutoFixtureXunit(
            [Modest] Person person)
        {
            Assert.Null(person.Name);
        }

        [Theorem]
        public void TeoremSupportsGreedyAttributeOfAutoFixtureXunit(
            [Frozen] string name,
            [Frozen] int age,
            [Greedy] Person person)
        {
            Assert.Same(name, person.Name);
            Assert.Equal(age, person.Age);
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
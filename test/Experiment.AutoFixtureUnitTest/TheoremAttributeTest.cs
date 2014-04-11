using System;
using System.Reflection;
using Jwc.Experiment;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit;
using Xunit;

namespace NuGet.Jwc.Experiment
{
    public class TheoremAttributeTest
    {
        [Fact]
        public void SutIsBaseTheoremAttribute()
        {
            var sut = new TestSpecificTheoremAttribute();
            Assert.IsAssignableFrom<BaseTheoremAttribute>(sut);
        }

        [Fact]
        public void CreatTestFixtureWithNullTestMethodThrows()
        {
            var sut = new TestSpecificTheoremAttribute();
            Assert.Throws<ArgumentNullException>(() => sut.CallCreateTestFixture(null));
        }

        [Fact]
        public void CreatTestFixtureReturnsCorrectTestFixture()
        {
            var sut = new TestSpecificTheoremAttribute();
            var dummyMethod = typeof(object).GetMethod("ToString");

            var actual = sut.CallCreateTestFixture(dummyMethod);

            var adapter = Assert.IsType<AutoFixtureAdapter>(actual);
            Assert.IsType<Fixture>(adapter.Fixture);
        }

        [Fact]
        public void CreateTestFixtureAppliesCustomizeAttribute()
        {
            var sut = new TestSpecificTheoremAttribute();
            var actual = sut.CallCreateTestFixture(GetType().GetMethod("FrozenTest"));
            Assert.Same(actual.Create(typeof(string)), actual.Create(typeof(string)));
        }

        [Fact]
        public void CreateTestFixtureAppliesComplexCustomizeAttributes()
        {
            var sut = new TestSpecificTheoremAttribute();

            var actual = sut.CallCreateTestFixture(GetType().GetMethod("PersonTest"));

            var name = (string)actual.Create(typeof(string));
            var age = (int)actual.Create(typeof(int));
            var person = (Person)actual.Create(typeof(Person));
            var other = actual.Create(typeof(object));
            Assert.Same(name, person.Name);
            Assert.Equal(age, person.Age);
            Assert.NotNull(other);
        }

        [Fact]
        public void CreateTestFixtureAppliesManyCustomizeAttributesOnSameParameter()
        {
            var sut = new TestSpecificTheoremAttribute();

            var actual = sut.CallCreateTestFixture(GetType().GetMethod("ManyAttributeTest"));

            var person = (Person)actual.Create(typeof(Person));
            Assert.NotNull(person.Name);
            Assert.NotEqual(0, person.Age);
        }

        [Fact]
        public void CreateFixtureReturnsCorrectFixture()
        {
            var sut = new TestSpecificTheoremAttribute();
            var actual = sut.CallCreateFixture(null);
            Assert.IsType<Fixture>(actual);
        }

        [Fact]
        public void CreateTestFixturePassesCorrectTestMethodToCreateFixture()
        {
            var sut = new TestSpecificTheoremAttribute();
            var testMethod = typeof(object).GetMethod("ToString");

            sut.CallCreateTestFixture(testMethod);

            var actual = sut.TestMethod;
            Assert.Equal(testMethod, actual);
        }

        public void FrozenTest([Frozen] string arg)
        {
        }

        public void PersonTest([Frozen] string name, [Frozen] int age, [Greedy] Person person, object other)
        {
        }

        public void ManyAttributeTest([Greedy][Frozen] Person person)
        {
        }

        private class TestSpecificTheoremAttribute : TheoremAttribute
        {
            public MethodInfo TestMethod
            {
                get;
                set;
            }

            public ITestFixture CallCreateTestFixture(MethodInfo testMethod)
            {
                return base.CreateTestFixture(testMethod);
            }

            public IFixture CallCreateFixture(MethodInfo testMethod)
            {
                return base.CreateFixture(testMethod);
            }

            protected override IFixture CreateFixture(MethodInfo testMethod)
            {
                TestMethod = testMethod;
                return base.CreateFixture(testMethod);
            }
        }
    }
}
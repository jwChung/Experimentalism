using System;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using Ploeh.AutoFixture.Xunit;
using Xunit;

namespace Jwc.Experiment
{
    public class AutoFixtureTheoremAttributeTest
    {
        [Fact]
        public void SutIsBaseTheoremAttribute()
        {
            var sut = new FakeAutoFixtureTheoremAttribute();
            Assert.IsAssignableFrom<BaseTheoremAttribute>(sut);
        }

        [Fact]
        public void CreatTestFixtureWithNullTestMethodThrows()
        {
            var sut = new FakeAutoFixtureTheoremAttribute();
            Assert.Throws<ArgumentNullException>(() => sut.CreateTestFixture(null));
        }

        [Fact]
        public void CreatTestFixtureReturnsCorrectFixture()
        {
            var sut = new FakeAutoFixtureTheoremAttribute();
            var dummyMethod = typeof(object).GetMethod("ToString");

            var actual = sut.CreateTestFixture(dummyMethod);

            var adapter = Assert.IsType<AutoFixtureAdapter>(actual);
            var context = Assert.IsType<SpecimenContext>(adapter.SpecimenContext);
            Assert.IsType<Fixture>(context.Builder);
        }

        [Fact]
        public void CreateTestFixtureAlwaysReturnsNewInstance()
        {
            var sut = new FakeAutoFixtureTheoremAttribute();
            var dummyMethod = typeof(object).GetMethod("ToString");

            var actual = sut.CreateTestFixture(dummyMethod);

            Assert.NotNull(actual);
            Assert.NotSame(sut.CreateTestFixture(dummyMethod), actual);
        }

        [Fact]
        public void CreateTestFixtureAppliesCustomizeAttribute()
        {
            var sut = new FakeAutoFixtureTheoremAttribute();
            var actual = sut.CreateTestFixture(GetType().GetMethod("FrozenTest"));
            Assert.Same(actual.Create(typeof(string)), actual.Create(typeof(string)));
        }

        [Fact]
        public void CreateTestFixtureAppliesComplexCustomizeAttributes()
        {
            var sut = new FakeAutoFixtureTheoremAttribute();

            var actual = sut.CreateTestFixture(GetType().GetMethod("PersonTest"));

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
            var sut = new FakeAutoFixtureTheoremAttribute();

            var actual = sut.CreateTestFixture(GetType().GetMethod("ManyAttributeTest"));

            var person = (Person)actual.Create(typeof(Person));
            Assert.NotNull(person.Name);
            Assert.NotEqual(0, person.Age);
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

        private class FakeAutoFixtureTheoremAttribute : AutoFixtureTheoremAttribute
        {
            protected override IFixture CreateFixture()
            {
                return new Fixture();
            }
        }
    }
}
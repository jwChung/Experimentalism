using System.Reflection;
using Jwc.Experiment;
using Ploeh.AutoFixture.Xunit;
using Xunit;

namespace NuGet.Jwc.Experiment
{
    public class AutoFixtureFactoryTest
    {
        [Fact]
        public void SutIsTestFixtureFactory()
        {
            var sut = new AutoFixtureFactory();
            Assert.IsAssignableFrom<ITestFixtureFactory>(sut);
        }

        [Fact]
        public void CreateReturnCorrectTestFixture()
        {
            var sut = new AutoFixtureFactory();
            var actual = sut.Create((MethodInfo)MethodBase.GetCurrentMethod());
            Assert.IsAssignableFrom<AutoFixture>(actual);
        }

        [Fact]
        public void CreateAppliesCustomizeAttribute()
        {
            var sut = new AutoFixtureFactory();
            var actual = sut.Create(GetType().GetMethod("FrozenTest"));
            Assert.Same(actual.Create(typeof(string)), actual.Create(typeof(string)));
        }

        [Fact]
        public void CreateAppliesComplexCustomizeAttributes()
        {
            var sut = new AutoFixtureFactory();

            var actual = sut.Create(GetType().GetMethod("PersonTest"));

            var name = (string)actual.Create(typeof(string));
            var age = (int)actual.Create(typeof(int));
            var person = (Person)actual.Create(typeof(Person));
            var other = actual.Create(typeof(object));
            Assert.Same(name, person.Name);
            Assert.Equal(age, person.Age);
            Assert.NotNull(other);
        }

        [Fact]
        public void CreateAppliesManyCustomizeAttributesOnSameParameter()
        {
            var sut = new AutoFixtureFactory();

            var actual = sut.Create(GetType().GetMethod("ManyAttributeTest"));

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
    }
}
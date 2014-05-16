using System;
using System.Reflection;
using Jwc.Experiment;
using Ploeh.AutoFixture;
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

        [Fact]
        public void CreateWithNullTestMethodThrows()
        {
            var sut = new AutoFixtureFactory();
            Assert.Throws<ArgumentNullException>(() => sut.Create(null));
        }

        [Fact]
        public void CreateCorrectlyUsesCreateFixtureMethod()
        {
            var expected = new Fixture();
            var testMethod = (MethodInfo)MethodBase.GetCurrentMethod();
            var sut = new DelegatingAutoFixtureFactory
            {
                OnCreateFixture = m =>
                {
                    Assert.Equal(testMethod, m);
                    return expected;
                }
            };

            var actual = sut.Create(testMethod);

            Assert.Same(expected, ((AutoFixture)actual).Fixture);
        }

        [Fact]
        public void CreateAppliesCustomizeAttributeToCustomizedFixture()
        {
            var fixture = new Fixture();
            var sut = new DelegatingAutoFixtureFactory { OnCreateFixture = m => fixture };

            sut.Create(GetType().GetMethod("FrozenTest"));

            Assert.Same(fixture.Create<string>(), fixture.Create<string>());
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
        
        private class DelegatingAutoFixtureFactory : AutoFixtureFactory
        {
            public Func<MethodInfo, IFixture> OnCreateFixture
            {
                get;
                set;
            }

            protected override IFixture CreateFixture(MethodInfo testMethod)
            {
                return OnCreateFixture(testMethod);
            }
        }
    }
}
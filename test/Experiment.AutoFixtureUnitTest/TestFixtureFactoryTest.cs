using System;
using System.Reflection;
using Ploeh.AutoFixture;
using Xunit;

namespace Jwc.Experiment.AutoFixture
{
    public class TestFixtureFactoryTest
    {
        [Fact]
        public void SutIsTestFixtureFactory()
        {
            var sut = new TestFixtureFactory();
            Assert.IsAssignableFrom<ITestFixtureFactory>(sut);
        }

        [Fact]
        public void CreateReturnCorrectTestFixture()
        {
            var sut = new TestFixtureFactory();
            var actual = sut.Create((MethodInfo)MethodBase.GetCurrentMethod());
            Assert.IsAssignableFrom<TestFixture>(actual);
        }

        [Fact]
        public void CreateAppliesCustomizeAttribute()
        {
            var sut = new TestFixtureFactory();
            var actual = sut.Create(GetType().GetMethod("FrozenTest"));
            Assert.Same(actual.Create(typeof(string)), actual.Create(typeof(string)));
        }

        [Fact]
        public void CreateAppliesComplexCustomizeAttributes()
        {
            var sut = new TestFixtureFactory();

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
            var sut = new TestFixtureFactory();

            var actual = sut.Create(GetType().GetMethod("ManyAttributeTest"));

            var person = (Person)actual.Create(typeof(Person));
            Assert.NotNull(person.Name);
            Assert.NotEqual(0, person.Age);
        }

        [Fact]
        public void CreateWithNullTestMethodThrows()
        {
            var sut = new TestFixtureFactory();
            Assert.Throws<ArgumentNullException>(() => sut.Create(null));
        }

        [Fact]
        public void CreateCorrectlyUsesCreateFixtureMethod()
        {
            var expected = new Fixture();
            var testMethod = (MethodInfo)MethodBase.GetCurrentMethod();
            var sut = new DelegatingTestFixtureFactory
            {
                OnCreateFixture = m =>
                {
                    Assert.Equal(testMethod, m);
                    return expected;
                }
            };

            var actual = sut.Create(testMethod);

            Assert.Same(expected, ((TestFixture)actual).Fixture);
        }

        [Fact]
        public void CreateCorrectlyAppliesCustomizeAttribute()
        {
            var fixture = new Fixture();
            var sut = new DelegatingTestFixtureFactory { OnCreateFixture = m => fixture };

            sut.Create(GetType().GetMethod("FrozenTest"));

            Assert.Same(fixture.Create<string>(), fixture.Create<string>());
        }

        [Fact]
        public void CreateReturnsFixtureOmittingAutoProperties()
        {
            var sut = new TestFixtureFactory();

            var actual = sut.Create((MethodInfo)MethodBase.GetCurrentMethod());

            var testFixture = Assert.IsAssignableFrom<TestFixture>(actual);
            Assert.True(testFixture.Fixture.OmitAutoProperties);
        }

        [Fact]
        public void CreateCanReturnFixtureAllowingAutoProperties()
        {
            var sut = new DelegatingTestFixtureFactory { OnCreateFixture = m => new Fixture() };

            var actual = sut.Create((MethodInfo)MethodBase.GetCurrentMethod());

            var testFixture = Assert.IsAssignableFrom<TestFixture>(actual);
            Assert.False(testFixture.Fixture.OmitAutoProperties);
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
        
        private class DelegatingTestFixtureFactory : TestFixtureFactory
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
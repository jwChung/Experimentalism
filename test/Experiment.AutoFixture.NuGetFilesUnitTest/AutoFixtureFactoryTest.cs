using System;
using System.Reflection;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using Ploeh.AutoFixture.Xunit;
using Xunit;
using Xunit.Extensions;

namespace Jwc.Experiment
{
    public class AutoFixtureFactoryTest
    {
        [Fact]
        public void SutIsTestFixtureFactory()
        {
            var sut = AutoFixtureFactory.Instance;
            Assert.IsAssignableFrom<ITestFixtureFactory>(sut);
        }

        [Fact]
        public void CreateWithNullMethodThrows()
        {
            var sut = AutoFixtureFactory.Instance;
            Assert.Throws<ArgumentNullException>(() => sut.Create(null));
        }

        [Fact]
        public void CreateReturnsCorrectFixture()
        {
            var sut = AutoFixtureFactory.Instance;
            var dummyMethod = typeof(object).GetMethod("ToString");

            var actual = sut.Create(dummyMethod);

            var adapter = Assert.IsType<AutoFixtureAdapter>(actual);
            var context = Assert.IsType<SpecimenContext>(adapter.SpecimenContext);
            Assert.IsType<Fixture>(context.Builder);
        }

        [Fact]
        public void CreateAlwaysReturnsNewInstance()
        {
            var sut = AutoFixtureFactory.Instance;
            var dummyMethod = typeof(object).GetMethod("ToString");

            var actual = sut.Create(dummyMethod);

            Assert.NotNull(actual);
            Assert.NotSame(sut.Create(dummyMethod), actual);
        }

        [Fact]
        public void CreateAppliesCustomizeAttribute()
        {
            var sut = AutoFixtureFactory.Instance;
            var actual = sut.Create(GetType().GetMethod("FrozenTest"));
            Assert.Same(actual.Create(typeof(string)), actual.Create(typeof(string)));
        }

        [Fact]
        public void CreateAppliesComplexCustomizeAttributes()
        {
            var sut = AutoFixtureFactory.Instance;

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
            var sut = AutoFixtureFactory.Instance;

            var actual = sut.Create(GetType().GetMethod("ManyAttributeTest"));

            var person = (Person)actual.Create(typeof(Person));
            Assert.NotNull(person.Name);
            Assert.NotEqual(0, person.Age);
        }

        [Fact]
        public void SutIsSingleton()
        {
            var sut = AutoFixtureFactory.Instance;
            Assert.IsType<AutoFixtureFactory>(sut);
            Assert.Same(AutoFixtureFactory.Instance, sut);
        }

        [Theory]
        [InlineData("InvalidParameterTypeTest")]
        [InlineData("InvalidReturnTypeTest")]
        public void CreateWithInvalidCustomizeAttributeDoesNotThrows(string testMethodName)
        {
            var sut = AutoFixtureFactory.Instance;
            var testMethod = GetType().GetMethod(testMethodName);
            Assert.DoesNotThrow(() => sut.Create(testMethod));
        }

        [Fact]
        public void CreateWithAssignablReturnTypeCustomizeAttributeAppliesCustomizeAttribute()
        {
            var sut = AutoFixtureFactory.Instance;
            var actual = sut.Create(GetType().GetMethod("AssignableReturnTypeTest"));
            Assert.Same(actual.Create(typeof(string)), actual.Create(typeof(string)));
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

        public void InvalidParameterTypeTest([InvalidParameterType] object value)
        {
        }

        public void InvalidReturnTypeTest([InvalidReturnType] object value)
        {
        }

        public void AssignableReturnTypeTest([AssignableReturnType] string value)
        {
        }

        public class InvalidParameterTypeAttribute : Attribute
        {
            public ICustomization GetCustomization(string invalidType)
            {
                return null;
            }
        }

        public class InvalidReturnTypeAttribute : Attribute
        {
            public object GetCustomization(ParameterInfo parameter)
            {
                return null;
            }
        }

        public class AssignableReturnTypeAttribute : Attribute
        {
            public FreezingCustomization GetCustomization(ParameterInfo parameter)
            {
                return new FreezingCustomization(parameter.ParameterType);
            }
        }
    }
}
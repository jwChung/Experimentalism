using System;
using System.Reflection;
using Jwc.NuGetFiles;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using Ploeh.AutoFixture.Xunit;
using Xunit;
using Xunit.Extensions;

namespace Jwc.Experiment
{
    public class TheoremAttributeTest
    {
        [Fact]
        public void SutIsBaseTheoremAttribute()
        {
            var sut = new TheoremAttribute();
            Assert.IsAssignableFrom<BaseTheoremAttribute>(sut);
        }

        [Fact]
        public void CreatTestFixtureWithNullTestMethodThrows()
        {
            var sut = new TheoremAttribute();
            Assert.Throws<ArgumentNullException>(() => sut.CreateTestFixture(null));
        }

        [Fact]
        public void CreatTestFixtureReturnsCorrectFixture()
        {
            var sut = new TheoremAttribute();
            var dummyMethod = typeof(object).GetMethod("ToString");

            var actual = sut.CreateTestFixture(dummyMethod);

            var adapter = Assert.IsType<AutoFixtureAdapter>(actual);
            var context = Assert.IsType<SpecimenContext>(adapter.SpecimenContext);
            Assert.IsType<Fixture>(context.Builder);
        }

        [Fact]
        public void CreateTestFixtureAlwaysReturnsNewInstance()
        {
            var sut = new TheoremAttribute();
            var dummyMethod = typeof(object).GetMethod("ToString");

            var actual = sut.CreateTestFixture(dummyMethod);

            Assert.NotNull(actual);
            Assert.NotSame(sut.CreateTestFixture(dummyMethod), actual);
        }

        [Fact]
        public void CreateTestFixtureAppliesCustomizeAttribute()
        {
            var sut = new TheoremAttribute();
            var actual = sut.CreateTestFixture(GetType().GetMethod("FrozenTest"));
            Assert.Same(actual.Create(typeof(string)), actual.Create(typeof(string)));
        }

        [Fact]
        public void CreateTestFixtureAppliesComplexCustomizeAttributes()
        {
            var sut = new TheoremAttribute();

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
            var sut = new TheoremAttribute();

            var actual = sut.CreateTestFixture(GetType().GetMethod("ManyAttributeTest"));

            var person = (Person)actual.Create(typeof(Person));
            Assert.NotNull(person.Name);
            Assert.NotEqual(0, person.Age);
        }

        [Theory]
        [InlineData("InvalidParameterTypeTest")]
        [InlineData("InvalidReturnTypeTest")]
        public void CreateTestFixtureDoesNotThrowsAlthoughGetCustomizationHasInvalidSignature(
            string testMethodName)
        {
            var sut = new TheoremAttribute();
            var testMethod = GetType().GetMethod(testMethodName);
            Assert.DoesNotThrow(() => sut.CreateTestFixture(testMethod));
        }

        [Fact]
        public void CreateTestFixtureAppliesCustomizeAttributeIfGetCustomizationHasAssignablReturnType()
        {
            var sut = new TheoremAttribute();
            var actual = sut.CreateTestFixture(GetType().GetMethod("AssignableReturnTypeTest"));
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
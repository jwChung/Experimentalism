using System.Linq;
using System.Reflection;
using Ploeh.Albedo;
using Ploeh.Albedo.Refraction;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class GuardClauseAssertionTestCasesTest
    {
        [Fact]
        public void SutIsIdiomaticTestCases()
        {
            var sut = new GuardClauseAssertionTestCases(GetType());
            Assert.IsAssignableFrom<IdiomaticTestCases>(sut);
        }

        [Fact]
        public void ReflectionElementsHasCorrectTargetMembers()
        {
            var type = GetType();
            var sut = new GuardClauseAssertionTestCases(type);

            var actual = sut.ReflectionElements;

            var reflectionElements = Assert.IsAssignableFrom<ReflectionElements>(actual);
            var filteringMembers1 = Assert.IsAssignableFrom<FilteringMembers>(reflectionElements.Sources);
            var filteringMembers2 = Assert.IsAssignableFrom<FilteringMembers>(filteringMembers1.TargetMembers);
            var targetMembers = Assert.IsAssignableFrom<TargetMembers>(filteringMembers2.TargetMembers);
            Assert.Equal(type, targetMembers.Type);
            Assert.Equal(Accessibilities.Public, targetMembers.Accessibilities);
        }

        [Fact]
        public void ReflectionElementsHasFilterToExceptCertainMembers()
        {
            var type = typeof(ClassWithTestMembers);
            var exceptedMembers = type.GetMethods().Cast<MemberInfo>().ToArray();
            var sut = new GuardClauseAssertionTestCases(type, exceptedMembers);

            var actual = sut.ReflectionElements;

            var reflectionElements = Assert.IsAssignableFrom<ReflectionElements>(actual);
            var filteringMembers = Assert.IsAssignableFrom<FilteringMembers>(reflectionElements.Sources);
            Assert.True(filteringMembers.All(m => !(m is MethodInfo)), "Except Condition.");
        }

        [Fact]
        public void ReflectionElementsHasFilterToExceptPropertiesNotHavingSetter()
        {
            var type = typeof(ClassWithProperties);
            var sut = new GuardClauseAssertionTestCases(type);
            var expected = new[]
            {
                new Properties<ClassWithProperties>().Select(x => x.GetSetProperty),
                typeof(ClassWithProperties).GetProperty("SetProperty")
            };

            var actual = sut.ReflectionElements;

            var reflectionElements = Assert.IsAssignableFrom<ReflectionElements>(actual);
            var filteringMembers = Assert.IsAssignableFrom<FilteringMembers>(reflectionElements.Sources);
            Assert.Equal(expected, filteringMembers.OfType<PropertyInfo>());
        }

        [Fact]
        public void ReflectionElementsHasCorrectRefractions()
        {
            var sut = new GuardClauseAssertionTestCases(GetType());

            var actual = sut.ReflectionElements;

            var reflectionElements = Assert.IsAssignableFrom<ReflectionElements>(actual);
            Assert.Equal(
                new[]
                {
                    typeof(ConstructorInfoElementRefraction<object>),
                    typeof(PropertyInfoElementRefraction<object>),
                    typeof(MethodInfoElementRefraction<object>)
                }
                .OrderBy(t => t.FullName),
                reflectionElements.Refractions.Select(r => r.GetType()).OrderBy(t => t.FullName));
        }

        [Fact]
        public void AssertionFactoryIsCorrect()
        {
            var sut = new GuardClauseAssertionTestCases(GetType());
            var actual = sut.AssertionFactory;
            Assert.IsType<GuardClauseAssertionFactory>(actual);
        }

        private class ClassWithProperties
        {
            public object GetSetProperty
            {
                get;
                set;
            }

            public object GetProperty
            {
                get
                {
                    return new object();
                }
            }

            public object SetProperty
            {
                set
                {
                }
            }

            public object PrivateSetProperty
            {
                get
                {
                    return new object();
                }
                private set
                {
                }
            }
        }
    }
}
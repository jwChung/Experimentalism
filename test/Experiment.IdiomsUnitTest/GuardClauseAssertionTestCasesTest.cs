using System;
using System.Linq;
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
        public void ReflectionElementsHasCorrectSources()
        {
            var type = GetType();
            var exceptedMembers = type.GetMembers();
            var sut = new GuardClauseAssertionTestCases(type, exceptedMembers);

            var actual = sut.ReflectionElements;

            var reflectionElements = Assert.IsAssignableFrom<ReflectionElements>(actual);
            var fileterMembers = Assert.IsAssignableFrom<FilteringMembers>(reflectionElements.Sources);
            var targetMembers = Assert.IsAssignableFrom<TargetMembers>(fileterMembers.TargetMembers);
            Assert.Equal(type, targetMembers.Type);
            Assert.Equal(Accessibilities.Public, targetMembers.Accessibilities);
            Assert.Equal(exceptedMembers, fileterMembers.ExceptedMembers);
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
    }
}
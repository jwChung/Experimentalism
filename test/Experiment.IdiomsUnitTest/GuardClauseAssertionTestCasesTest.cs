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
        public void ReflectionElementsIsCorrect()
        {
            // Fixture setup
            var type = GetType();
            var exceptedMembers = type.GetMembers();
            var sut = new GuardClauseAssertionTestCases(type, exceptedMembers);

            // Exercise system
            var actual = sut.ReflectionElements;

            // Verify outcome
            var reflectionElements = Assert.IsAssignableFrom<ReflectionElements>(actual);
            var fileterMembers = Assert.IsAssignableFrom<FilteringMembers>(reflectionElements.Sources);
            var defaultMembers = Assert.IsAssignableFrom<DefaultMembers>(fileterMembers.TargetMembers);
            Assert.Equal(type, defaultMembers.Type);
            Assert.Equal(exceptedMembers, fileterMembers.ExceptedMembers);

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
    }
}
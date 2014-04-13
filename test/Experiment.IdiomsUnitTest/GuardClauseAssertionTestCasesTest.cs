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
        public void ReflectionElementsHasCorrectRefractionSources()
        {
            // Fixture setup
            var type = GetType();
            var excludedMembers = typeof(object).GetMethods().Cast<MemberInfo>().ToArray();
            var sut = new GuardClauseAssertionTestCases(type, excludedMembers);

            // Excercise system
            var actual = sut.ReflectionElements;

            // Verify outcome
            var reflectionElements = Assert.IsAssignableFrom<ReflectionElements>(actual);
            var excludingReadOnlyProperties = Assert.IsAssignableFrom<ExcludingReadOnlyProperties>(
                reflectionElements.Sources);
            var excludingMembers = Assert.IsAssignableFrom<ExcludingMembers>(
                excludingReadOnlyProperties.TargetMembers);

            var targetMembers = Assert.IsAssignableFrom<TargetMembers>(excludingMembers.TargetMembers);
            Assert.Equal(type, targetMembers.Type);
            Assert.Equal(Accessibilities.Public, targetMembers.Accessibilities);

            Assert.Equal(excludedMembers, excludingMembers.ExcludedMembers);
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
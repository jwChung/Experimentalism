using System.Linq;
using System.Reflection;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class AssemblyLevelTest
    {
        [Fact]
        public void SutReferencesOnlySpecifiedAssemblies()
        {
            var sut = typeof(IdiomaticTestCase).Assembly;
            var specifiedAssemblies = new []
            {
                // GAC
                "mscorlib",
                "System.Core",

                // Direct references
                "Jwc.Experiment",
                "Ploeh.Albedo",
                "xunit"

                // Indirect references
            };

            var actual = sut.GetActualReferencedAssemblies();

            Assert.Equal(specifiedAssemblies.OrderBy(x => x), actual.OrderBy(x => x));
        }

        //[Theory]
        //[InlineData("Jwc.Experiment")]
        public void SutDoesNotExposeAnyTypesOfSpecifiedReference(string name)
        {
            // Fixture setup
            var sut = typeof(IdiomaticTestCase).Assembly;
            var assemblyName = sut.GetActualReferencedAssemblies().Single(n => n == name);
            var types = Assembly.Load(assemblyName).GetExportedTypes();

            // Exercise system and Verify outcome
            sut.VerifyDoesNotExpose(types);
        }
    }
}
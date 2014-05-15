using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Extensions;

namespace Jwc.Experiment.Xunit
{
    public class AssemblyLevelTest
    {
        [Fact]
        public void SutReferencesOnlySpecifiedAssemblies()
        {
            var sut = typeof(ExamAttribute).Assembly;
            var specifiedAssemblies = new []
            {
                // GAC
                "mscorlib",
                "System.Core",

                // Direct references
                "xunit",
                "Jwc.Experiment",

                // Indirect references
                "xunit.extensions"
            };

            var actual = sut.GetActualReferencedAssemblies();

            Assert.Equal(specifiedAssemblies.OrderBy(x => x), actual.OrderBy(x => x));
        }

        [Theory]
        [InlineData("xunit.extensions")]
        public void SutDoesNotExposeAnyTypesOfSpecifiedReference(string name)
        {
            // Fixture setup
            var sut = typeof(ExamAttribute).Assembly;
            var assemblyName = sut.GetActualReferencedAssemblies().Single(n => n == name);
            var types = Assembly.Load(assemblyName).GetExportedTypes();

            // Exercise system and Verify outcome
            sut.VerifyDoesNotExpose(types);
        }
    }
}
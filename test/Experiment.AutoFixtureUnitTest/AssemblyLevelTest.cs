using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Extensions;

namespace Jwc.Experiment.AutoFixture
{
    public class AssemblyLevelTest
    {
        [Fact]
        public void SutReferencesOnlySpecifiedAssemblies()
        {
            var sut = typeof(AutoFixture).Assembly;
            var specifiedAssemblies = new []
            {
                // GAC
                "mscorlib",
                "System.Core",

                // Direct references
                "Jwc.Experiment",
                "Ploeh.AutoFixture",

                // Indirect references
                "Ploeh.AutoFixture.Xunit"
            };

            var actual = sut.GetActualReferencedAssemblies();

            Assert.Equal(specifiedAssemblies.OrderBy(x => x), actual.OrderBy(x => x));
        }

        [Theory]
        [InlineData("Ploeh.AutoFixture.Xunit")]
        public void SutDoesNotExposeAnyTypesOfSpecifiedReference(string name)
        {
            // Fixture setup
            var sut = typeof(AutoFixture).Assembly;
            var assemblyName = sut.GetActualReferencedAssemblies().Single(n => n == name);
            var types = Assembly.Load(assemblyName).GetExportedTypes();

            // Exercise system and Verify outcome
            sut.VerifyDoesNotExpose(types);
        }
    }
}
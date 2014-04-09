using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Extensions;

namespace Jwc.Experiment
{
    public class AssemblyLevelTest
    {
        [Fact]
        public void SutReferencesOnlySpecifiedAssemblies()
        {
            var sut = typeof(AutoFixtureAdapter).Assembly;
            var specifiedAssemblies = new []
            {
                "mscorlib",
                "System.Core",
                "Jwc.Experiment",
                "Ploeh.AutoFixture",
                "Ploeh.AutoFixture.Xunit",
                "xunit"
            };

            var actual = sut.GetActualReferencedAssemblies();

            Assert.Equal(specifiedAssemblies.Length, actual.Length);
            Assert.False(specifiedAssemblies.Except(actual).Any(), "Assemblies are not same.");
        }

        [Theory]
        [InlineData("Ploeh.AutoFixture.Xunit")]
        public void SutDoesNotExposeAnyTypeOfSpecifiedReference(string name)
        {
            // Fixture setup
            var sut = typeof(AutoFixtureAdapter).Assembly;
            var assemblyName = sut.GetActualReferencedAssemblies().Single(n => n == name);
            var typesNotExposed = Assembly.Load(assemblyName).GetExportedTypes();

            // Exercise system and Verify outcome
            sut.VerifyDoesNotExpose(typesNotExposed);
        }
    }
}
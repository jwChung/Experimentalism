using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Jwc.Experiment;
using Xunit;
using Xunit.Extensions;

namespace NuGet.Jwc.Experiment
{
    public class AssemblyLevelTest
    {
        const string _productDirectory = @"..\..\..\..\src\Experiment.AutoFixture\";
        const string _testDirectory = @"..\..\..\..\test\Experiment.AutoFixtureUnitTest\";

        [Fact]
        public void SutReferencesOnlySpecifiedAssemblies()
        {
            var sut = typeof(AutoFixtureAdapter).Assembly;
            var specifiedAssemblies = new []
            {
                // GAC
                "mscorlib",
                "System.Core",

                // Direct references
                "xunit",
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
            var sut = typeof(AutoFixtureAdapter).Assembly;
            var assemblyName = sut.GetActualReferencedAssemblies().Single(n => n == name);
            var types = Assembly.Load(assemblyName).GetExportedTypes();

            // Exercise system and Verify outcome
            sut.VerifyDoesNotExpose(types);
        }

        [Theory]
        [InlineData(_productDirectory, "TheoremAttribute")]
        [InlineData(_productDirectory, "FirstClassTheoremAttribute")]
        [InlineData(_testDirectory, "Scenario")]
        public void SutCorrectlyGeneratesNugetTransformFiles(string directory, string originName)
        {
            var origin = directory + originName + ".cs";
            var destination = directory + originName + ".cs.pp";
            Assert.True(File.Exists(origin), "exists.");
            VerifyGeneratingFile(origin, destination);
        }

        [Conditional("CI")]
        private static void VerifyGeneratingFile(string origin, string destination)
        {
            var content = File.ReadAllText(origin, Encoding.UTF8)
                .Replace("namespace NuGet.Jwc.Experiment", "namespace $rootnamespace$");
            File.WriteAllText(destination, content, Encoding.UTF8);
            Assert.True(File.Exists(destination), "exists.");
        }
    }
}
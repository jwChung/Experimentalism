using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Jwc.NuGetFiles;
using Xunit;
using Xunit.Extensions;

namespace Jwc.Experiment
{
    public class AssemblyLevelTest
    {
        [Fact]
        public void TargetAssemblyReferencesOnlySpecifiedAssemblies()
        {
            var sut = typeof(TheoremAttribute).Assembly;
            var specifiedAssemblies = new []
            {
                "mscorlib",
                "System.Core",
                "Jwc.Experiment",
                "Ploeh.AutoFixture"
            };

            var actual = sut.GetReferencedAssemblies().Select(an => an.Name).Distinct().ToArray();

            Assert.Equal(specifiedAssemblies.Length, actual.Length);
            Assert.False(specifiedAssemblies.Except(actual).Any(), "Empty");
        }

        const string _productDirectory = @"..\..\..\..\src\Experiment.AutoFixture.NuGetFiles\";
        const string _testDirectory = @"..\..\..\..\test\Experiment.AutoFixture.NuGetFilesUnitTest\";

        [Theory]
        [InlineData(_productDirectory, "TheoremAttribute")]
        [InlineData(_productDirectory, "FirstClassTheoremAttribute")]
        [InlineData(_productDirectory, "AutoFixtureAdapter")]
        [InlineData(_testDirectory, "Scenario")]
        [InlineData(_testDirectory, "Person")]
        public void ThisCorrectlyGeneratesNugetTransformFiles(string directory, string originName)
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
                .Replace("namespace Jwc.Experiment", "namespace $rootnamespace$");

            File.WriteAllText(destination, content, Encoding.UTF8);

            Assert.True(File.Exists(destination), "exists.");
        }

        [Theory]
        [InlineData(_productDirectory, "TheoremAttribute")]
        [InlineData(_productDirectory, "FirstClassTheoremAttribute")]
        [InlineData(_productDirectory, "AutoFixtureAdapter")]
        [InlineData(_testDirectory, "Scenario")]
        public void NugetTransformFileShouldHaveJwcExperimentUsingDirective(string directory, string originName)
        {
            var path = directory + originName + ".cs";
            var actual = File.ReadAllText(path, Encoding.UTF8).Contains("using Jwc.Experiment;");
            Assert.True(actual, "Constains 'using Jwc.Experiment;'.");
        }
    }
}
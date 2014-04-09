using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;
using Xunit.Extensions;

namespace Jwc.NuGetFiles
{
    public class AssemblyLevelTest
    {
        const string _productDirectory = @"..\..\..\..\src\Experiment.AutoFixture.NuGetFiles\";
        const string _testDirectory = @"..\..\..\..\test\Experiment.AutoFixture.NuGetFilesUnitTest\";

        [Fact]
        public void SutReferencesOnlySpecifiedAssemblies()
        {
            var sut = typeof(TheoremAttribute).Assembly;
            var specifiedAssemblies = new []
            {
                "mscorlib",
                "System.Core",
                "Jwc.Experiment",
                "Experiment.AutoFixture",
                "Ploeh.AutoFixture"
            };

            var actual = sut.GetReferencedAssemblies().Select(an => an.Name).Distinct().ToArray();

            Assert.Equal(specifiedAssemblies.Length, actual.Length);
            Assert.False(specifiedAssemblies.Except(actual).Any(), "Empty");
        }

        [Theory]
        [InlineData(_productDirectory, "TheoremAttribute")]
        [InlineData(_productDirectory, "FirstClassTheoremAttribute")]
        [InlineData(_testDirectory, "Scenario")]
        [InlineData(_testDirectory, "Person")]
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
                .Replace("namespace Jwc.NuGetFiles", "namespace $rootnamespace$");

            File.WriteAllText(destination, content, Encoding.UTF8);

            Assert.True(File.Exists(destination), "exists.");
        }
    }
}
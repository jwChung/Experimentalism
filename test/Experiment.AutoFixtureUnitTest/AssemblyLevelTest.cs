using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Jwc.Experiment;
using Xunit;
using Xunit.Extensions;

namespace Jwc.Experiment
{
    public class AssemblyLevelTest
    {
        [Fact]
        public void SutReferencesOnlySpecifiedAssemblies()
        {
            var sut = typeof(AutoDataTheoremAttribute).Assembly;
            var specifiedAssemblies = new []
            {
                "mscorlib",
                "Jwc.Experiment",
                "Ploeh.AutoFixture"
            };

            var actual = sut.GetReferencedAssemblies().Select(an => an.Name).Distinct().ToArray();

            Assert.Equal(specifiedAssemblies.Length, actual.Length);
            Assert.False(specifiedAssemblies.Except(actual).Any(), "Empty");
        }

        [Theory]
        [InlineData("AutoDataTheoremAttribute")]
        [InlineData("TestFixtureAdapter")]
        public void SutGeneratesNugetTransformFiles(string originName)
        {
            string directory = @"..\..\..\..\src\Experiment.AutoFixture\";
            var origin = directory + originName + ".cs";
            var destination = directory + originName + ".cs.pp";
            Assert.True(File.Exists(origin), "exists.");
            VerifyGenerateFile(origin, destination);
        }

        [Conditional("CI")]
        private static void VerifyGenerateFile(string origin, string destination)
        {
            var content = File.ReadAllText(origin, Encoding.UTF8)
                .Replace("namespace Jwc.Experiment", "namespace $rootnamespace$");

            File.WriteAllText(destination, content, Encoding.UTF8);

            Assert.True(File.Exists(destination), "exists.");
        }
    }
}
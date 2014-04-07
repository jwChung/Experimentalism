﻿using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
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

        [Theory]
        [InlineData("TheoremAttribute")]
        [InlineData("FirstClassTheoremAttribute")]
        [InlineData("AutoFixtureAdapter")]
        public void ThisCorrectlyGeneratesNugetTransformFiles(string originName)
        {
            string directory = @"..\..\..\..\src\Experiment.AutoFixture.NuGetFiles\";
            var origin = directory + originName + ".cs";
            var destination = directory + originName + ".cs.pp";
            Assert.True(File.Exists(origin), "exists.");
            VerifyGeneratingFile(origin, destination);
        }

        [Theory]
        [InlineData("TheoremAttribute")]
        [InlineData("FirstClassTheoremAttribute")]
        [InlineData("AutoFixtureAdapter")]
        public void NugetTransformFileShouldHaveJwcExperimentUsingDirective(string originName)
        {
            string directory = @"..\..\..\..\src\Experiment.AutoFixture.NuGetFiles\";
            var path = directory + originName + ".cs";

            var actual = File.ReadAllText(path, Encoding.UTF8).Contains("using Jwc.Experiment;");

            Assert.True(actual, "Constains 'using Jwc.Experiment;'.");
        }

        [Theory]
        [InlineData("Scenario")]
        [InlineData("Person")]
        public void ThisCorrectlyGeneratesNugetTransformFilesForTest(string originName)
        {
            string directory = @"..\..\..\..\test\Experiment.AutoFixture.NuGetFilesUnitTest\";
            var origin = directory + originName + ".cs";
            var destination = directory + originName + ".cs.pp";
            Assert.True(File.Exists(origin), "exists.");
            VerifyGeneratingFile(origin, destination);
        }

        [Theory]
        [InlineData("Scenario")]
        public void NugetTransformFileForTestShouldHaveJwcExperimentUsingDirective(string originName)
        {
            string directory = @"..\..\..\..\test\Experiment.AutoFixture.NuGetFilesUnitTest\";
            var path = directory + originName + ".cs";

            var actual = File.ReadAllText(path, Encoding.UTF8).Contains("using Jwc.Experiment;");

            Assert.True(actual, "Constains 'using Jwc.Experiment;'.");
        }


        [Conditional("CI")]
        private static void VerifyGeneratingFile(string origin, string destination)
        {
            var content = File.ReadAllText(origin, Encoding.UTF8)
                .Replace("namespace Jwc.Experiment", "namespace $rootnamespace$");

            File.WriteAllText(destination, content, Encoding.UTF8);

            Assert.True(File.Exists(destination), "exists.");
        }
    }
}
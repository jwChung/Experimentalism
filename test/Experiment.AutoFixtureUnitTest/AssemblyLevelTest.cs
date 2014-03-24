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
        public void SutReferencesOnlySpecifiedAssemblies()
        {
            var sut = typeof(TheoremAttribute).Assembly;
            var specifiedAssemblies = new []
            {
                "mscorlib",
                "Jwc.Experiment",
                "Ploeh.AutoFixture",
                "Ploeh.AutoFixture.Xunit"
            };

            var actual = sut.GetReferencedAssemblies().Select(an => an.Name).Distinct().ToArray();

            Assert.Equal(specifiedAssemblies.Length, actual.Length);
            Assert.False(specifiedAssemblies.Except(actual).Any(), "Empty");
        }

        [Theory]
        [InlineData("TheoremAttribute")]
        [InlineData("TestFixtureAdapter")]
        public void SutGeneratesNugetTransformFiles(string originName)
        {
            string directory = @"..\..\..\..\src\Experiment.AutoFixture\";
            var origin = directory + originName + ".cs";
            var destination = directory + originName + ".cs.pp";
            Assert.True(File.Exists(origin), "exists.");
            VerifyGeneratingFile(origin, destination);
        }

        [Theory]
        [InlineData("Scenario")]
        [InlineData("Person")]
        public void SutGeneratesNugetTransformFilesForTest(string originName)
        {
            string directory = @"..\..\..\..\test\Experiment.AutoFixtureUnitTest\";
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
    }
}
﻿using System.Linq;
using System.Reflection;
using Jwc.Experiment.Idioms.Assertions;
using Xunit;
using Xunit.Extensions;

namespace Jwc.Experiment.Idioms
{
    public class AssemblyTest
    {
        [Fact]
        public void SutReferencesOnlySpecifiedAssemblies()
        {
            var sut = typeof(IIdiomaticMemberAssertion).Assembly;
            var specifiedAssemblies = new []
            {
                // GAC
                "mscorlib",
                "System.Core",

                // Direct references
                "Jwc.Experiment",
                "Ploeh.Albedo",
                
                // Indirect references
                "Ploeh.AutoFixture",
                "Ploeh.AutoFixture.Idioms",
                "Mono.Reflection"
            };

            var actual = sut.GetActualReferencedAssemblies();

            Assert.Equal(specifiedAssemblies.OrderBy(x => x), actual.OrderBy(x => x));
        }

        [Theory]
        [InlineData("Ploeh.AutoFixture")]
        [InlineData("Ploeh.AutoFixture.Idioms")]
        [InlineData("Mono.Reflection")]
        public void SutDoesNotExposeAnyTypesOfSpecifiedReference(string name)
        {
            // Fixture setup
            var sut = typeof(IIdiomaticMemberAssertion).Assembly;
            var assemblyName = sut.GetActualReferencedAssemblies().Single(n => n == name);
            var types = Assembly.Load(assemblyName).GetExportedTypes();

            // Exercise system and Verify outcome
            sut.VerifyDoesNotExpose(types);
        }
    }
}
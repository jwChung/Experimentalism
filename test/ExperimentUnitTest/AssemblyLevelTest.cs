﻿using System.Linq;
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
            var sut = typeof(BaseTheoremAttribute).Assembly;
            var specifiedAssemblies = new []
            {
                "mscorlib",
                "System.Core",
                "xunit",
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
            var sut = typeof(BaseTheoremAttribute).Assembly;
            var assemblyName = sut.GetActualReferencedAssemblies().Single(n => n == name);
            var types = Assembly.Load(assemblyName).GetExportedTypes();

            // Exercise system and Verify outcome
            sut.VerifyDoesNotExpose(types);
        }
    }
}
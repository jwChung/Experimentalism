using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jwc.Experiment.Idioms;
using Xunit;

namespace Jwc.Experiment
{
    public class AssemblyTest
    {
        [Fact]
        public void SutReferencesOnlySpecifiedAssemblies()
        {
            new RestrictiveReferenceAssertion(
                Assembly.Load("mscorlib"),
                typeof(ISet<>).Assembly /*System*/,
                typeof(Enumerable).Assembly /*System.Core*/,
                Assembly.Load("Jwc.Experiment"),
                Assembly.Load("Ploeh.Albedo"),
                Assembly.Load("Ploeh.AutoFixture"),
                Assembly.Load("Ploeh.AutoFixture.Idioms"),
                Assembly.Load("Mono.Reflection"))
            .Verify(typeof(IIdiomaticMemberAssertion).Assembly);
        }

        [Fact]
        public void SutDoesNotExposeAnyTypesOfSpecifiedReference()
        {
            new IndirectReferenceAssertion(
                Assembly.Load("Ploeh.AutoFixture"),
                Assembly.Load("Ploeh.AutoFixture.Idioms"),
                Assembly.Load("Mono.Reflection"))
            .Verify(typeof(IIdiomaticMemberAssertion).Assembly);
        }
    }
}
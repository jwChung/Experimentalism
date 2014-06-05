using System.Linq;
using System.Reflection;
using Jwc.Experiment.Idioms.Assertions;
using Xunit;

namespace Jwc.Experiment.Xunit
{
    public class AssemblyTest
    {
        [Fact]
        public void SutReferencesOnlySpecifiedAssemblies()
        {
            new RestrictiveReferenceAssertion(
                Assembly.Load("mscorlib"),
                typeof(Enumerable).Assembly /*System.Core*/,
                Assembly.Load("Jwc.Experiment"),
                Assembly.Load("xunit"),
                Assembly.Load("xunit.extensions"))
            .Verify(typeof(TestAttribute).Assembly);
        }

        [Fact]
        public void SutDoesNotExposeAnyTypesOfSpecifiedReference()
        {
            new IndirectReferenceAssertion(
                Assembly.Load("xunit.extensions"))
            .Verify(typeof(TestAttribute).Assembly);
        }
    }
}
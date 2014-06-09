using System.Linq;
using System.Reflection;
using Jwc.Experiment.Idioms;
using Xunit;

namespace Jwc.Experiment.AutoFixture
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
                Assembly.Load("Ploeh.AutoFixture"),
                Assembly.Load("Ploeh.AutoFixture.AutoMoq"))
            .Verify(typeof(TestFixture).Assembly);
        }

        [Fact]
        public void SutDoesNotExposeAnyTypesOfSpecifiedReference()
        {
            new IndirectReferenceAssertion(
                Assembly.Load("Ploeh.AutoFixture.AutoMoq"))
            .Verify(typeof(TestFixture).Assembly);
        }
    }
}
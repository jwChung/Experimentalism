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
                    Assembly.Load("System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"),
                    Assembly.Load("System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"),
                    Assembly.Load("Jwc.Experiment"),
                    Assembly.Load("Ploeh.Albedo"),
                    Assembly.Load("Ploeh.AutoFixture"),
                    Assembly.Load("Ploeh.AutoFixture.Idioms"),
                    Assembly.Load("Mono.Reflection"))
                .Verify(Assembly.Load("Jwc.Experiment.Idioms"));
        }

        [Fact]
        public void SutDoesNotExposeAnyTypesOfSpecifiedAssemblies()
        {
            new IndirectReferenceAssertion(
                    Assembly.Load("Ploeh.AutoFixture"),
                    Assembly.Load("Ploeh.AutoFixture.Idioms"),
                    Assembly.Load("Mono.Reflection"))
                .Verify(Assembly.Load("Jwc.Experiment.Idioms"));
        }
    }
}
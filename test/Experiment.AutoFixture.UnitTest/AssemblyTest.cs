namespace Jwc.Experiment.AutoFixture
{
    using System.Reflection;
    using Jwc.Experiment.Idioms;
    using global::Xunit;

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
                Assembly.Load("Ploeh.AutoFixture"),
                Assembly.Load("Ploeh.AutoFixture.AutoMoq"),
                Assembly.Load("Ploeh.AutoFixture.Xunit"))
                .Verify(Assembly.Load("Jwc.Experiment.AutoFixture"));
        }

        [Fact]
        public void SutDoesNotExposeAnyTypesOfSpecifiedAssemblies()
        {
            new IndirectReferenceAssertion(
                Assembly.Load("Ploeh.AutoFixture.AutoMoq"),
                Assembly.Load("Ploeh.AutoFixture.Xunit")) // Not il-merged
                .Verify(Assembly.Load("Jwc.Experiment.AutoFixture"));
        }
    }
}
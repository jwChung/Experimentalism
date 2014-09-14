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
                Assembly.Load("Jwc.Experiment"),
                Assembly.Load("Ploeh.AutoFixture"),
                Assembly.Load("Ploeh.AutoFixture.AutoMoq"))
                .Verify(Assembly.Load("Jwc.Experiment.AutoFixture"));
        }

        [Fact]
        public void SutDoesNotExposeAnyTypesOfSpecifiedAssemblies()
        {
            new IndirectReferenceAssertion(
                Assembly.Load("Ploeh.AutoFixture.AutoMoq"))
                .Verify(Assembly.Load("Jwc.Experiment.AutoFixture"));
        }
    }
}
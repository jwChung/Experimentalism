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
                Assembly.Load("Ploeh.AutoFixture"))
                .Verify(Assembly.Load("Jwc.Experiment.AutoFixture"));
        }
    }
}
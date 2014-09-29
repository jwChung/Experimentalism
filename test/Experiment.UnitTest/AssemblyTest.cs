namespace Jwc.Experiment
{
    using System.Reflection;
    using Jwc.Experiment.Idioms;
    using Xunit;

    public class AssemblyTest
    {
        [Fact]
        public void SutReferencesOnlySpecifiedAssemblies()
        {
            new RestrictiveReferenceAssertion(
                Assembly.Load("mscorlib"))
                .Verify(Assembly.Load("Jwc.Experiment"));
        }
    }
}
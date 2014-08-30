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
                    Assembly.Load("mscorlib"),
                    Assembly.Load("System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"))
                .Verify(Assembly.Load("Jwc.Experiment"));
        }
    }
}
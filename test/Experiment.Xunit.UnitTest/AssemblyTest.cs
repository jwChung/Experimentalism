namespace Jwc.Experiment.Xunit
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
                Assembly.Load("System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"),
                Assembly.Load("System.Xml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"),
                Assembly.Load("Jwc.Experiment"),
                Assembly.Load("xunit"),
                Assembly.Load("xunit.extensions"))
                .Verify(Assembly.Load("Jwc.Experiment.Xunit"));
        }

        [Fact]
        public void SutDoesNotExposeAnyTypesOfSpecifiedAssemblies()
        {
            new IndirectReferenceAssertion(
                Assembly.Load("xunit.extensions"))
                .Verify(Assembly.Load("Jwc.Experiment.Xunit"));
        }
    }
}
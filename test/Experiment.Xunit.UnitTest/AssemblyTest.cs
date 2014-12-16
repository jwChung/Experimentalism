namespace Jwc.Experiment.Xunit
{
    using System.Reflection;
    using Jwc.Experiment.Idioms;
    using global::Xunit;
    using global::Xunit.Extensions;

    public class AssemblyTest
    {
        [Fact]
        public void SutReferencesOnlySpecifiedAssemblies()
        {
            new RestrictiveReferenceAssertion(
                Assembly.Load("mscorlib"),
                Assembly.Load("System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"),
                Assembly.Load("System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"),
                Assembly.Load("Ploeh.AutoFixture, Version=3.18.7.0, Culture=neutral, PublicKeyToken=b24654c590009d4f"),
                Assembly.Load("Ploeh.AutoFixture.Xunit, Version=3.18.7.0, Culture=neutral, PublicKeyToken=b24654c590009d4f"),
                Assembly.Load("xunit"),
                Assembly.Load("xunit.extensions"))
                .Verify(Assembly.Load("Jwc.Experiment.Xunit"));
        }

        [Theory]
        [InlineData("xunit.extensions")] // Not il-merged
        public void SutDoesNotExposeSpecifiedAssemblies(string assembly)
        {
            new NotExposedReferenceAssertion(Assembly.Load(assembly))
                .Verify(Assembly.Load("Jwc.Experiment.Xunit"));
        }
    }
}
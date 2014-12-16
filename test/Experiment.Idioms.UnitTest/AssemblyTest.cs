namespace Jwc.Experiment
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
            var assemblies = new[]
            {
                Assembly.Load("mscorlib"),
                Assembly.Load("System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"),
                Assembly.Load("System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"),
                Assembly.Load("Ploeh.Albedo"),
                Assembly.Load("Ploeh.AutoFixture"),
                Assembly.Load("Mono.Reflection")
            };

            new RestrictiveReferenceAssertion(assemblies)
                .Verify(Assembly.Load("Jwc.Experiment.Idioms"));
        }

        [Theory]
        [InlineData("Mono.Reflection")]
        public void SutDoesNotExposeAnySpecifiedAssemblies(string assembly)
        {
            new NotExposedReferenceAssertion(Assembly.Load(assembly))
                .Verify(Assembly.Load("Jwc.Experiment.Idioms"));
        }
    }
}
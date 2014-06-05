using System.Reflection;
using Jwc.Experiment.Idioms.Assertions;
using Xunit;

namespace Jwc.Experiment
{
    public class AssemblyTest
    {
        [Fact]
        public void SutReferencesOnlySpecifiedAssemblies()
        {
            new RestrictiveReferenceAssertion(Assembly.Load("mscorlib"))
                .Verify(typeof(ITestFixture).Assembly);
        }
    }
}
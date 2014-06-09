using System.Linq;
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
                typeof(Enumerable).Assembly /*System.Core*/)
            .Verify(typeof(ITestFixture).Assembly);
        }
    }
}
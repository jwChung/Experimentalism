using System.Linq;
using Xunit;

namespace Jwc.Experiment
{
    public class AssemblyLevelTest
    {
        [Fact]
        public void SutReferencesOnlySpecifiedAssemblies()
        {
            var sut = typeof(ITestFixture).Assembly;
            var specifiedAssemblies = new []
            {
                // GAC
                "mscorlib"

                // Direct references
                // Indirect references
            };

            var actual = sut.GetActualReferencedAssemblies();

            Assert.Equal(specifiedAssemblies.OrderBy(x => x), actual.OrderBy(x => x));
        }
    }
}
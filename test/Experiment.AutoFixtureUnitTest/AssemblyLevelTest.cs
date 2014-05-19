using System.Linq;
using Xunit;

namespace Jwc.Experiment.AutoFixture
{
    public class AssemblyLevelTest
    {
        [Fact]
        public void SutReferencesOnlySpecifiedAssemblies()
        {
            var sut = typeof(TestFixture).Assembly;
            var specifiedAssemblies = new []
            {
                // GAC
                "mscorlib",
                "System.Core",

                // Direct references
                "Jwc.Experiment",
                "Ploeh.AutoFixture"

                // Indirect references
            };

            var actual = sut.GetActualReferencedAssemblies();

            Assert.Equal(specifiedAssemblies.OrderBy(x => x), actual.OrderBy(x => x));
        }
    }
}
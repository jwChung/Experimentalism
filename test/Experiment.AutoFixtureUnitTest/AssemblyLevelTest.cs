using System.Linq;
using Xunit;

namespace Jwc.Experiment
{
    public class AssemblyLevelTest
    {
        [Fact]
        public void SutReferencesOnlySpecifiedAssemblies()
        {
            var sut = typeof(AutoFixtureAdapter).Assembly;
            var specifiedAssemblies = new []
            {
                "mscorlib",
                "Jwc.Experiment",
                "Ploeh.AutoFixture"
            };

            var actual = sut.GetReferencedAssemblies().Select(an => an.Name).Distinct().ToArray();

            Assert.Equal(specifiedAssemblies.Length, actual.Length);
            Assert.False(specifiedAssemblies.Except(actual).Any(), "Empty");
        }
    }
}
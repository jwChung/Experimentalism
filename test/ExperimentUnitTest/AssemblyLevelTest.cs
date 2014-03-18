using System.Linq;
using Xunit;

namespace Jwc.Experiment
{
    public class AssemblyLevelTest
    {
        [Fact]
        public void SutOnlyReferencesSpecifiedAssemblies()
        {
            var sut = typeof(TheoremAttribute).Assembly;
            var specifiedAssemblies = new []
            {
                "mscorlib",
                "xunit",
                "xunit.extensions"
            };

            var actual = sut.GetReferencedAssemblies().Select(an => an.Name).Distinct().ToArray();

            Assert.Equal(specifiedAssemblies.Length, actual.Length);
            Assert.False(specifiedAssemblies.Except(actual).Any(), "Empty");
        } 
    }
}
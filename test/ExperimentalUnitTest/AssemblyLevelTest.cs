using System.Linq;
using System.Reflection;
using Xunit;

namespace Jwc.Experimental
{
    public class AssemblyLevelTest
    {
        [Fact]
        public void SutOnlyReferencesSpecifiedAssemblies()
        {
            var sut = Assembly.LoadFrom("Jwc.Experimental.dll");
            Assert.NotNull(sut);
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
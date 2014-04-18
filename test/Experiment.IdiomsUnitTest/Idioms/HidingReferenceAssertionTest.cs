using System.Linq;
using Ploeh.Albedo;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class HidingReferenceAssertionTest
    {
        [Fact]
        public void SutIsReflectionVisitor()
        {
            var sut = new HidingReferenceAssertion();
            Assert.IsAssignableFrom<IReflectionVisitor<object>>(sut);
        }

        [Fact]
        public void AssembliesIsCorrect()
        {
            var assemblies = new[]
            {
                typeof(object).Assembly,
                typeof(Enumerable).Assembly
            };
            var sut = new HidingReferenceAssertion(assemblies);

            var actual = sut.Assemblies;

            Assert.Equal(assemblies, actual);
        }
    }
}
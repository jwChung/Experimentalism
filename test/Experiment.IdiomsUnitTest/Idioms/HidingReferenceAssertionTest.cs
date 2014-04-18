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
    }
}
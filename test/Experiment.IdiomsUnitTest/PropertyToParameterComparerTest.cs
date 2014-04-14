using Ploeh.Albedo;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class PropertyToParameterComparerTest
    {
        [Fact]
        public void SutIsInverseEqualityComparer()
        {
            var sut = new PropertyToParameterComparer(new DelegatingTestFixture());
            Assert.IsAssignableFrom<InverseEqualityComparer<IReflectionElement>>(sut);
        }
    }
}
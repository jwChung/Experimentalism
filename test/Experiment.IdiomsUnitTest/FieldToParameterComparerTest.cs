using Ploeh.Albedo;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class FieldToParameterComparerTest
    {
        [Fact]
        public void SutIsInverseEqualityComparer()
        {
            var sut = new FieldToParameterComparer(new DelegatingTestFixture());
            Assert.IsAssignableFrom<InverseEqualityComparer<IReflectionElement>>(sut);
        } 
    }
}
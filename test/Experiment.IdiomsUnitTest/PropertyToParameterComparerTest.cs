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

        [Fact]
        public void EqualityComaprerIsCorrect()
        {
            var testFixture = new DelegatingTestFixture();
            var sut = new PropertyToParameterComparer(testFixture);

            var actual = sut.EqualityComparer;

            var comparer = Assert.IsAssignableFrom<ParameterToPropertyComparer>(actual);
            Assert.Equal(testFixture, comparer.TestFixture);
        }
    }
}
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

        [Fact]
        public void EqualityComaprerIsCorrect()
        {
            var testFixture = new DelegatingTestFixture();
            var sut = new FieldToParameterComparer(testFixture);

            var actual = sut.EqualityComparer;

            var comparer = Assert.IsAssignableFrom<ParameterToFieldComparer>(actual);
            Assert.Equal(testFixture, comparer.TestFixture);
        }
    }
}
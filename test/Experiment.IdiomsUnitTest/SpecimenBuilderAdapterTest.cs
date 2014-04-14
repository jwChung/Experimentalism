using Ploeh.AutoFixture.Kernel;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class SpecimenBuilderAdapterTest
    {
        [Fact]
        public void SutIsSpecimenBuilder()
        {
            var sut = new SpecimenBuilderAdapter(new DelegatingTestFixture());
            Assert.IsAssignableFrom<ISpecimenBuilder>(sut);
        }

        [Fact]
        public void TestFixtureIsCorrect()
        {
            var testFixture = new DelegatingTestFixture();
            var sut = new SpecimenBuilderAdapter(testFixture);

            var actual = sut.TestFixture;

            Assert.Equal(testFixture, actual);
        }
    }
}

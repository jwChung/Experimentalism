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
    }
}

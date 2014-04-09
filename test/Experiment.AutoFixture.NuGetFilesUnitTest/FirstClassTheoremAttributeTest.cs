using Jwc.Experiment;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using Xunit;

namespace Jwc.NuGetFiles
{
    public class FirstClassTheoremAttributeTest
    {
        [Fact]
        public void SutIsAutoFixtureFirstClassTheoremAttribute()
        {
            var sut = new FirstClassTheoremAttribute();
            Assert.IsAssignableFrom<AutoFixtureFirstClassTheoremAttribute>(sut);
        }

        [Fact]
        public void CreatTestFixtureReturnsCorrectFixture()
        {
            var sut = new FirstClassTheoremAttribute();
            var dummyMethod = typeof(object).GetMethod("ToString");

            var actual = sut.CreateTestFixture(dummyMethod);

            var adapter = Assert.IsType<AutoFixtureAdapter>(actual);
            var context = Assert.IsType<SpecimenContext>(adapter.SpecimenContext);
            Assert.IsType<Fixture>(context.Builder);
        }
    }
}
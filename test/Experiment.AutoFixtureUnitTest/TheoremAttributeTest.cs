using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using Xunit;

namespace Jwc.Experiment
{
    public class TheoremAttributeTest
    {
        [Fact]
        public void SutIsTheoremAttribute()
        {
            var sut = new TheoremAttribute();
            Assert.IsAssignableFrom<NaiveTheoremAttribute>(sut);
        }

        [Fact]
        public void FixtureTypeIsCorrect()
        {
            var sut = new TheoremAttribute();
            var actual = sut.FixtureType;
            Assert.Equal(typeof(TestFixtureAdapter), actual);
        }

        [Fact]
        public void FixtureFactoryIsCorrect()
        {
            var sut = new TheoremAttribute();

            var actual = sut.FixtureFactory(null);

            var adapter = Assert.IsType<TestFixtureAdapter>(actual);
            var context = Assert.IsType<SpecimenContext>(adapter.SpecimenContext);
            Assert.IsType<Fixture>(context.Builder);
            Assert.NotSame(sut.FixtureFactory(null), actual);
        }
    }
}
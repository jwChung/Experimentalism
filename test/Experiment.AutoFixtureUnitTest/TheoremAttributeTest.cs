using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using Ploeh.AutoFixture.Xunit;
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
            var dummyMethod = typeof(object).GetMethod("ToString");

            var actual = sut.FixtureFactory(dummyMethod);

            var adapter = Assert.IsType<TestFixtureAdapter>(actual);
            var context = Assert.IsType<SpecimenContext>(adapter.SpecimenContext);
            Assert.IsType<Fixture>(context.Builder);
        }

        [Fact]
        public void FixtureFactoryAlwaysCreatesNewInstance()
        {
            var sut = new TheoremAttribute();
            var dummyMethod = typeof(object).GetMethod("ToString");

            var actual = sut.FixtureFactory(dummyMethod);

            Assert.NotNull(actual);
            Assert.NotSame(sut.FixtureFactory(dummyMethod), actual);
        }

        [Fact]
        public void FixtureFactoryReflectsCustomizeAttribute()
        {
            var sut = new TheoremAttribute();
            var actual = sut.FixtureFactory(GetType().GetMethod("FrozenTest"));
            Assert.Same(actual.Create(typeof(string)), actual.Create(typeof(string)));
        }

        public void FrozenTest([Frozen] string arg)
        {
        }
    }
}
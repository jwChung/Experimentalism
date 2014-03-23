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
        public void FixtureFactoryIsCorrect()
        {
            var sut = new TheoremAttribute();

            var actual = sut.FixtureFactory(null);

            Assert.IsType<TestFixtureAdapter>(actual);
            Assert.NotSame(sut.FixtureFactory(null), actual);
        }
    }
}
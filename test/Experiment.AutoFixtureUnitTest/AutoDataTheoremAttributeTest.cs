using Xunit;

namespace Jwc.Experiment
{
    public class AutoDataTheoremAttributeTest
    {
        [Fact]
        public void SutIsTheoremAttribute()
        {
            var sut = new AutoDataTheoremAttribute();
            Assert.IsAssignableFrom<TheoremAttribute>(sut);
        }

        [Fact]
        public void FixtureFactoryIsCorrect()
        {
            var sut = new AutoDataTheoremAttribute();

            var actual = sut.FixtureFactory();

            Assert.IsType<TestFixtureAdapter>(actual);
            Assert.NotSame(sut.FixtureFactory(), actual);
        }
    }
}
using Xunit;

namespace Jwc.Experiment
{
    public class TheoremAttributeTest
    {
        [Fact]
        public void SutIsTheoremAttribute()
        {
            var sut = new TheoremAttribute();
            Assert.IsAssignableFrom<DefaultTheoremAttribute>(sut);
        }

        [Fact]
        public void FixtureTypeIsCorrect()
        {
            var sut = new TheoremAttribute();
            var actual = sut.FixtureFactory;
            Assert.IsType<AutoFixtureFactory>(actual);
        }
    }
}
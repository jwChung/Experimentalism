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
        public void FixtureFactoryIsCorrect()
        {
            var sut = new TheoremAttribute();
            var actual = sut.FixtureFactory;
            Assert.Same(AutoFixtureFactory.Instance, actual);
        }
    }
}
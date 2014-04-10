using Jwc.Experiment;
using Ploeh.AutoFixture;
using Xunit;

namespace Jwc.NuGetFiles
{
    public class TheoremAttributeTest
    {
        [Fact]
        public void SutIsAutoFixtureTheoremAttribute()
        {
            var sut = new TheoremAttribute();
            Assert.IsAssignableFrom<AutoFixtureTheoremAttribute>(sut);
        }

        [Fact]
        public void CreatFixtureReturnsCorrectFixture()
        {
            var sut = new TestSpecificTheoremAttribute();
            var actual = sut.CallCreateFixture();
            Assert.IsType<Fixture>(actual);
        }

        private class TestSpecificTheoremAttribute : TheoremAttribute
        {
            public IFixture CallCreateFixture()
            {
                return CreateFixture();
            }
        }
    }
}
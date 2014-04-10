using Jwc.Experiment;
using Ploeh.AutoFixture;
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
        public void CreatFixtureReturnsCorrectFixture()
        {
            var sut = new TestSpecificFirstClassTheoremAttribute();
            var actual = sut.CallCreateFixture();
            Assert.IsType<Fixture>(actual);
        }

        private class TestSpecificFirstClassTheoremAttribute : FirstClassTheoremAttribute
        {
            public IFixture CallCreateFixture()
            {
                return CreateFixture();
            }
        }
    }
}
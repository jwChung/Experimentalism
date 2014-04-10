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
        public void CreatTestFixtureReturnsCorrectFixture()
        {
            var sut = new TheoremAttribute();
            var dummyMethod = typeof(object).GetMethod("ToString");

            var actual = sut.CreateTestFixture(dummyMethod);

            var adapter = Assert.IsType<AutoFixtureAdapter>(actual);
            Assert.IsType<Fixture>(adapter.Fixture);
        }
        
        [Fact]
        public void CreateTestFixtureAlwaysReturnsNewInstance()
        {
            var sut = new TheoremAttribute();
            var dummyMethod = typeof(object).GetMethod("ToString");

            var actual = sut.CreateTestFixture(dummyMethod);

            Assert.NotNull(actual);
            Assert.NotSame(sut.CreateTestFixture(dummyMethod), actual);
        }
    }
}
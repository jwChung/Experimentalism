using Ploeh.AutoFixture;
using Xunit;

namespace NuGet.Jwc.Experiment
{
    public class FixtureFactoryTest
    {
        [Fact]
        public void CreateReturnsCorrectFixture()
        {
            var actual = FixtureFactory.Create(null);
            Assert.IsAssignableFrom<Fixture>(actual);
        } 
    }
}
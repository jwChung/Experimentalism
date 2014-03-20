using Jwc.Experiment;
using Xunit;

namespace Experiment.AutoFixtureUnitTest
{
    public class AutoDataTheoremAttributeTest
    {
        [Fact]
        public void SutIsTheoremAttribute()
        {
            var sut = new AutoDataTheoremAttribute();
            Assert.IsAssignableFrom<TheoremAttribute>(sut);
        }
    }
}
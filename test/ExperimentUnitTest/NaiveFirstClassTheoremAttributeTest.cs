using Xunit;

namespace Jwc.Experiment
{
    public class NaiveFirstClassTheoremAttributeTest
    {
        [Fact]
        public void SutIsFactAttribute()
        {
            var sut = new NaiveFirstClassTheoremAttribute();
            Assert.IsAssignableFrom<FactAttribute>(sut);
        }
    }
}
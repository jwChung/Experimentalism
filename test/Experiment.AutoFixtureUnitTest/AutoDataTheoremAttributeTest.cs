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
    }
}
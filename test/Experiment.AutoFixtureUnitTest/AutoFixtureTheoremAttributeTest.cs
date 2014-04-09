using Xunit;

namespace Jwc.Experiment
{
    public class AutoFixtureTheoremAttributeTest
    {
        [Fact]
        public void SutIsBaseTheoremAttribute()
        {
            var sut = new DelegatingAutoFixtureTheoremAttribute();
            Assert.IsAssignableFrom<BaseTheoremAttribute>(sut);
        }

        private class DelegatingAutoFixtureTheoremAttribute : AutoFixtureTheoremAttribute
        {
        }
    }
}
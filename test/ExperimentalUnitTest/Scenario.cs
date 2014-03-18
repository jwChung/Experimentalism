using Xunit;

namespace Jwc.Experimental
{
    public class Scenario
    {
        [Theorem]
        public void TheoremActAsFactAttribute()
        {
            Assert.True(true, "Excuted");
        }
    }
}
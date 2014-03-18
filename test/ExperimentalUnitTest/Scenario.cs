using Xunit;

namespace Jwc.Experimental
{
    public class Scenario
    {
        [Theorem]
        public void TheoremAttributeOnMethodIndicatesTestCase()
        {
            Assert.True(true, "excuted.");
        }
    }
}
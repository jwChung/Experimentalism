using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class GuardClauseAssertionTest
    {
        [Fact]
        public void SutIsAssertionAdapter()
        {
            var sut = new GuardClauseAssertion(new DelegatingTestFixture());
            Assert.IsAssignableFrom<AssertionAdapter>(sut);
        }
    }
}
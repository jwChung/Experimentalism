using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class GuardClauseAssertionTestCasesTest
    {
        [Fact]
        public void SutIsIdiomaticTestCases()
        {
            var sut = new GuardClauseAssertionTestCases(GetType());
            Assert.IsAssignableFrom<IdiomaticTestCases>(sut);
        } 
    }
}
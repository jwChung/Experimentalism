using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class GuardClauseAssertionFactoryTest
    {
        [Fact]
        public void SutIsAssertionFactory()
        {
            var sut = new GuardClauseAssertionFactory();
            Assert.IsAssignableFrom<IAssertionFactory>(sut);
        } 
    }
}
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

        [Fact]
        public void CreateReturnsCorrectAssertion()
        {
            var sut = new GuardClauseAssertionFactory();
            var testFixture = new DelegatingTestFixture();

            var actual = sut.Create(testFixture);

            var assertion = Assert.IsAssignableFrom<GuardClauseAssertion>(actual);
            Assert.Equal(testFixture, assertion.TestFixture);
        }
    }
}
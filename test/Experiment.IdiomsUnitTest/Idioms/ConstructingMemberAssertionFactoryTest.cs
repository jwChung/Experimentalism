using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class ConstructingMemberAssertionFactoryTest
    {
        [Fact]
        public void SutIsAssertionFactory()
        {
            var sut = new ConstructingMemberAssertionFactory();
            Assert.IsAssignableFrom<IAssertionFactory>(sut);
        }

        [Fact]
        public void CreateReturnsCorrectConstructingMemberAssertion()
        {
            var testFixture = new DelegatingTestFixture();
            var sut = new ConstructingMemberAssertionFactory();

            var actual = sut.Create(testFixture);

            var assertion = Assert.IsAssignableFrom<ConstructingMemberAssertion>(actual);
            Assert.Equal(testFixture, assertion.TestFixture);
        }
    }
}
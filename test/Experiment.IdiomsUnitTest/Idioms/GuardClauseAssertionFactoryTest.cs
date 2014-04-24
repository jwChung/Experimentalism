using System;
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

        [Fact]
        public void CreateWithNullTestFixtureThrows()
        {
            var sut = new GuardClauseAssertionFactory();
            Assert.Throws<ArgumentNullException>(() => sut.Create(null));
        }
    }
}
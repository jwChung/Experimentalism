using System;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Idioms;
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
            var fixture = new Fixture();
            var fakeTestFixture = new DelegatingTestFixture
            {
                OnCreate = s =>
                {
                    Assert.Equal(typeof(Fixture), s);
                    return fixture;
                }
            };
            
            var actual = sut.Create(fakeTestFixture);

            var assertionAdapter = Assert.IsAssignableFrom<AssertionAdapter>(actual);
            var assertion = Assert.IsAssignableFrom<GuardClauseAssertion>(assertionAdapter.Assertion);
            Assert.Same(fixture, assertion.Builder);
        }

        [Fact]
        public void CreateWithNullTestFixtureThrows()
        {
            var sut = new GuardClauseAssertionFactory();
            Assert.Throws<ArgumentNullException>(() => sut.Create(null));
        }
    }
}
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

        [Fact]
        public void AssertionIsCorrect()
        {
            var testFixture = new DelegatingTestFixture();
            var sut = new GuardClauseAssertion(testFixture);

            var actual = sut.Assertion;

            var assertion = Assert.IsAssignableFrom<Ploeh.AutoFixture.Idioms.GuardClauseAssertion>(actual);
            var builder = Assert.IsAssignableFrom<SpecimenBuilderAdapter>(assertion.Builder);
            Assert.Equal(testFixture, builder.TestFixture);
        }

        [Fact]
        public void TestFixtureIsCorrect()
        {
            var testFixture = new DelegatingTestFixture();
            var sut = new GuardClauseAssertion(testFixture);

            var actual = sut.TestFixture;

            Assert.Equal(testFixture, actual);
        }
    }
}
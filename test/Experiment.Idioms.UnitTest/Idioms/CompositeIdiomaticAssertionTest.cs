namespace Jwc.Experiment.Idioms
{
    using System.Collections.Generic;
    using Ploeh.AutoFixture.Idioms;
    using Ploeh.AutoFixture.Xunit;
    using global::Xunit;
    using global::Xunit.Extensions;

    public class CompositeIdiomaticAssertionTest
    {
        [Theory, TestData]
        public void SutIsIdiomaticAssertion(CompositeIdiomaticAssertion sut)
        {
            Assert.IsAssignableFrom<IIdiomaticAssertion>(sut);
        }

        [Theory, TestData]
        public void AssertionsIsCorrect(
            [Frozen] IEnumerable<IIdiomaticAssertion> assertions,
            CompositeIdiomaticAssertion sut)
        {
            var actual = sut.Assertions;
            Assert.Equal(assertions, actual);
        }
    }
}
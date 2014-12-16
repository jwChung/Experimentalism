namespace Jwc.Experiment.Idioms
{
    using global::Xunit;
    using global::Xunit.Extensions;

    public class CompositeIdiomaticAssertionTest
    {
        [Theory, TestData]
        public void SutIsIdiomaticAssertion(CompositeIdiomaticAssertion sut)
        {
            Assert.IsAssignableFrom<IIdiomaticAssertion>(sut);
        }
    }
}
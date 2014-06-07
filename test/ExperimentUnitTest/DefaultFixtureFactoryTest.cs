using Xunit;

namespace Jwc.Experiment
{
    public class DefaultFixtureFactoryTest
    {
        [Fact]
        public void CurrentIsCorrect()
        {
            Assert.IsAssignableFrom<NotSupportedFixtureFactory>(DefaultFixtureFactory.Current);
        }

        [StaticFact]
        public void SetCurrentCorrectlySetsFactoryToCurrent()
        {
            var expected = new DelegatingTestFixtureFactory();
            DefaultFixtureFactory.SetCurrent(expected);
            Assert.Equal(expected, DefaultFixtureFactory.Current);
        }
    }
}
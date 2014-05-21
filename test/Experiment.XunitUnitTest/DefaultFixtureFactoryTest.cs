using Xunit;

namespace Jwc.Experiment.Xunit
{
    public class DefaultFixtureFactoryTest
    {
        [Fact]
        public void CurrentIsCorrect()
        {
            Assert.IsAssignableFrom<NotSupportedFixtureFactory>(DefaultFixtureFactory.Current);
        }

        [Fact]
        public void SetCurrentCorrectlySetsFactoryToCurrent()
        {
            var expected = new DelegatingTestFixtureFactory();
            DefaultFixtureFactory.SetCurrent(expected);
            Assert.Equal(expected, DefaultFixtureFactory.Current);
            DefaultFixtureFactory.SetCurrent(null);
        }
    }
}
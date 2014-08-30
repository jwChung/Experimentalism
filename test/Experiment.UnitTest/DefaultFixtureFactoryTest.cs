namespace Jwc.Experiment
{
    using Xunit;

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
            try
            {
                var expected = new DelegatingTestFixtureFactory();
                DefaultFixtureFactory.SetCurrent(expected);
                Assert.Equal(expected, DefaultFixtureFactory.Current);
            }
            finally
            {
                DefaultFixtureFactory.SetCurrent(null);
            }
        }
    }
}
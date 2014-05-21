using Xunit;
using Xunit.Extensions;

namespace Jwc.Experiment.Xunit
{
    public class DefaultFixtureFactoryTest
    {
        [Theory]
        [InlineData("CurrentIsCorrect")]
        [InlineData("SetCurrentCorrectlySetsFactoryToCurrent")]
        public void RunTestWithStaticFixture(string testMethod)
        {
            GetType().GetMethod(testMethod).Execute();
        }

        public void CurrentIsCorrect()
        {
            Assert.IsAssignableFrom<NotSupportedFixtureFactory>(DefaultFixtureFactory.Current);
        }

        public void SetCurrentCorrectlySetsFactoryToCurrent()
        {
            var expected = new DelegatingTestFixtureFactory();
            DefaultFixtureFactory.SetCurrent(expected);
            Assert.Equal(expected, DefaultFixtureFactory.Current);
        }
    }
}
using System;
using Xunit;
using Xunit.Extensions;

namespace Jwc.Experiment.Xunit
{
    public class TestFixtureFactoryTest
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
            Assert.IsAssignableFrom<NotSupportedFixtureFactory>(TestFixtureFactory.Current);
        }

        public void SetCurrentCorrectlySetsFactoryToCurrent()
        {
            var expected = new DelegatingTestFixtureFactory();
            TestFixtureFactory.SetCurrent(expected);
            Assert.Equal(expected, TestFixtureFactory.Current);
        }
    }
}
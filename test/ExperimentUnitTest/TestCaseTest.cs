using System;
using Xunit;

namespace Jwc.Experiment
{
    public class TestCaseTest
    {
        [Fact]
        public void SutIsTestCase()
        {
            var sut = new TestCase(() => { });
            Assert.IsAssignableFrom<ITestCase>(sut);
        }

        [Fact]
        public void InitializeWithNullActionThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new TestCase((Action)null));
        }

        [Fact]
        public void InitializeWithNullFuncThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new TestCase((Func<object>)null));
        }
    }
}
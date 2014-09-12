namespace Jwc.Experiment.Xunit
{
    using System;
    using global::Xunit;

    public class TestInfoTest
    {
        [Fact]
        public void SutIsTestMethodInfo()
        {
            var sut = new TestInfo();
            Assert.IsAssignableFrom<ITestMethodInfo>(sut);
        }

        [Fact]
        public void SutIsTestCommandInfo()
        {
            var sut = new TestInfo();
            Assert.IsAssignableFrom<ITestCommandInfo>(sut);
        }
    }
}

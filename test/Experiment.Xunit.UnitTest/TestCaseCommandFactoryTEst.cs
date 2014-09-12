namespace Jwc.Experiment.Xunit
{
    using System;
    using global::Xunit;

    public class TestCaseCommandFactoryTest
    {
        [Fact]
        public void SutIsTestCommandFactory()
        {
            var sut = new TestCaseCommandFactory();
            Assert.IsAssignableFrom<ITestCommandFactory>(sut);
        }
    }
}

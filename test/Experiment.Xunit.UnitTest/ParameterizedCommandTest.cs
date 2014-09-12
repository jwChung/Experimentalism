namespace Jwc.Experiment.Xunit
{
    using System;
    using Ploeh.Albedo;
    using global::Xunit;
    using global::Xunit.Sdk;

    public class ParameterizedCommandTest
    {
        [Fact]
        public void SutIsTestCommand()
        {
            var sut = new ParameterizedCommand(Mocked.Of<ITestCommandInfo>());
            Assert.IsAssignableFrom<ITestCommand>(sut);
        }

        [Fact]
        public void InitializeWithNullTestCommandInfoThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new ParameterizedCommand(null));
        }

        [Test]
        public void MethodNameIsCorrect()
        {
            var method = new Methods<ParameterizedCommandTest>().Select(x => x.MethodNameIsCorrect());
            var sut = new ParameterizedCommand(
                Mocked.Of<ITestCommandInfo>(x => x.TestMethod == Reflector.Wrap(method)));

            var actual = sut.MethodName;

            Assert.Equal(method.Name, actual);
        }
    }
}

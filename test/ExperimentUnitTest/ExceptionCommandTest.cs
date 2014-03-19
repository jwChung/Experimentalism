using System.Reflection;
using Xunit;
using Xunit.Sdk;

namespace Jwc.Experiment
{
    public class ExceptionCommandTest
    {
        [Fact]
        public void SutIsTestCommand()
        {
            var sut = new ExceptionCommand(Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()));
            Assert.IsAssignableFrom<TestCommand>(sut);
        }

        [Fact]
        public void MethodNameIsCorrect()
        {
            var sut = new ExceptionCommand(Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()));
            var actual = sut.MethodName;
            Assert.Equal("MethodNameIsCorrect", actual);
        }

        [Fact]
        public void DisplayNameIsCorrect()
        {
            var sut = new ExceptionCommand(Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()));
            var actual = sut.DisplayName;
            Assert.Equal("Jwc.Experiment.ExceptionCommandTest.DisplayNameIsCorrect", actual);
        }

        [Fact]
        public void TimeoutIsCorrect()
        {
            var sut = new ExceptionCommand(Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()));
            var actual = sut.Timeout;
            Assert.Equal(0, actual);
        }
    }
}
using System;
using System.Reflection;
using Xunit;
using Xunit.Sdk;

namespace Jwc.Experiment.Xunit
{
    public class ExceptionCommandTest
    {
        [Fact]
        public void SutIsTestCommand()
        {
            var sut = new ExceptionCommand(
                Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()),
                new Exception());
            Assert.IsAssignableFrom<TestCommand>(sut);
        }

        [Fact]
        public void MethodNameIsCorrect()
        {
            var sut = new ExceptionCommand(
                Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()),
                new Exception());
            var actual = sut.MethodName;
            Assert.Equal("MethodNameIsCorrect", actual);
        }

        [Fact]
        public void DisplayNameIsCorrect()
        {
            var sut = new ExceptionCommand(
                Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()),
                new Exception());
            var actual = sut.DisplayName;
            Assert.Equal("Jwc.Experiment.Xunit.ExceptionCommandTest.DisplayNameIsCorrect", actual);
        }

        [Fact]
        public void TimeoutIsCorrect()
        {
            var sut = new ExceptionCommand(
                Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()),
                new Exception());
            var actual = sut.Timeout;
            Assert.Equal(0, actual);
        }

        [Fact]
        public void InitializeWithNullMethodThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new ExceptionCommand(null, new Exception()));
        }

        [Fact]
        public void InitializeWithNullExceptionThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new ExceptionCommand(Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()), null));
        }

        [Fact]
        public void ExceptionIsCorrect()
        {
            var exception = new Exception();
            var sut = new ExceptionCommand(
                Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()),
                exception);

            var actual = sut.Exception;

            Assert.Equal(exception, actual);
        }

        [Fact]
        public void ExecuteReturnsCorrectResult()
        {
            var sut = new ExceptionCommand(
                Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()),
                new Exception());

            var actual = sut.Execute(null);

            var failedResult = Assert.IsType<FailedResult>(actual);
            Assert.Equal(sut.MethodName, failedResult.MethodName);
            Assert.Equal(sut.Exception.GetType().FullName, failedResult.ExceptionType);
            Assert.Equal(sut.DisplayName, failedResult.DisplayName);
        }
    }
}
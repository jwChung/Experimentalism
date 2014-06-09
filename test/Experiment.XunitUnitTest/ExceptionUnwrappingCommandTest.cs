using System;
using System.Reflection;
using System.Xml;
using Moq;
using Xunit;
using Xunit.Sdk;

namespace Jwc.Experiment.Xunit
{
    public class ExceptionUnwrappingCommandTest
    {
        [Fact]
        public void SutIsTestCommand()
        {
            var sut = new ExceptionUnwrappingCommand(new Mock<ITestCommand>().Object);
            Assert.IsAssignableFrom<ITestCommand>(sut);
        }

        [Fact]
        public void InitializeWithNullTestCommandThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new ExceptionUnwrappingCommand(null));
        }

        [Fact]
        public void TestCommandIsCorrect()
        {
            var testCommand = new Mock<ITestCommand>().Object;
            var sut = new ExceptionUnwrappingCommand(testCommand);

            var actual = sut.TestCommand;

            Assert.Equal(testCommand, actual);
        }

        [Fact]
        public void DisplayNameIsCorrect()
        {
            var testCommand = new Mock<ITestCommand>().Object;
            var expected = "anonymous";
            Mock.Get(testCommand).SetupGet(x => x.DisplayName).Returns(expected);
            var sut = new ExceptionUnwrappingCommand(testCommand);

            var actual = sut.DisplayName;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldCreateInstanceIsCorrect()
        {
            var testCommand = new Mock<ITestCommand>().Object;
            var expected = true;
            Mock.Get(testCommand).SetupGet(x => x.ShouldCreateInstance).Returns(expected);
            var sut = new ExceptionUnwrappingCommand(testCommand);

            var actual = sut.ShouldCreateInstance;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TimeoutIsCorrect()
        {
            var testCommand = new Mock<ITestCommand>().Object;
            var expected = 123;
            Mock.Get(testCommand).SetupGet(x => x.Timeout).Returns(expected);
            var sut = new ExceptionUnwrappingCommand(testCommand);

            var actual = sut.Timeout;

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void ToStartXmlReturnsCorrectResult()
        {
            var testCommand = new Mock<ITestCommand>().Object;
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml("<top/>");
            Mock.Get(testCommand).Setup(x => x.ToStartXml()).Returns(xmlDocument.FirstChild);
            var sut = new ExceptionUnwrappingCommand(testCommand);

            var actual = sut.ToStartXml();

            Assert.Equal(xmlDocument.FirstChild, actual);
        }

        [Fact]
        public void ExcuteReturnsCorrectResult()
        {
            var testClass = new object();
            var methodResult = new PassedResult(Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()), null);
            var testCommand = new Mock<ITestCommand>().Object;
            Mock.Get(testCommand).Setup(x => x.Execute(testClass)).Returns(methodResult);
            var sut = new ExceptionUnwrappingCommand(testCommand);

            var actual = sut.Execute(testClass);

            Assert.Equal(methodResult, actual);
        }

        [Fact]
        public void ExecuteUnwrapsTargetInvocationException()
        {
            // Fixture setup
            var testClass = new object();

            var testCommand = new Mock<ITestCommand>().Object;
            var inner = new InvalidOperationException();
            var exception = new TargetInvocationException(inner);
            Mock.Get(testCommand).Setup(x => x.Execute(testClass)).Throws(exception);

            var sut = new ExceptionUnwrappingCommand(testCommand);

            // Exercise system and Verify outcome
            Assert.Throws(inner.GetType(), () => sut.Execute(testClass));
        }

        [Fact]
        public void ExecuteDoesNotUnwrapIfTargetInvocationExceptionIsNotThrown()
        {
            // Fixture setup
            var testClass = new object();

            var testCommand = new Mock<ITestCommand>().Object;
            var exception = new Exception("message", new InvalidOperationException());
            Mock.Get(testCommand).Setup(x => x.Execute(testClass)).Throws(exception);

            var sut = new ExceptionUnwrappingCommand(testCommand);

            // Exercise system and Verify outcome
            Assert.Throws(exception.GetType(), () => sut.Execute(testClass));
        }
    }
}
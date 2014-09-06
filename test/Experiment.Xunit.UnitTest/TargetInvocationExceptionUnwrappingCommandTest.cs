namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Reflection;
    using System.Xml;
    using Moq;
    using global::Xunit;
    using global::Xunit.Sdk;

    public class TargetInvocationExceptionUnwrappingCommandTest
    {
        public static void ThrowInvalidOperationException()
        {
            throw new InvalidOperationException();
        }

        public static void ThrowInvalidOperationException2()
        {
            typeof(TargetInvocationExceptionUnwrappingCommandTest)
                .GetMethod("ThrowInvalidOperationException")
                .Invoke(null, new object[0]);
        }

        [Fact]
        public void SutIsTestCommand()
        {
            var sut = new TargetInvocationExceptionUnwrappingCommand(new Mock<ITestCommand>().Object);
            Assert.IsAssignableFrom<ITestCommand>(sut);
        }

        [Fact]
        public void InitializeWithNullTestCommandThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new TargetInvocationExceptionUnwrappingCommand(null));
        }

        [Fact]
        public void TestCommandIsCorrect()
        {
            var testCommand = new Mock<ITestCommand>().Object;
            var sut = new TargetInvocationExceptionUnwrappingCommand(testCommand);

            var actual = sut.TestCommand;

            Assert.Equal(testCommand, actual);
        }

        [Fact]
        public void DisplayNameIsCorrect()
        {
            var expected = "anonymous";
            var testCommand = Mock.Of<ITestCommand>(x => x.DisplayName == expected);
            var sut = new TargetInvocationExceptionUnwrappingCommand(testCommand);

            var actual = sut.DisplayName;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ShouldCreateInstanceIsCorrect()
        {
            var expected = true;
            var testCommand = Mock.Of<ITestCommand>(x => x.ShouldCreateInstance == expected);
            var sut = new TargetInvocationExceptionUnwrappingCommand(testCommand);

            var actual = sut.ShouldCreateInstance;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TimeoutIsCorrect()
        {
            var expected = 123;
            var testCommand = Mock.Of<ITestCommand>(x => x.Timeout == expected);
            var sut = new TargetInvocationExceptionUnwrappingCommand(testCommand);

            var actual = sut.Timeout;

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void ToStartXmlReturnsCorrectResult()
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml("<top/>");
            var expected = xmlDocument.FirstChild;
            var testCommand = Mock.Of<ITestCommand>(x => x.ToStartXml() == expected);
            var sut = new TargetInvocationExceptionUnwrappingCommand(testCommand);

            var actual = sut.ToStartXml();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ExcuteReturnsCorrectResult()
        {
            var testClass = new object();
            var methodResult = new PassedResult(Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()), null);
            var testCommand = Mock.Of<ITestCommand>(x => x.Execute(testClass) == methodResult);
            var sut = new TargetInvocationExceptionUnwrappingCommand(testCommand);

            var actual = sut.Execute(testClass);

            Assert.Equal(methodResult, actual);
        }

        [Fact]
        public void ExecuteUnwrapsTargetInvocationException()
        {
            // Fixture setup
            var testClass = new object();

            var testCommand = Mock.Of<ITestCommand>();
            var inner = new InvalidOperationException();
            var exception = new TargetInvocationException(inner);
            Mock.Get(testCommand).Setup(x => x.Execute(testClass)).Throws(exception);

            var sut = new TargetInvocationExceptionUnwrappingCommand(testCommand);

            // Exercise system and Verify outcome
            Assert.Throws(inner.GetType(), () => sut.Execute(testClass));
        }

        [Fact]
        public void ExecuteDoesNotUnwrapIfTargetInvocationExceptionIsNotThrown()
        {
            // Fixture setup
            var testClass = new object();

            var testCommand = Mock.Of<ITestCommand>();
            var exception = new Exception("message", new InvalidOperationException());
            Mock.Get(testCommand).Setup(x => x.Execute(testClass)).Throws(exception);

            var sut = new TargetInvocationExceptionUnwrappingCommand(testCommand);

            // Exercise system and Verify outcome
            Assert.Throws(exception.GetType(), () => sut.Execute(testClass));
        }

        [Fact]
        public void ExecuteDoesNotThrowIfInnerExceptionIsNullWhenUnwrappingTargetInvocaionException()
        {
            // Fixture setup
            var testClass = new object();

            var testCommand = Mock.Of<ITestCommand>();
            var exception = new TargetInvocationException(null);
            Mock.Get(testCommand).Setup(x => x.Execute(testClass)).Throws(exception);

            var sut = new TargetInvocationExceptionUnwrappingCommand(testCommand);

            // Exercise system and Verify outcome
            Assert.Throws(exception.GetType(), () => sut.Execute(testClass));
        }

        [Test]
        public void ExecuteUnwrapsTargetInvocationExceptionWithoutLoosingStackTraces()
        {
            var testCommand = Mock.Of<ITestCommand>();
            Mock.Get(testCommand).Setup(x => x.Execute(null)).Callback(
                () =>
                {
                    this.GetType().GetMethod("ThrowInvalidOperationException").Invoke(null, new object[0]);
                });
            var sut = new TargetInvocationExceptionUnwrappingCommand(testCommand);

            var e = Assert.Throws<InvalidOperationException>(() => sut.Execute(null));
            Assert.Contains(
                "TargetInvocationExceptionUnwrappingCommandTest.ThrowInvalidOperationException",
                e.ToString());
        }

        [Test]
        public void ExecuteRecursivelyUnwrapsTargetInvocationException()
        {
            var testCommand = Mock.Of<ITestCommand>();
            Mock.Get(testCommand).Setup(x => x.Execute(null)).Callback(
                () =>
                {
                    this.GetType().GetMethod("ThrowInvalidOperationException2").Invoke(null, new object[0]);
                });
            var sut = new TargetInvocationExceptionUnwrappingCommand(testCommand);

            var e = Assert.Throws<InvalidOperationException>(() => sut.Execute(null));
            Assert.Contains(
                "TargetInvocationExceptionUnwrappingCommandTest.ThrowInvalidOperationException",
                e.ToString());
        }
    }
}
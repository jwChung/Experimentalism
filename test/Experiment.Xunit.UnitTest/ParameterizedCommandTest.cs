namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Reflection;
    using global::Xunit;
    using global::Xunit.Extensions;
    using global::Xunit.Sdk;

    public class ParameterizedCommandTest
    {
        [Fact]
        public void SutIsTestCommand()
        {
            var sut = new ParameterizedCommand(Mocked.Of<ITestCommandContext>());
            Assert.IsAssignableFrom<ITestCommand>(sut);
        }

        [Fact]
        public void InitializeWithNullContextThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new ParameterizedCommand(null));
        }

        [Fact]
        public void InitializeCorrectlyInitializesProperties()
        {
            var method = (MethodInfo)MethodBase.GetCurrentMethod();
            var context = Mocked.Of<ITestCommandContext>(x => x.TestMethod == Reflector.Wrap(method));

            var sut = new ParameterizedCommand(context);

            Assert.Equal(context, sut.TestCommandContext);
            Assert.Equal(method.Name, sut.MethodName);
            Assert.Equal(0, sut.Timeout);
        }

        [Fact(Timeout = 12345)]
        public void InitializeCorrectlyInitializesTimeoutWhenSpecifyingTimeout()
        {
            var method = (MethodInfo)MethodBase.GetCurrentMethod();
            var context = Mocked.Of<ITestCommandContext>(x => x.TestMethod == Reflector.Wrap(method));

            var sut = new ParameterizedCommand(context);

            Assert.Equal(12345, sut.Timeout);
        }

        [Fact]
        public void ExecuteReturnsCorrectResult()
        {
            // Fixture setup
            var method = new Action(() => { }).Method;
            
            var testObject = new object();

            var methodContext = Mocked.Of<ITestMethodContext>(x => x.ActualMethod == method);

            var context = Mocked.Of<ITestCommandContext>(x => x.GetMethodContext(testObject) == methodContext);
            
            var sut = new ParameterizedCommand(context);

            // Exercise system
            var actual = sut.Execute(testObject);

            // Verify outcome
            var passedResult = Assert.IsAssignableFrom<PassedResult>(actual);
            Assert.Equal(method.Name, passedResult.MethodName);
            Assert.Equal(actual.DisplayName, passedResult.DisplayName);
        }

        [Fact]
        public void ExecuteCorrectlyInvokesActualMethod()
        {
            // Fixture setup
            var verified = false;

            var arguments = new object[] { "1", 1 };

            var delegator = new Action<string, int>((x, y) =>
            {
                Assert.Equal(x, arguments[0]);
                Assert.Equal(y, arguments[1]);
                verified = true;
            });

            var testObject = new object();

            var methodContext = Mocked.Of<ITestMethodContext>(
                x => x.ActualMethod == delegator.Method
                && x.ActualObject == delegator.Target);

            var context = Mocked.Of<ITestCommandContext>(x =>
                x.GetMethodContext(testObject) == methodContext
                && x.GetArguments(methodContext) == arguments);

            var sut = new ParameterizedCommand(context);

            // Exercise system
            sut.Execute(testObject);

            // Verify outcome
            Assert.True(verified, "verified");
        }

        [Fact]
        public void ExecuteSetsCorrectDisplayName()
        {
            // Fixture setup
            var arguments = new object[] { "1", 1 };

            var delegator = new Action<string, int>((x, y) => { });

            var testObject = new object();

            var methodContext = Mocked.Of<ITestMethodContext>();

            var context = Mocked.Of<ITestCommandContext>(x =>
                x.TestMethod == Reflector.Wrap(delegator.Method)
                && x.GetMethodContext(testObject) == methodContext
                && x.GetArguments(methodContext) == arguments);

            var sut = new ParameterizedCommand(context);

            var expectecd = new TheoryCommand(Reflector.Wrap(delegator.Method), arguments).DisplayName;

            // Exercise system
            sut.Execute(testObject);

            // Verify outcome
            Assert.Equal(expectecd, sut.DisplayName);
        }
    }
}
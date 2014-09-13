namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Reflection;
    using global::Xunit;
    using global::Xunit.Extensions;
    using global::Xunit.Sdk;

    public class ParameterizedCommand2Test
    {
        [Fact]
        public void SutIsTestCommand()
        {
            var sut = new ParameterizedCommand2(Mocked.Of<ITestCommandContext>());
            Assert.IsAssignableFrom<ITestCommand>(sut);
        }

        [Fact]
        public void InitializeWithNullContextThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new ParameterizedCommand2(null));
        }

        [Fact]
        public void InitializeCorrectlyInitializesProperties()
        {
            var method = (MethodInfo)MethodBase.GetCurrentMethod();
            var context = Mocked.Of<ITestCommandContext>(x => x.ActualMethod == Reflector.Wrap(method));

            var sut = new ParameterizedCommand2(context);

            Assert.Equal(context, sut.TestCommandContext);
            Assert.Equal(method.Name, sut.MethodName);
            Assert.Equal(0, sut.Timeout);
        }

        [Fact(Timeout = 12345)]
        public void InitializeCorrectlyInitializesTimeoutWhenSpecifyingTimeout()
        {
            var method = (MethodInfo)MethodBase.GetCurrentMethod();
            var context = Mocked.Of<ITestCommandContext>(x => x.ActualMethod == Reflector.Wrap(method));

            var sut = new ParameterizedCommand2(context);

            Assert.Equal(12345, sut.Timeout);
        }

        [Fact]
        public void ExecuteReturnsCorrectResult()
        {
            var method = new Action(() => { }).Method;
            var context = Mocked.Of<ITestCommandContext>(x => x.ActualMethod == Reflector.Wrap(method));
            var sut = new ParameterizedCommand2(context);

            var actual = sut.Execute(null);

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

            var methodContext = Mocked.Of<ITestMethodContext>();

            var context = Mocked.Of<ITestCommandContext>(x =>
                x.ActualMethod == Reflector.Wrap(delegator.Method)
                && x.GetMethodContext(delegator.Target) == methodContext
                && x.GetArguments(methodContext) == arguments);

            var sut = new ParameterizedCommand2(context);

            // Exercise system
            sut.Execute(delegator.Target);

            // Verify outcome
            Assert.True(verified, "verified");
        }

        [Test]
        public void ExecuteSetsCorrectDisplayName()
        {
            // Fixture setup
            var delegator = new Action<string, int>((x, y) => { });

            var arguments = new object[] { "1", 1 };

            var methodContext = Mocked.Of<ITestMethodContext>();

            var context = Mocked.Of<ITestCommandContext>(x =>
                x.TestMethod == Reflector.Wrap(delegator.Method)
                && x.GetMethodContext(delegator.Target) == methodContext
                && x.GetArguments(methodContext) == arguments);

            var sut = new ParameterizedCommand2(context);

            var expectecd = new TheoryCommand(Reflector.Wrap(delegator.Method), arguments).DisplayName;

            // Exercise system
            sut.Execute(delegator.Target);

            // Verify outcome
            Assert.Equal(expectecd, sut.DisplayName);
        }
    }
}
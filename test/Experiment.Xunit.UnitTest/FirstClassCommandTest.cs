using System;
using System.Reflection;
using Xunit;
using Xunit.Sdk;

namespace Jwc.Experiment.Xunit
{
    public class FirstClassCommandTest
    {
        [Fact]
        public void SutIsTestCommand()
        {
            var sut = new FirstClassCommand(
                Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()),
                string.Empty,
                () => { });
            Assert.IsAssignableFrom<TestCommand>(sut);
        }

        [Fact]
        public void InitializeWithNullMethodThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new FirstClassCommand(null, string.Empty, () => { }));
        }

        [Fact]
        public void InitializeWithNullParameterDisplayNameThrows()
        {
            Action action = () => { };
            Assert.Throws<ArgumentNullException>(
                () => new FirstClassCommand(
                    Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()),
                    null,
                    action));
        }

        [Fact]
        public void InitializeWithNullActionThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new FirstClassCommand(
                    Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()),
                    string.Empty,
                    null));
        }
        
        [Fact]
        public void MethodIsCorrect()
        {
            var method = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            var sut = new FirstClassCommand(method, string.Empty, () => { });

            var actual = sut.Method;

            Assert.Equal(method, actual);
        }

        [Fact]
        public void DisplayParameterNameIsCorrect()
        {
            string expected = "DisplayParameterName";
            var sut = new FirstClassCommand(
                Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()),
                expected,
                () => { });

            var actual = sut.DisplayParameterName;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ActionIsCorrect()
        {
            var action = new Action(() => { });
            var sut = new FirstClassCommand(
                Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()),
                string.Empty,
                action);

            var actual = sut.Action;

            Assert.Equal(action, actual);
        }

        [Fact]
        public void DisplayNameIsCorrect()
        {
            var method = (MethodInfo)MethodBase.GetCurrentMethod();
            string displayParameterName = "Anonymous Parameters";
            var sut = new FirstClassCommand(
                Reflector.Wrap(method),
                displayParameterName,
                () => { });
            var expected = method.ReflectedType.FullName + "." + method.Name + "(" + displayParameterName + ")";

            var actual = sut.DisplayName;

            Assert.Equal(expected, actual);
        }

        [Fact(DisplayName = "CustomDisplayName")]
        public void DisplayNameIsCorrectWhenInitializedWithDisplayName()
        {
            var method = (MethodInfo)MethodBase.GetCurrentMethod();
            var sut = new FirstClassCommand(
                Reflector.Wrap(method),
                string.Empty,
                () => { });
            string expected = "CustomDisplayName()";

            var actual = sut.DisplayName;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TimeoutIsCorrect()
        {
            var sut = new FirstClassCommand(
                Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()),
                string.Empty,
                () => { });
            var actual = sut.Timeout;
            Assert.Equal(0, actual);
        }

        [Fact(Timeout = 12345)]
        public void TimeoutIsCorrectWhenInitializedWithTimeout()
        {
            var sut = new FirstClassCommand(
                Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()),
                string.Empty,
                () => { });
            var actual = sut.Timeout;
            Assert.Equal(12345, actual);
        }

        [Fact]
        public void ShouldCreateInstanceIsCorrect()
        {
            var sut = new FirstClassCommand(
                Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()),
                string.Empty,
                () => { });
            var actual = sut.ShouldCreateInstance;
            Assert.False(actual, "ShouldCreateInstance");
        }

        [Fact]
        public void ExecuteCallsAction()
        {
            // Fixture setup
            var verified = false;
            Action action = () => verified = true;
            var sut = new FirstClassCommand(
                Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()),
                string.Empty,
                action);

            // Exercise system
            Assert.DoesNotThrow(() => sut.Execute(null));

            // Verify outcome
            Assert.True(verified, "verified.");
        }

        [Fact]
        public void ExecuteReturnsCorrectResult()
        {
            var method = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            var sut = new FirstClassCommand(
                method,
                string.Empty,
                () => { });

            var actual = sut.Execute(null);

            var result = Assert.IsType<PassedResult>(actual);
            Assert.Equal(method.Name, result.MethodName);
            Assert.Equal(sut.DisplayName, result.DisplayName);
        }
    }
}
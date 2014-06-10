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
                new Action(() => { }),
                new object[0]);
            Assert.IsAssignableFrom<TestCommand>(sut);
        }

        [Fact]
        public void InitializeWithNullMethodThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new FirstClassCommand(null, string.Empty, new Action(() => { }), new object[0]));
        }

        [Fact]
        public void InitializeWithNullParameterDisplayNameThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new FirstClassCommand(
                    Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()),
                    null,
                    new Action(() => { }),
                    new object[0]));
        }

        [Fact]
        public void InitializeWithNullDelegateThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new FirstClassCommand(
                    Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()),
                    string.Empty,
                    null,
                    new object[0]));
        }

        [Fact]
        public void InitializeWithNullArgumentsThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new FirstClassCommand(
                    Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()),
                    string.Empty,
                    new Action(() => { }),
                    null));
        }

        [Fact]
        public void MethodIsCorrect()
        {
            var method = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            var sut = new FirstClassCommand(method, string.Empty, new Action(() => { }), new object[0]);

            var actual = sut.Method;

            Assert.Equal(method, actual);
        }

        [Fact]
        public void TestParameterNameIsCorrect()
        {
            string expected = "DisplayParameterName";
            var sut = new FirstClassCommand(
                Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()),
                expected,
                new Action(() => { }),
                new object[0]);

            var actual = sut.DisplayParameterName;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DelegateIsCorrect()
        {
            var @delegate = new Action(() => { });
            var sut = new FirstClassCommand(
                Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()),
                string.Empty,
                @delegate,
                new object[0]);

            var actual = sut.Delegate;

            Assert.Equal(@delegate, actual);
        }

        [Fact]
        public void ArgumentsIsCorrect()
        {
            var arguments = new[] { 1, new object(), "string" };
            var sut = new FirstClassCommand(
                Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()),
                string.Empty,
                new Action(() => { }),
                arguments);

            var actual = sut.Arguments;

            Assert.Equal(arguments, actual);
        }

        [Fact]
        public void DisplayNameIsCorrect()
        {
            var method = (MethodInfo)MethodBase.GetCurrentMethod();
            string parameterDisplayName = "Anonymous Parameters";
            var sut = new FirstClassCommand(
                Reflector.Wrap(method),
                parameterDisplayName,
                new Action<int, object, string, Type>((a, b, c, d) => { }),
                new[] { 1, new object(), "string", null });
            var expected = method.ReflectedType.FullName + "." + method.Name + "(" + parameterDisplayName + ")";

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
                new Action<int, object, string, Type>((a, b, c, d) => { }),
                new[] { 1, new object(), "string", null });
            var expected = "CustomDisplayName()";

            var actual = sut.DisplayName;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TimeoutIsCorrect()
        {
            var sut = new FirstClassCommand(
                Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()),
                string.Empty,
                new Action(() => { }),
                new object[0]);
            var actual = sut.Timeout;
            Assert.Equal(0, actual);
        }

        [Fact(Timeout = 12345)]
        public void TimeoutIsCorrectWhenInitializedWithTimeout()
        {
            var sut = new FirstClassCommand(
                Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()),
                string.Empty,
                new Action(() => { }),
                new object[0]);
            var actual = sut.Timeout;
            Assert.Equal(12345, actual);
        }

        [Fact]
        public void ShouldCreateInstanceIsCorrect()
        {
            var sut = new FirstClassCommand(
                Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()),
                string.Empty,
                new Action(() => { }),
                new object[0]);
            var actual = sut.ShouldCreateInstance;
            Assert.False(actual, "ShouldCreateInstance");
        }

        [Fact]
        public void ExecuteCallsDelegate()
        {
            // Fixture setup
            var arguments = new object[] { 1, "string" };
            var verified = false;
            Action<int, string> @delegate = (x, y) =>
            {
                Assert.Equal(1, x);
                Assert.Equal("string", y);
                verified = true;
            };
            var sut = new FirstClassCommand(
                Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()),
                string.Empty,
                @delegate,
                arguments);

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
                new Action(() => { }),
                new object[0]);

            var actual = sut.Execute(null);

            var result = Assert.IsType<PassedResult>(actual);
            Assert.Equal(method.Name, result.MethodName);
            Assert.Equal(sut.DisplayName, result.DisplayName);
        }
    }
}
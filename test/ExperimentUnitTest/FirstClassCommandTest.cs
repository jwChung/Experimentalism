using System;
using System.Reflection;
using Xunit;
using Xunit.Sdk;

namespace Jwc.Experiment
{
    public class FirstClassCommandTest
    {
        [Fact]
        public void SutIsTestCommand()
        {
            var sut = new FirstClassCommand(
                Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()),
                new Action(() => { }),
                new object[0]);
            Assert.IsAssignableFrom<TestCommand>(sut);
        }

        [Fact]
        public void InitializeWithNullMethodThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new FirstClassCommand(null, new Action(() => { }), new object[0]));
        }

        [Fact]
        public void InitializeWithNullDelegateThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new FirstClassCommand(
                    Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()),
                    null,
                    new object[0]));
        }

        [Fact]
        public void InitializeWithNullArgumentsThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new FirstClassCommand(
                    Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()),
                    new Action(() => { }),
                    null));
        }

        [Fact]
        public void MethodIsCorrect()
        {
            var method = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            var sut = new FirstClassCommand(method, new Action(() => { }), new object[0]);

            var actual = sut.Method;

            Assert.Equal(method, actual);
        }

        [Fact]
        public void DelegateIsCorrect()
        {
            var @delegate = new Action(() => { });
            var sut = new FirstClassCommand(
                Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()),
                @delegate,
                new object[0]);

            var actual = sut.Delegate;

            Assert.Equal(@delegate, actual);
        }

        [Fact]
        public void ArgumentsIsCorrect()
        {
            var arguments = new[]{1, new object(), "string"};
            var sut = new FirstClassCommand(
                Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()),
                new Action(() => { }),
                arguments);

            var actual = sut.Arguments;

            Assert.Equal(arguments, actual);
        }

        [Fact]
        public void DisplayNameIsCorrect()
        {
            var arguments = new[] { 1, new object(), "string", null };
            var sut = new FirstClassCommand(
                Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()),
                new Action<int, object, string, Type>((a, b, c, d) => { }),
                arguments);
            var exptected = "Jwc.Experiment.FirstClassCommandTest.DisplayNameIsCorrect" +
                            "(Int32: \"1\", Object: \"System.Object\", String: \"string\", Type: NULL)";

            var actual = sut.DisplayName;

            Assert.Equal(exptected, actual);
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
                new Action(() => { }),
                new object[0]);

            var actual = sut.Execute(null);

            var result = Assert.IsType<PassedResult>(actual);
            Assert.Equal(method.Name, result.MethodName);
            Assert.Equal(sut.DisplayName, result.DisplayName);
        }

        [Fact]
        public void TimeoutIsCorrect()
        {
            var sut = new FirstClassCommand(
                Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()),
                new Action(() => { }),
                new object[0]);
            var actual = sut.Timeout;
            Assert.Equal(0, actual);
        }

        [Fact]
        public void ShouldCreateInstanceIsCorrect()
        {
            var sut = new FirstClassCommand(
                Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()),
                new Action(() => { }),
                new object[0]);
            var actual = sut.ShouldCreateInstance;
            Assert.False(actual, "ShouldCreateInstance");
        }
    }
}
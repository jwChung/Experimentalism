using System;
using System.Reflection;
using Xunit;
using Xunit.Sdk;

namespace Jwc.Experiment
{
    public class FirstClassCommandTest
    {
        [Fact]
        public void SutIsFactCommand()
        {
            var sut = new FirstClassCommand(
                Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()),
                new Action(() => { }).Method,
                new object[0]);
            Assert.IsAssignableFrom<FactCommand>(sut);
        }

        [Fact]
        public void InitializeWithNullMethodThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new FirstClassCommand(null, new Action(() => { }).Method, new object[0]));
        }

        [Fact]
        public void InitializeWithNullTestCaseThrows()
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
                    new Action(() => { }).Method,
                    null));
        }

        [Fact]
        public void DeclaredMethodIsCorrect()
        {
            var method = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            var sut = new FirstClassCommand(method, new Action(() => { }).Method, new object[0]);

            var actual = sut.DeclaredMethod;

            Assert.Equal(method, actual);
        }

        [Fact]
        public void TestMethodIsCorrect()
        {
            var testCase = new Action(() => { }).Method;
            var sut = new FirstClassCommand(
                Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()),
                testCase,
                new object[0]);

            var actual = sut.TestMethod;

            Assert.Equal(testCase, actual);
        }

        [Fact]
        public void ArgumentsIsCorrect()
        {
            var arguments = new[]{1, new object(), "string"};
            var sut = new FirstClassCommand(
                Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()),
                new Action(() => { }).Method,
                arguments);

            var actual = sut.Arguments;

            Assert.Equal(arguments, actual);
        }

        [Fact]
        public void DisplayNameIsWellFormatted()
        {
            var arguments = new[] { 1, new object(), "string", null };
            var sut = new FirstClassCommand(
                Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()),
                new Action<int, object, string, Type>((a, b, c, d) => { }).Method,
                arguments);
            var exptected = "Jwc.Experiment.FirstClassCommandTest.DisplayNameIsWellFormatted" +
                            "(Int32: \"1\", Object: \"System.Object\", String: \"string\", Type: NULL)";

            var actual = sut.DisplayName;

            Assert.Equal(exptected, actual);
        }

        [Fact]
        public void ExecuteCallsTestCase()
        {
            // Fixture setup
            var arguments = new object[] { 1, "string" };
            var testCase = new Action<int, string>(StaticDelegateMethod).Method;
            var sut = new FirstClassCommand(
                Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()),
                testCase,
                arguments);

            // Exercise system
            Assert.DoesNotThrow(() => sut.Execute(null));

            // Verify outcome
            Assert.True(_verified, "verified.");

            // Fixture teardown
            _verified = false;
        }

        [Fact]
        public void ExecuteReturnsCorrectResult()
        {
            var method = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            var sut = new FirstClassCommand(
                method,
                new Action(() => { }).Method,
                new object[0]);

            var actual = sut.Execute(null);

            var result = Assert.IsType<PassedResult>(actual);
            Assert.Equal(method.Name, result.MethodName);
            Assert.Equal(sut.DisplayName, result.DisplayName);
        }

        private static bool _verified;
        private static void StaticDelegateMethod(int arg1, string arg2)
        {
            Assert.Equal(1, arg1);
            Assert.Equal("string", arg2);
            _verified = true;
        }
    }
}
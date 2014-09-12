﻿namespace Jwc.Experiment.Xunit
{
    using System;
    using Ploeh.Albedo;
    using global::Xunit;
    using global::Xunit.Extensions;
    using global::Xunit.Sdk;

    public class ParameterizedCommandTest
    {
        [Fact]
        public void SutIsTestCommand()
        {
            var sut = new ParameterizedCommand(Mocked.Of<ITestCommandInfo>());
            Assert.IsAssignableFrom<ITestCommand>(sut);
        }

        [Fact]
        public void InitializeWithNullTestCommandInfoThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new ParameterizedCommand(null));
        }

        [Fact]
        public void MethodNameIsCorrect()
        {
            var method = new Methods<ParameterizedCommandTest>().Select(x => x.MethodNameIsCorrect());
            var sut = new ParameterizedCommand(
                Mocked.Of<ITestCommandInfo>(x => x.TestMethod == Reflector.Wrap(method)));

            var actual = sut.MethodName;

            Assert.Equal(method.Name, actual);
        }

        [Fact]
        public void TestCommandInfoIsCorrect()
        {
            var expected = Mocked.Of<ITestCommandInfo>();
            var sut = new ParameterizedCommand(expected);

            var actual = sut.TestCommandInfo;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ExecuteReturnsCorrectResult()
        {
            bool verifid = false;
            var arguments = new object[] { 123, "Foo" };
            var @delegate = new Action<int, string>((x, y) =>
            {
                Assert.Equal(arguments[0], x);
                Assert.Equal(arguments[1], y);
                verifid = true;
            });
            var testCommandInfo = Mocked.Of<ITestCommandInfo>(
                c => c.TestMethod == Reflector.Wrap(@delegate.Method)
                && c.GetArguments(@delegate.Target) == arguments);
            var sut = new ParameterizedCommand(testCommandInfo);

            var actual = sut.Execute(@delegate.Target);

            Assert.True(verifid);
            var passedResult = Assert.IsAssignableFrom<PassedResult>(actual);
            Assert.Equal(@delegate.Method.Name, passedResult.MethodName);
            Assert.Equal(sut.DisplayName, passedResult.DisplayName);
        }

        [Fact]
        public void ExecuteSetsCorrectDisplayName()
        {
            var arguments = new object[] { 123, "Foo", new object() };
            var @delegate = new Action<int, string, object>((x, y, z) => { });
            var testCommandInfo = Mocked.Of<ITestCommandInfo>(
                c => c.TestMethod == Reflector.Wrap(@delegate.Method)
                && c.GetArguments(@delegate.Target) == arguments);
            var sut = new ParameterizedCommand(testCommandInfo);
            var expected = new TheoryCommand(Reflector.Wrap(@delegate.Method), arguments).DisplayName;
            Assert.NotEqual(expected, sut.DisplayName);

            sut.Execute(@delegate.Target);

            Assert.Equal(expected, sut.DisplayName);
        }
    }
}

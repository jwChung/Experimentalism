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
                new Action(() => { }),
                new object[0]);
            Assert.IsAssignableFrom<FactCommand>(sut);
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
        public void DisplayNameIsWellFormatted()
        {
            var arguments = new[] { 1, new object(), "string", null };
            var sut = new FirstClassCommand(
                Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()),
                new Action<int, object, string, Type>((a, b, c, d) => { }),
                arguments);
            var exptected = "Jwc.Experiment.FirstClassCommandTest.DisplayNameIsWellFormatted" +
                            "(Int32: \"1\", Object: \"System.Object\", String: \"string\", Type: NULL)";

            var actual = sut.DisplayName;

            Assert.Equal(exptected, actual);
        }
    }
}
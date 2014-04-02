﻿using System;
using System.Reflection;
using Xunit;
using Xunit.Sdk;

namespace Jwc.Experiment
{
    public class TestCaseTest
    {
        [Fact]
        public void SutIsTestCase()
        {
            var sut = new TestCase(() => { });
            Assert.IsAssignableFrom<ITestCase>(sut);
        }

        [Fact]
        public void InitializeWithNullActionThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new TestCase((Action)null));
        }

        [Fact]
        public void InitializeWithNullFuncThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new TestCase((Func<object>)null));
        }

        [Fact]
        public void InitializeWithCompositeActionThrows()
        {
            Action action = () => { };
            action += () => { };
            Assert.Throws<ArgumentException>(() => new TestCase(action));
        }

        [Fact]
        public void InitializeWithCompositeFuncThrows()
        {
            Func<object> func = () => null;
            func += () => null;
            Assert.Throws<ArgumentException>(() => new TestCase(func));
        }

        [Fact]
        public void DelegateIsCorrectWhenInitializedWithAction()
        {
            Action action = () => { };
            var sut = new TestCase(action);

            var actual = sut.Delegate;

            Assert.Equal(action, actual);
        }

        [Fact]
        public void DelegateIsCorrectWhenInitializedWithFunc()
        {
            Func<object> func = () => null;
            var sut = new TestCase(func);

            var actual = sut.Delegate;

            Assert.Equal(func, actual);
        }

        [Fact]
        public void ConvertNullMethodToTestCommandThrowsIfDeclaredOnSut()
        {
            var sut = new TestCase(() => { });
            ITestFixtureFactory dummyFixtureFactory = null;
            Assert.Throws<ArgumentNullException>(() => sut.ConvertToTestCommand(null, dummyFixtureFactory));
        }

        [Fact]
        public void ConvertToTestCommandReturnsCorrectTestCommand()
        {
            Action action = () => { };
            var sut = new TestCase(action);
            var method = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());

            var actual = sut.ConvertToTestCommand(method, null);

            var command = Assert.IsType<FirstClassCommand>(actual);
            Assert.Equal(method, command.DeclaredMethod);
            Assert.Equal(action.Method, command.TestMethod);
            Assert.Empty(command.Arguments);
        }

        [Fact]
        public void SutOfTIsTestCase()
        {
            var sut = new TestCase<object>(x => { });
            Assert.IsAssignableFrom<ITestCase>(sut);
        }

        [Fact]
        public void InitializeWithNullActionOfTThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new TestCase<object>((Action<object>)null));
        }

        [Fact]
        public void InitializeWithNullFuncOfTThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new TestCase<string>((Func<string, object>)null));
        }

        [Fact]
        public void InitializeWithCompositeActionOfTThrows()
        {
            Action<object> action = x => { };
            action += x => { };
            Assert.Throws<ArgumentException>(() => new TestCase<object>(action));
        }

        [Fact]
        public void InitializeWithCompositeFuncOfTThrows()
        {
            Func<object, object> func = x => null;
            func += x => null;
            Assert.Throws<ArgumentException>(() => new TestCase<object>(func));
        }

        [Fact]
        public void DelegateIsCorrectWhenInitializedWithActionOfT()
        {
            Action<object> action = x => { };
            var sut = new TestCase<object>(action);

            var actual = sut.Delegate;

            Assert.Equal(action, actual);
        }

        [Fact]
        public void DelegateIsCorrectWhenInitializedWithFuncOfT()
        {
            Func<int, object> func = x => null;
            var sut = new TestCase<int>(func);

            var actual = sut.Delegate;

            Assert.Equal(func, actual);
        }

        [Fact]
        public void ConvertNullMethodToTestCommandThrowsIfDeclaredOnSutOfT()
        {
            var sut = new TestCase<object>(x => { });
            Assert.Throws<ArgumentNullException>(() => sut.ConvertToTestCommand(null, new FakeFixtureFactory()));
        }

        [Fact]
        public void ConvertToTestCommandWithNullFixtureFactoryThrowsIfDeclaredOnSutOfT()
        {
            var sut = new TestCase<object>(x => { });
            var dummyMethod = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            Assert.Throws<ArgumentNullException>(() => sut.ConvertToTestCommand(dummyMethod, null));
        }

        [Fact]
        public void ConvertToTestCommandReturnsCorrectTestCommandIfDeclaredOnSutOfT()
        {
            Action<int> action = x => { };
            var sut = new TestCase<int>(action);
            var method = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            var testFixture = new FakeTestFixture();
            var fixtureFactory = new FakeFixtureFactory
            {
                OnCreate = x =>
                {
                    Assert.Equal(action.Method, x);
                    return testFixture;
                }
            };

            var actual = sut.ConvertToTestCommand(method, fixtureFactory);

            var command = Assert.IsType<FirstClassCommand>(actual);
            Assert.Equal(method, command.DeclaredMethod);
            Assert.Equal(action.Method, command.TestMethod);
            Assert.Equal(new object[] { testFixture.IntValue }, command.Arguments);
        }
    }
}
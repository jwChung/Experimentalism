using System;
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
        public void DelegateIsCorrectWhenInitializedByAction()
        {
            Action action = () => { };
            var sut = new TestCase(action);

            var actual = sut.Delegate;

            Assert.Equal(action, actual);
        }

        [Fact]
        public void DelegateIsCorrectWhenInitializedByFunc()
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
            Assert.Throws<ArgumentNullException>(
                () => sut.ConvertToTestCommand(null, new DelegatingTestFixtureFactory()));
        }

        [Fact]
        public void ConvertToTestCommandReturnsCorrectTestCommand()
        {
            Action action = () => { };
            var sut = new TestCase(action);
            var method = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());

            var actual = sut.ConvertToTestCommand(method, null);

            var command = Assert.IsType<FirstClassCommand>(actual);
            Assert.Equal(method, command.Method);
            Assert.Equal(action, command.Delegate);
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
        public void DelegateIsCorrectWhenInitializedByActionOfT()
        {
            Action<object> action = x => { };
            var sut = new TestCase<object>(action);

            var actual = sut.Delegate;

            Assert.Equal(action, actual);
        }

        [Fact]
        public void DelegateIsCorrectWhenInitializedByFuncOfT()
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
            Assert.Throws<ArgumentNullException>(
                () => sut.ConvertToTestCommand(null, new DelegatingTestFixtureFactory()));
        }

        [Fact]
        public void ConvertToTestCommandWithNullFixtureFactoryThrowsIfDeclaredOnSutOfT()
        {
            var sut = new TestCase<object>(x => { });
            var dummyMethod = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            Assert.Throws<ArgumentNullException>(
                () => sut.ConvertToTestCommand(dummyMethod, null));
        }

        [Fact]
        public void ConvertToTestCommandReturnsCorrectTestCommandIfDeclaredOnSutOfT()
        {
            Action<int> action = x => { };
            var sut = new TestCase<int>(action);
            var method = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            var fixture = new FakeTestFixture();
            var fixtureFactory = new DelegatingTestFixtureFactory
            {
                OnCreate = mi => { Assert.Equal(action.Method, mi); return fixture; }
            };

            var actual = sut.ConvertToTestCommand(method, fixtureFactory);

            var command = Assert.IsType<FirstClassCommand>(actual);
            Assert.Equal(method, command.Method);
            Assert.Equal(action, command.Delegate);
            Assert.Equal(new[] { fixture.Create(typeof(int)) }, command.Arguments);
        }

        [Fact]
        public void ConvertToTestCommandCreatesFixtureOnlyOnceIfDeclaredOnSutOfT()
        {
            var sut = new TestCase<int>(x => { });
            int createdCount = 0;
            var fixtureFactory = new DelegatingTestFixtureFactory
            {
                OnCreate = mi => { createdCount++; return new FakeTestFixture(); }
            };
            var dummyMethod = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());

            sut.ConvertToTestCommand(dummyMethod, fixtureFactory);

            Assert.Equal(1, createdCount);
        }

        [Fact]
        public void SutOfT1T2IsTestCase()
        {
            var sut = new TestCase<object, int>((x, y) => { });
            Assert.IsAssignableFrom<ITestCase>(sut);
        }

        [Fact]
        public void InitializeWithNullActionOfT1T2Throws()
        {
            Assert.Throws<ArgumentNullException>(() => new TestCase<object, int>((Action<object, int>)null));
        }

        [Fact]
        public void InitializeWithNullFuncOfT1T2Throws()
        {
            Assert.Throws<ArgumentNullException>(() => new TestCase<string, int>((Func<string, int, object>)null));
        }

        [Fact]
        public void InitializeWithCompositeActionOfT1T2Throws()
        {
            Action<object, int> action = (x, y) => { };
            action += (x, y) => { };
            Assert.Throws<ArgumentException>(() => new TestCase<object, int>(action));
        }

        [Fact]
        public void InitializeWithCompositeFuncOfT1T2Throws()
        {
            Func<object, int, object> func = (x, y) => null;
            func += (x, y) => null;
            Assert.Throws<ArgumentException>(() => new TestCase<object, int>(func));
        }

        [Fact]
        public void DelegateIsCorrectWhenInitializedByActionOfT1T2()
        {
            Action<object, int> action = (x, y) => { };
            var sut = new TestCase<object, int>(action);

            var actual = sut.Delegate;

            Assert.Equal(action, actual);
        }

        [Fact]
        public void DelegateIsCorrectWhenInitializedByFuncOfT1T2()
        {
            Func<int, string, object> func = (x, y) => null;
            var sut = new TestCase<int, string>(func);

            var actual = sut.Delegate;

            Assert.Equal(func, actual);
        }

        [Fact]
        public void ConvertNullMethodToTestCommandThrowsIfDeclaredOnSutOfT1T2()
        {
            var sut = new TestCase<object, int>((x, y) => { });
            Assert.Throws<ArgumentNullException>(
                () => sut.ConvertToTestCommand(null, new DelegatingTestFixtureFactory()));
        }

        [Fact]
        public void ConvertToTestCommandWithNullFixtureFactoryThrowsIfDeclaredOnSutOfT1T2()
        {
            var sut = new TestCase<object, int>((x, y) => { });
            var dummyMethod = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            Assert.Throws<ArgumentNullException>(
                () => sut.ConvertToTestCommand(dummyMethod, null));
        }

        [Fact]
        public void ConvertToTestCommandReturnsCorrectTestCommandIfDeclaredOnSutOfT1T2()
        {
            Action<int, string> action = (x, y) => { };
            var sut = new TestCase<int, string>(action);
            var method = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            var fixture = new FakeTestFixture();
            var fixtureFactory = new DelegatingTestFixtureFactory
            {
                OnCreate = mi => { Assert.Equal(action.Method, mi); return fixture; }
            };

            var actual = sut.ConvertToTestCommand(method, fixtureFactory);

            var command = Assert.IsType<FirstClassCommand>(actual);
            Assert.Equal(method, command.Method);
            Assert.Equal(action, command.Delegate);
            Assert.Equal(
                new[] { fixture.Create(typeof(int)), fixture.Create(typeof(string)) },
                command.Arguments);
        }

        [Fact]
        public void ConvertToTestCommandCreatesFixtureOnlyOnceIfDeclaredOnSutOfT1T2()
        {
            var sut = new TestCase<int, string>((x, y) => { });
            int createdCount = 0;
            var fixtureFactory = new DelegatingTestFixtureFactory
            {
                OnCreate = mi => { createdCount++; return new FakeTestFixture(); }
            };
            var dummyMethod = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());

            sut.ConvertToTestCommand(dummyMethod, fixtureFactory);

            Assert.Equal(1, createdCount);
        }
    }
}
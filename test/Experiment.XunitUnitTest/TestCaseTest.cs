using System;
using System.Reflection;
using Xunit;
using Xunit.Sdk;

namespace Jwc.Experiment.Xunit
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
        public void InitializeWithNullDelegateThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new TestCase((Delegate)null));
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
        public void InitializeWithCompositeDelegateThrows()
        {
            Func<object> func = () => null;
            func += () => null;
            Assert.Throws<ArgumentException>(() => new TestCase((Delegate)func));
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
        public void DelegateIsCorrectWhenInitializedWithDelegate()
        {
            Delegate @delegate = new Func<object>(() => null);
            var sut = new TestCase(@delegate);

            var actual = sut.Delegate;

            Assert.Equal(@delegate, actual);
        }

        [Fact]
        public void ConvertNullMethodToTestCommandThrows()
        {
            var sut = new TestCase(() => { });
            Assert.Throws<ArgumentNullException>(
                () => sut.ConvertToTestCommand(null, new DelegatingTestFixtureFactory()));
        }

        [Fact]
        public void ConvertToTestCommandWithNullFixtureFactoryThrows()
        {
            var sut = new TestCase(() => null);
            var dummyMethod = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            Assert.Throws<ArgumentNullException>(
                () => sut.ConvertToTestCommand(dummyMethod, null));
        }

        [Fact]
        public void ConvertNonParameterizedDelegateToTestCommandReturnsCorrectTestCommand()
        {
            var sut = new TestCase(() => { });
            var method = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());

            var actual = sut.ConvertToTestCommand(method, new DelegatingTestFixtureFactory());

            var command = Assert.IsType<FirstClassCommand>(actual);
            Assert.Equal(method, command.Method);
            Assert.Equal(string.Empty, command.TestParameterName);
            Assert.Equal(sut.Delegate, command.Delegate);
            Assert.Empty(command.Arguments);
        }

        [Fact]
        public void ConvertParameterizedDelegateToTestCommandReturnsCorrectTestCommand()
        {
            // Fixture setup
            Action<int, string, object> @delegate = (x, y, z) => { };
            var sut = new TestCase(@delegate);

            var fixture = new DelegatingTestFixture
            {
                OnCreate = r =>
                {
                    var type = r as Type;
                    if (type == typeof(string))
                        return "anonymous";
                    if (type == typeof(int))
                        return 123;
                    if (type == typeof(object))
                        return null;

                    throw new NotSupportedException();
                }
            };

            var fixtureFactory = new DelegatingTestFixtureFactory
            {
                OnCreate = mi =>
                {
                    Assert.Equal(@delegate.Method, mi);
                    return fixture;
                }
            };

            var method = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            string testParameterName = "Int32: 123, String: anonymous, Object: (null)";
            var arguments = new object[] { 123, "anonymous", null };

            // Exercise system
            var actual = sut.ConvertToTestCommand(method, fixtureFactory);

            // Verify outcome
            var command = Assert.IsType<FirstClassCommand>(actual);

            Assert.Equal(method, command.Method);
            Assert.Equal(testParameterName, command.TestParameterName);
            Assert.Equal(@delegate, command.Delegate);
            Assert.Equal(arguments, command.Arguments);
        }

        [Fact]
        public void ConvertNonParameterizedDelegateToTestCommandDoesNotCreateFixture()
        {
            // Fixture setup
            var sut = new TestCase(() => "anonymous");
            var fixtureFactory = new DelegatingTestFixtureFactory
            {
                OnCreate = mi => { throw new InvalidOperationException(); }
            };
            var dummyMethod = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());

            // Exercise system and Verify outcome
            Assert.DoesNotThrow(() => sut.ConvertToTestCommand(dummyMethod, fixtureFactory));
        }

        [Fact]
        public void ConvertParameterizedDelegateToTestCommandCreatesFixtureOnlyOnce()
        {
            var sut = new TestCase(new Action<int, string>((x, y) => { }));
            int createdCount = 0;
            var fixtureFactory = new DelegatingTestFixtureFactory
            {
                OnCreate = mi =>
                {
                    createdCount++;
                    return new FakeTestFixture();
                }
            };
            var dummyMethod = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());

            sut.ConvertToTestCommand(dummyMethod, fixtureFactory);

            Assert.Equal(1, createdCount);
        }
    }
}
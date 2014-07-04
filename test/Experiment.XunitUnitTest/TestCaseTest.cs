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
        public void InitializeWithNullDelegateAndDisplayParameterNameThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new TestCase(null, string.Empty));
        }

        [Fact]
        public void InitializeWithNullDisplayParameterNameThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new TestCase(new Action(() => { }), null));
        }

        [Fact]
        public void DisplayParameterNameIsCorrectWhenInitializedWithAction()
        {
            var sut = new TestCase(() => { });
            var actual = sut.DisplayParameterName;
            Assert.Null(actual);
        }

        [Fact]
        public void DisplayParameterNameIsCorrectWhenInitializedWithFunc()
        {
            var sut = new TestCase(() => null);
            var actual = sut.DisplayParameterName;
            Assert.Null(actual);
        }

        [Fact]
        public void DisplayParameterNameIsCorrectWhenInitializedWithDelegate()
        {
            Delegate @delegate = new Func<object>(() => null);
            var sut = new TestCase(@delegate);

            var actual = sut.DisplayParameterName;

            Assert.Null(actual);
        }

        [Fact]
        public void DisplayParameterNameIsCorrectWhenInitializedWithDisplayParameterNameAndDelegate()
        {
            string displayParameterName = "anonymous";
            var sut = new TestCase(new Func<object>(() => null), displayParameterName);

            var actual = sut.DisplayParameterName;

            Assert.Equal(displayParameterName, actual);
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
        public void DelegateIsCorrectWhenInitializedWithDisplayParameterNameAndDelegate()
        {
            Delegate @delegate = new Func<object>(() => null);
            var sut = new TestCase(@delegate, "anonymous");

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

            var command = Assert.IsType<FirstClassCommand>(
               Assert.IsType<TargetInvocationExceptionUnwrappingCommand>(actual).TestCommand);
            Assert.Equal(method, command.Method);
            Assert.Equal(string.Empty, command.DisplayParameterName);
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
            string displayParameterName = "Int32: 123, String: anonymous, Object: (null)";

            // Exercise system
            var actual = sut.ConvertToTestCommand(method, fixtureFactory);

            // Verify outcome
            var command = Assert.IsType<FirstClassCommand>(
               Assert.IsType<TargetInvocationExceptionUnwrappingCommand>(actual).TestCommand);

            Assert.Equal(method, command.Method);
            Assert.Equal(displayParameterName, command.DisplayParameterName);
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

        [Fact]
        public void ConvertNonParameterizedDelegateToTestCommandReturnsTestCommandReflectingCorrectDisplayParameterName()
        {
            string displayParameterName = "anonymous";
            var sut = new TestCase(new Func<object>(() => null), displayParameterName);
            var method = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());

            var actual = sut.ConvertToTestCommand(method, new DelegatingTestFixtureFactory());

            var command = Assert.IsType<FirstClassCommand>(
                Assert.IsType<TargetInvocationExceptionUnwrappingCommand>(actual).TestCommand);
            Assert.Equal(displayParameterName, command.DisplayParameterName);
        }

        [Fact]
        public void ConvertParameterizedDelegateToTestCommandReturnsTestCommandReflectingCorrectDisplayParameterName()
        {
            string displayParameterName = "anonymous";
            var sut = new TestCase(new Action<int>(x => { }), displayParameterName);
            var method = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            var testFixtureFactory = new DelegatingTestFixtureFactory { OnCreate = m => new FakeTestFixture() };

            var actual = sut.ConvertToTestCommand(method, testFixtureFactory);

            var command = Assert.IsType<FirstClassCommand>(
                Assert.IsType<TargetInvocationExceptionUnwrappingCommand>(actual).TestCommand);
            Assert.Equal(displayParameterName, command.DisplayParameterName);
        }

        [Fact]
        public void ConvertParameterizedDelegateToTestCommandReturnsTestCommandWithCorrectDelegate()
        {
            bool verifyMock = false;
            var fixture = new DelegatingTestFixture { OnCreate = r => 123 };
            var sut = new TestCase(new Action<int>(x => { Assert.Equal(123, x); verifyMock = true; }));
            var factory = new DelegatingTestFixtureFactory { OnCreate = m => fixture };

            var actual = sut.ConvertToTestCommand(
                Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()), factory);

            var command = Assert.IsType<FirstClassCommand>(
                Assert.IsType<TargetInvocationExceptionUnwrappingCommand>(actual).TestCommand);
            command.Action.Invoke();
            Assert.True(verifyMock);
        }

        [Fact]
        public void ConvertActionDelegateToTestCommandReturnsTestCommandWithCorrectDelegate()
        {
            Delegate @delegate = new Action(() => { });
            var sut = new TestCase(@delegate);

            var actual = sut.ConvertToTestCommand(
                Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()),
                new DelegatingTestFixtureFactory());

            var command = Assert.IsType<FirstClassCommand>(
                Assert.IsType<TargetInvocationExceptionUnwrappingCommand>(actual).TestCommand);
            Assert.Equal(@delegate, command.Action);
        }
    }
}
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
            var sut = TestCase.New(() => { });
            Assert.IsAssignableFrom<ITestCase>(sut);
        }

        [Fact]
        public void DelegateIsCorrectWhenInitializedWithNoArguments()
        {
            Action expected = () => { };
            var sut = (TestCase)TestCase.New(expected);

            var actual = sut.Delegate;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DelegateIsCorrectWhenInitializedWithOneArgument()
        {
            Action<object> expected = x => { };
            var sut = (TestCase)TestCase.New(null, expected);

            var actual = sut.Delegate;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ArgumentsIsCorrectWhenInitializedWithNoArguments()
        {
            var sut = (TestCase)TestCase.New(() => { });
            var actual = sut.Arguments;
            Assert.Empty(actual);
        }

        [Fact]
        public void ArgumentsIsCorrectWhenInitializedWithOneArgument()
        {
            var expected = new object();
            var sut = (TestCase)TestCase.New(expected, x => { });

            var actual = sut.Arguments;

            Assert.Equal(new[] { expected }, actual);
        }

        [Fact]
        public void InitializeWithNullDelegateThrows()
        {
            Assert.Throws<ArgumentNullException>(() => TestCase.New(null));
        }

        [Fact]
        public void InitializeWithNullDelegateOfTArgThrows()
        {
            Assert.Throws<ArgumentNullException>(() => TestCase.New<object>(null, null));
        }

        [Fact]
        public void DelegateIsCorrectWhenInitializedWithOneAutoArgument()
        {
            Action<object> expected = x => { };
            var sut = (TestCase)TestCase.New(expected);

            var actual = sut.Delegate;

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void ArgumentsIsCorrectWhenInitizliedWithOneAutoArgument()
        {
            var sut = (TestCase)TestCase.New<object>(x => { });
            var actual = sut.Arguments;
            Assert.Empty(actual);
        }

        [Fact]
        public void DelegateIsCorrectWhenInitizliedWithTwoAutoArguments()
        {
            Action<object, string> expected = (x, y) => { };
            var sut = (TestCase)TestCase.New(expected);

            var actual = sut.Delegate;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DelegateIsCorrectWhenInitizliedWithOneArgumentAndOneAutoArgument()
        {
            Action<int, object> expected = (x, y) => { };
            var sut = (TestCase)TestCase.New(0, expected);

            var actual = sut.Delegate;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DelegateIsCorrectWhenInitizliedWithTwoArguments()
        {
            Action<int, object> expected = (x, y) => { };
            var sut = (TestCase)TestCase.New(0, null, expected);

            var actual = sut.Delegate;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ArgumentsIsCorrectWhenInitizliedWithTwoAutoArguments()
        {
            var sut = (TestCase)TestCase.New<object, string>((x, y) => { });
            var actual = sut.Arguments;
            Assert.Empty(actual);
        }

        [Fact]
        public void ArgumentsIsCorrectWhenInitizliedWithOneArgumentAndOneAutoArgument()
        {
            var expected = new object();
            var sut = (TestCase)TestCase.New<object, string>(expected, (x, y) => { });

            var actual = sut.Arguments;

            Assert.Equal(new[] { expected }, actual);
        }

        [Fact]
        public void ArgumentsIsCorrectWhenInitizliedWithTwoArguments()
        {
            const string expected1 = "anonymous";
            const int expected2 = 1234;
            var sut = (TestCase)TestCase.New(expected1, expected2, (x, y) => { });

            var actual = sut.Arguments;

            Assert.Equal(new object[] { expected1, expected2 }, actual);
        }

        [Fact]
        public void ConvertNullMethodToTestCommandThrows()
        {
            var sut = TestCase.New(() => { });
            Assert.Throws<ArgumentNullException>(
                () => sut.ConvertToTestCommand(null, new FakeFixtureFactory()));
        }

        [Fact]
        public void ConvertToTestCommandWithNullFixtureFactoryThrows()
        {
            var sut = TestCase.New(() => { });
            IMethodInfo dummyMethodInfo = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            Assert.Throws<ArgumentNullException>(
                () => sut.ConvertToTestCommand(dummyMethodInfo, null));
        }
        
        [Fact]
        public void InitializeWithNonStaticDelegateThrows()
        {
            Assert.Throws<ArgumentException>(() => TestCase.New(ConvertToTestCommandReturnsCorrectCommand));
        }

        [Fact]
        public void ConvertToTestCommandReturnsCorrectCommand()
        {
            var arguments = new[] { new object() };
            Action<object> @delegate = x => { };
            var sut = TestCase.New(arguments[0], @delegate);
            var method = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());

            var actual = sut.ConvertToTestCommand(method, new FakeFixtureFactory { OnCreate = x => null });

            var command = Assert.IsAssignableFrom<FirstClassCommand>(actual);
            Assert.Equal(method, command.DeclaredMethod);
            Assert.Equal(@delegate.Method, command.TestMethod);
            Assert.Equal(arguments, command.Arguments);
        }

        [Fact]
        public void ConvertToTestCommandPassesAutoDataToCommand()
        {
            var testFixture = new FakeTestFixture();
            var obj = new object();
            var sut = TestCase.New<object, int, string>(obj, (x, y, z) => { });
            var method = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());

            var actual = sut.ConvertToTestCommand(method, new FakeFixtureFactory { OnCreate = x => testFixture });

            var command = Assert.IsAssignableFrom<FirstClassCommand>(actual);
            Assert.Equal(new[] { obj, testFixture.IntValue, testFixture.StringValue }, command.Arguments);
        }

        [Fact]
        public void ConvertToTestCommandInitializesFixtureOnlyOnceWhenCreatingAutoData()
        {
            var sut = TestCase.New<int>(x => { });
            var method = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            int creatCount = 0;
            var fixtureFactory = new FakeFixtureFactory
            {
                OnCreate = mi =>
                {
                    creatCount++;
                    return new FakeTestFixture();
                }
            };

            sut.ConvertToTestCommand(method, fixtureFactory);

            Assert.Equal(1, creatCount);
        }

        [Fact]
        public void ConvertToTestCommandPassesCorrectMethodInfoToFixtureFactory()
        {
            Action @delegate = () => { };
            var sut = TestCase.New(@delegate);
            bool verified = false;
            var fixtureFactory = new FakeFixtureFactory
            {
                OnCreate = mi =>
                {
                    Assert.Equal(@delegate.Method, mi);
                    verified = true;
                    return new FakeTestFixture();
                }
            };

            sut.ConvertToTestCommand(
                Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()),
                fixtureFactory);

            Assert.True(verified);
        }

        [Fact]
        public void InitializeWithCompositeDelegateThrows()
        {
            Action @delegate = () => { };
            @delegate += () => { };
            Assert.Throws<ArgumentException>(() => TestCase.New(@delegate));
        }
    }
}
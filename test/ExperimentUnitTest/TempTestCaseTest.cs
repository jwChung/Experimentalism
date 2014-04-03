using System;
using System.Reflection;
using Xunit;
using Xunit.Sdk;

namespace Jwc.Experiment
{
    public class TempTestCaseTest
    {
        [Fact]
        public void SutIsTestCase()
        {
            var sut = TempTestCase.New(() => { });
            Assert.IsAssignableFrom<ITestCase>(sut);
        }

        [Fact]
        public void DelegateIsCorrectWhenInitializedWithNoArguments()
        {
            Action expected = () => { };
            var sut = (TempTestCase)TempTestCase.New(expected);

            var actual = sut.Delegate;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DelegateIsCorrectWhenInitializedWithOneArgument()
        {
            Action<object> expected = x => { };
            var sut = (TempTestCase)TempTestCase.New(null, expected);

            var actual = sut.Delegate;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ArgumentsIsCorrectWhenInitializedWithNoArguments()
        {
            var sut = (TempTestCase)TempTestCase.New(() => { });
            var actual = sut.Arguments;
            Assert.Empty(actual);
        }

        [Fact]
        public void ArgumentsIsCorrectWhenInitializedWithOneArgument()
        {
            var expected = new object();
            var sut = (TempTestCase)TempTestCase.New(expected, x => { });

            var actual = sut.Arguments;

            Assert.Equal(new[] { expected }, actual);
        }

        [Fact]
        public void InitializeWithNullDelegateThrows()
        {
            Assert.Throws<ArgumentNullException>(() => TempTestCase.New(null));
        }

        [Fact]
        public void InitializeWithNullDelegateOfTArgThrows()
        {
            Assert.Throws<ArgumentNullException>(() => TempTestCase.New<object>(null, null));
        }

        [Fact]
        public void DelegateIsCorrectWhenInitializedWithOneAutoArgument()
        {
            Action<object> expected = x => { };
            var sut = (TempTestCase)TempTestCase.New(expected);

            var actual = sut.Delegate;

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void ArgumentsIsCorrectWhenInitizliedWithOneAutoArgument()
        {
            var sut = (TempTestCase)TempTestCase.New<object>(x => { });
            var actual = sut.Arguments;
            Assert.Empty(actual);
        }

        [Fact]
        public void DelegateIsCorrectWhenInitizliedWithTwoAutoArguments()
        {
            Action<object, string> expected = (x, y) => { };
            var sut = (TempTestCase)TempTestCase.New(expected);

            var actual = sut.Delegate;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DelegateIsCorrectWhenInitizliedWithOneArgumentAndOneAutoArgument()
        {
            Action<int, object> expected = (x, y) => { };
            var sut = (TempTestCase)TempTestCase.New(0, expected);

            var actual = sut.Delegate;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DelegateIsCorrectWhenInitizliedWithTwoArguments()
        {
            Action<int, object> expected = (x, y) => { };
            var sut = (TempTestCase)TempTestCase.New(0, null, expected);

            var actual = sut.Delegate;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ArgumentsIsCorrectWhenInitizliedWithTwoAutoArguments()
        {
            var sut = (TempTestCase)TempTestCase.New<object, string>((x, y) => { });
            var actual = sut.Arguments;
            Assert.Empty(actual);
        }

        [Fact]
        public void ArgumentsIsCorrectWhenInitizliedWithOneArgumentAndOneAutoArgument()
        {
            var expected = new object();
            var sut = (TempTestCase)TempTestCase.New<object, string>(expected, (x, y) => { });

            var actual = sut.Arguments;

            Assert.Equal(new[] { expected }, actual);
        }

        [Fact]
        public void ArgumentsIsCorrectWhenInitizliedWithTwoArguments()
        {
            const string expected1 = "anonymous";
            const int expected2 = 1234;
            var sut = (TempTestCase)TempTestCase.New(expected1, expected2, (x, y) => { });

            var actual = sut.Arguments;

            Assert.Equal(new object[] { expected1, expected2 }, actual);
        }

        [Fact]
        public void ConvertNullMethodToTestCommandThrows()
        {
            var sut = TempTestCase.New(() => { });
            Assert.Throws<ArgumentNullException>(
                () => sut.ConvertToTestCommand(null, new DelegatingFixtureFactory()));
        }

        [Fact]
        public void ConvertToTestCommandWithNullFixtureFactoryThrows()
        {
            var sut = TempTestCase.New(() => { });
            IMethodInfo dummyMethodInfo = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            Assert.Throws<ArgumentNullException>(
                () => sut.ConvertToTestCommand(dummyMethodInfo, null));
        }
        
        [Fact]
        public void InitializeWithNonStaticDelegateThrows()
        {
            Assert.Throws<ArgumentException>(() => TempTestCase.New(ConvertToTestCommandReturnsCorrectCommand));
        }

        [Fact]
        public void ConvertToTestCommandReturnsCorrectCommand()
        {
            var arguments = new[] { new object() };
            Action<object> @delegate = x => { };
            var sut = TempTestCase.New(arguments[0], @delegate);
            var method = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());

            var actual = sut.ConvertToTestCommand(method, new DelegatingFixtureFactory { OnCreate = x => null });

            var command = Assert.IsAssignableFrom<FirstClassCommand>(actual);
            Assert.Equal(method, command.Method);
            Assert.Equal(@delegate, command.Delegate);
            Assert.Equal(arguments, command.Arguments);
        }

        [Fact]
        public void ConvertToTestCommandPassesAutoDataToCommand()
        {
            var testFixture = new FakeTestFixture();
            var obj = new object();
            var sut = TempTestCase.New<object, int, string>(obj, (x, y, z) => { });
            var method = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());

            var actual = sut.ConvertToTestCommand(method, new DelegatingFixtureFactory { OnCreate = x => testFixture });

            var command = Assert.IsAssignableFrom<FirstClassCommand>(actual);
            Assert.Equal(new[] { obj, testFixture.IntValue, testFixture.StringValue }, command.Arguments);
        }

        [Fact]
        public void ConvertToTestCommandInitializesFixtureOnlyOnceWhenCreatingAutoData()
        {
            var sut = TempTestCase.New<int>(x => { });
            var method = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            int creatCount = 0;
            var fixtureFactory = new DelegatingFixtureFactory
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
            var sut = TempTestCase.New(@delegate);
            bool verified = false;
            var fixtureFactory = new DelegatingFixtureFactory
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
            Assert.Throws<ArgumentException>(() => TempTestCase.New(@delegate));
        }
    }
}
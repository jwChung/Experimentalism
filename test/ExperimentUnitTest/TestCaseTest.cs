using System;
using System.Reflection;
using Xunit;
using Xunit.Extensions;
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
        public void DelegateIsCorrect()
        {
            Action expected = () => { };
            var sut = (TestCase)TestCase.New(expected);

            var actual = sut.Delegate;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DelegateInitializeWithOneArgumentIsCorrect()
        {
            Action<object> expected = x => { };
            var sut = (TestCase)TestCase.New(null, expected);

            var actual = sut.Delegate;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ArgumentsIsCorrect()
        {
            var sut = (TestCase)TestCase.New(() => { });
            var actual = sut.Arguments;
            Assert.Empty(actual);
        }

        [Fact]
        public void ArgumentsWithOneArgumentIsCorrect()
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
        public void DelegateInitializeWithOneAutoArgumentIsCorrect()
        {
            Action<object> expected = x => { };
            var sut = (TestCase)TestCase.New(expected);

            var actual = sut.Delegate;

            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void ArgumentsInitializeWithOneAutoArgumentIsCorrect()
        {
            var sut = (TestCase)TestCase.New<object>(x => { });
            var actual = sut.Arguments;
            Assert.Empty(actual);
        }

        [Fact]
        public void DelegateInitializeWithTwoAutoArgumentsIsCorrect()
        {
            Action<object, string> expected = (x, y) => { };
            var sut = (TestCase)TestCase.New(expected);

            var actual = sut.Delegate;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DelegateInitializeWithOneArgumentAndOneAutoArgumentIsCorrect()
        {
            Action<int, object> expected = (x, y) => { };
            var sut = (TestCase)TestCase.New(0, expected);

            var actual = sut.Delegate;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DelegateInitializeWithTwoArgumentsIsCorrect()
        {
            Action<int, object> expected = (x, y) => { };
            var sut = (TestCase)TestCase.New(0, null, expected);

            var actual = sut.Delegate;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ArgumentsInitializeWithTwoAutoArgumentsIsCorrect()
        {
            var sut = (TestCase)TestCase.New<object, string>((x, y) => { });
            var actual = sut.Arguments;
            Assert.Empty(actual);
        }

        [Fact]
        public void ArgumentsInitializeWithOneArgumentAndOneAutoArgumentIsCorrect()
        {
            var expected = new object();
            var sut = (TestCase)TestCase.New<object, string>(expected, (x, y) => { });

            var actual = sut.Arguments;

            Assert.Equal(new[] { expected }, actual);
        }

        [Fact]
        public void ArgumentsInitializeWithTwoArgumentsIsCorrect()
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
                () => sut.ConvertToTestCommand(null, new FakeTestFixture()));
        }

        [Fact]
        public void ConvertToTestCommandWithNullFixtureThrows()
        {
            var sut = TestCase.New(() => { });
            IMethodInfo dummyMethodInfo = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            Assert.Throws<ArgumentNullException>(
                () => sut.ConvertToTestCommand(dummyMethodInfo, null));
        }

        [Fact]
        public void ConvertToTestCommandReturnsCorrectCommand()
        {
            var obj = new object();
            var sut = TestCase.New<object, int, string>(obj, (x, y, z) => { });
            IMethodInfo method = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            var testFixture = new FakeTestFixture();
            var expectedArguments = new[] { obj, testFixture.IntValue, testFixture.StringValue };
            var expectedDisplayName = method.TypeName + "." + method.Name;

            var actual = sut.ConvertToTestCommand(method, testFixture);

            var command = Assert.IsAssignableFrom<TheoryCommand>(actual);
            Assert.Equal(expectedArguments, command.Parameters);
            Assert.Equal(expectedDisplayName, command.DisplayName);
        }

        [Fact]
        public void InitializeWithNonStaticDelegateThrows()
        {
            Assert.Throws<ArgumentException>(() => TestCase.New(ConvertToTestCommandReturnsCorrectCommand));
        }
    }
}
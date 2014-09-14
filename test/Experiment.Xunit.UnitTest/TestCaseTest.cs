namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Reflection;
    using global::Xunit;

    public class TestCaseTest
    {
        [Fact]
        public void SutIsTestCase2()
        {
            var sut = new TestCase(new Action(() => { }), new object[0]);
            Assert.IsAssignableFrom<ITestCase>(sut);
        }
        
        [Fact]
        public void InitializeWithAnyNullArgumentsThrows()
        {
            var delegator = new Action(() => { });
            var arguments = new object[] { "1", 123 };

            Assert.Throws<ArgumentNullException>(() => new TestCase((Delegate)null, arguments));
            Assert.Throws<ArgumentNullException>(() => new TestCase(delegator, null));
        }

        [Fact]
        public void InitializeCorrectlyInitializesProperties()
        {
            var delegator = new Action(() => { });
            var arguments = new object[] { "1", 123 };

            var sut = new TestCase(delegator, arguments);

            Assert.Equal(delegator, sut.Delegator);
            Assert.Equal(delegator.Target, sut.Target);
            Assert.Equal(delegator.Method, sut.TestMethod);
            Assert.Equal(arguments, sut.Arguments);
        }

        [Fact]
        public void CreateWithNoArgumentsReturnsCorrectTestCase()
        {
            var delegator = new Action(() => { });

            var actual = TestCase.Create(delegator);

            var testCase = Assert.IsAssignableFrom<TestCase>(actual);
            Assert.Empty(testCase.Arguments);
            Assert.Equal(delegator, testCase.Delegator);
        }

        [Fact]
        public void CreateWithExplicit1ReturnsCorrectTestCase()
        {
            var arg1 = "anonymous";
            var delegator = new Action<string>(x => { });

            var actual = TestCase.WithArgs<string>(arg1).Create(delegator);

            var testCase = Assert.IsAssignableFrom<TestCase>(actual);
            Assert.Equal(new object[] { arg1 }, testCase.Arguments);
            Assert.Equal(delegator, testCase.Delegator);
        }

        [Fact]
        public void CreateWithAuto1ReturnsCorrectTestCase()
        {
            var delegator = new Action<string>(x => { });

            var actual = TestCase.WithAuto<string>().Create(delegator);

            var testCase = Assert.IsAssignableFrom<TestCase>(actual);
            Assert.Empty(testCase.Arguments);
            Assert.Equal(delegator, testCase.Delegator);
        }

        [Fact]
        public void CreateWithExplicit2ReturnsCorrectTestCase()
        {
            object arg1 = "anonymous";
            object arg2 = 123;
            var delegator = new Action<object, object>((x, y) => { });

            var actual = TestCase.WithArgs(arg1, arg2).Create(delegator);

            var testCase = Assert.IsAssignableFrom<TestCase>(actual);
            Assert.Equal(new object[] { arg1, arg2 }, testCase.Arguments);
            Assert.Equal(delegator, testCase.Delegator);
        }

        [Fact]
        public void CreateWithExplicit1AndAuto1ReturnsCorrectTestCase()
        {
            object arg1 = "anonymous";
            var delegator = new Action<object, int>((x, y) => { });

            var actual = TestCase.WithArgs(arg1).WithAuto<int>().Create(delegator);

            var testCase = Assert.IsAssignableFrom<TestCase>(actual);
            Assert.Equal(new object[] { arg1 }, testCase.Arguments);
            Assert.Equal(delegator, testCase.Delegator);
        }

        [Fact]
        public void CreateWithAuto2ReturnsCorrectTestCase()
        {
            var delegator = new Action<object, int>((x, y) => { });
            
            var actual = TestCase.WithAuto<object, int>().Create(delegator);

            var testCase = Assert.IsAssignableFrom<TestCase>(actual);
            Assert.Empty(testCase.Arguments);
            Assert.Equal(delegator, testCase.Delegator);
        }

        [Fact]
        public void CreateWithExplicit3AndAuto1ReturnsCorrectTestCase()
        {
            var arg1 = "anonymous";
            int arg2 = 123;
            var arg3 = new object();
            var delegator = new Action<string, int, object, int>((a1, a2, a3, a4) => { });
            
            var actual = TestCase.WithArgs(arg1, arg2, arg3).WithAuto<int>().Create(delegator);

            var testCase = Assert.IsAssignableFrom<TestCase>(actual);
            Assert.Equal(new object[] { arg1, arg2, arg3 }, testCase.Arguments);
            Assert.Equal(delegator, testCase.Delegator);
        }

        [Fact]
        public void CreateWithExplicit5AndAuto4ReturnsCorrectTestCase()
        {
            var arg1 = new object();
            var arg2 = new object();
            var arg3 = new object();
            var arg4 = new object();
            var arg5 = new object();
            var delegator = new Action<object, object, object, object, object, object, object, object, object>(
                (a1, a2, a3, a4, a5, a6, a7, a8, a9) => { });

            var actual = TestCase.WithArgs(arg1, arg2, arg3, arg4, arg5)
                .WithAuto<object, object, object, object>()
                .Create(delegator);

            var testCase = Assert.IsAssignableFrom<TestCase>(actual);
            Assert.Equal(new object[] { arg1, arg2, arg3, arg4, arg5 }, testCase.Arguments);
            Assert.Equal(delegator, testCase.Delegator);
        }

        [Fact]
        public void CreateWithNullDelegatorThrows()
        {
            Assert.Throws<ArgumentNullException>(() => TestCase.Create(null));
            Assert.Throws<ArgumentNullException>(() => TestCase.WithArgs("1").Create(null));
            Assert.Throws<ArgumentNullException>(() => TestCase.WithAuto<string>().Create(null));
            Assert.Throws<ArgumentNullException>(() => TestCase.WithArgs("1", 1).Create(null));
            Assert.Throws<ArgumentNullException>(() => TestCase.WithArgs("1").WithAuto<int>().Create(null));
            Assert.Throws<ArgumentNullException>(() => TestCase.WithAuto<object, int>().Create(null));
            Assert.Throws<ArgumentNullException>(
                () => TestCase.WithArgs("1", 1, new object()).WithAuto<int>().Create(null));
            Assert.Throws<ArgumentNullException>(
                () => TestCase.WithArgs("1", 1, new object(), "1", 1)
                    .WithAuto<object, object, object, object>()
                    .Create(null));
        }
    }
}

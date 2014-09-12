namespace Jwc.Experiment.Xunit
{
    using System;
    using global::Xunit;

    public class TestCase2Test
    {
        [Fact]
        public void SutIsTestCase2()
        {
            var sut = new TestCase2(new Action(() => { }).Method, new object[0]);
            Assert.IsAssignableFrom<ITestCase2>(sut);
        }
        
        [Fact]
        public void InitializeWithAnyNullArgumentsThrows()
        {
            var testMethod = new Action(() => { }).Method;
            var arguments = new object[] { "1", 123 };

            Assert.Throws<ArgumentNullException>(() => new TestCase2(null, arguments));
            Assert.Throws<ArgumentNullException>(() => new TestCase2(testMethod, null));
        }

        [Fact]
        public void InitializeCorrectlyInitializesProperties()
        {
            var testMethod = new Action(() => { }).Method;
            var arguments = new object[] { "1", 123 };

            var sut = new TestCase2(testMethod, arguments);

            Assert.Equal(testMethod, sut.TestMethod);
            Assert.Equal(arguments, sut.Arguments);
        }

        [Fact]
        public void CreateWithNoArgumentsReturnsCorrectTestCase()
        {
            var delegator = new Action(() => { });

            var actual = TestCase2.Create(delegator);

            Assert.Empty(actual.Arguments);
            Assert.Equal(delegator.Method, actual.TestMethod);
        }

        [Fact]
        public void CreateWithExplicit1ReturnsCorrectTestCase()
        {
            var arg1 = "anonymous";
            var delegator = new Action<string>(x => { });

            var actual = TestCase2.WithArgs<string>(arg1).Create(delegator);
            
            Assert.Equal(new object[] { arg1 }, actual.Arguments);
            Assert.Equal(delegator.Method, actual.TestMethod);
        }

        [Fact]
        public void CreateWithAuto1ReturnsCorrectTestCase()
        {
            var delegator = new Action<string>(x => { });

            var actual = TestCase2.WithAuto<string>().Create(delegator);

            Assert.Empty(actual.Arguments);
            Assert.Equal(delegator.Method, actual.TestMethod);
        }

        [Fact]
        public void CreateWithExplicit2ReturnsCorrectTestCase()
        {
            object arg1 = "anonymous";
            object arg2 = 123;
            var delegator = new Action<object, object>((x, y) => { });

            var actual = TestCase2.WithArgs(arg1, arg2).Create(delegator);

            Assert.Equal(new object[] { arg1, arg2 }, actual.Arguments);
            Assert.Equal(delegator.Method, actual.TestMethod);
        }

        [Fact]
        public void CreateWithExplicit1AndAuto1ReturnsCorrectTestCase()
        {
            object arg1 = "anonymous";
            var delegator = new Action<object, int>((x, y) => { });

            var actual = TestCase2.WithArgs(arg1).WithAuto<int>().Create(delegator);

            Assert.Equal(new object[] { arg1 }, actual.Arguments);
            Assert.Equal(delegator.Method, actual.TestMethod);
        }

        [Fact]
        public void CreateWithAuto2ReturnsCorrectTestCase()
        {
            var delegator = new Action<object, int>((x, y) => { });
            
            var actual = TestCase2.WithAuto<object, int>().Create(delegator);

            Assert.Empty(actual.Arguments);
            Assert.Equal(delegator.Method, actual.TestMethod);
        }

        [Fact]
        public void CreateWithExplicit3AndAuto1ReturnsCorrectTestCase()
        {
            var arg1 = "anonymous";
            int arg2 = 123;
            var arg3 = new object();
            var delegator = new Action<string, int, object, int>((a1, a2, a3, a4) => { });
            
            var actual = TestCase2.WithArgs(arg1, arg2, arg3).WithAuto<int>().Create(delegator);

            Assert.Equal(new object[] { arg1, arg2, arg3 }, actual.Arguments);
            Assert.Equal(delegator.Method, actual.TestMethod);
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

            var actual = TestCase2.WithArgs(arg1, arg2, arg3, arg4, arg5)
                .WithAuto<object, object, object, object>()
                .Create(delegator);

            Assert.Equal(new object[] { arg1, arg2, arg3, arg4, arg5 }, actual.Arguments);
            Assert.Equal(delegator.Method, actual.TestMethod);
        }
    }
}

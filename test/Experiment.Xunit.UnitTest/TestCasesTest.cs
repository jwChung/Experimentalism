namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Linq;
    using global::Xunit;

    public class TestCasesTest
    {
        [Fact]
        public void CreateWithExplicit1ReturnsCorrectTestCases()
        {
            var args1 = new[] { "a", "b", "c" };
            var delegator = new Action<string>(x => { });

            var actual = TestCases.WithArgs(args1).Create(delegator).ToArray();

            for (int i = 0; i < args1.Length; i++)
            {
                var testCase = Assert.IsAssignableFrom<TestCase>(actual[i]);
                Assert.Equal(new object[] { args1[i] }, testCase.Arguments);
                Assert.Equal(delegator, testCase.Delegator);
            }
        }

        [Fact]
        public void CreateWithExplicit2ReturnsCorrectTestCases()
        {
            var args1 = new[] { "a", "b", "c" };
            var args2 = new[] { 1, 2, 3 };
            var delegator = new Action<string, int>((x, y) => { });

            var actual = TestCases.WithArgs(args1, args2).Create(delegator).ToArray();

            for (int i = 0; i < args1.Length; i++)
            {
                var testCase = Assert.IsAssignableFrom<TestCase>(actual[i]);
                Assert.Equal(new object[] { args1[i], args2[i] }, testCase.Arguments);
                Assert.Equal(delegator, testCase.Delegator);
            }
        }

        [Fact]
        public void CreateWithExplicit1AndAuto1ReturnsCorrectTestCases()
        {
            var args1 = new[] { "a", "b", "c" };
            var delegator = new Action<string, int>((x, y) => { });

            var actual = TestCases.WithArgs(args1).WithAuto<int>().Create(delegator).ToArray();

            for (int i = 0; i < args1.Length; i++)
            {
                var testCase = Assert.IsAssignableFrom<TestCase>(actual[i]);
                Assert.Equal(new object[] { args1[i] }, testCase.Arguments);
                Assert.Equal(delegator, testCase.Delegator);
            }
        }

        [Fact]
        public void CreateWithExplicit3ReturnsCorrectTestCases()
        {
            var args1 = new[] { "a", "b", "c" };
            var args2 = new[] { 1, 2, 3 };
            var args3 = new[] { new object(), new object(), new object() };

            var delegator = new Action<string, int, object>((x, y, z) => { });

            var actual = TestCases.WithArgs(args1, args2, args3).Create(delegator).ToArray();

            for (int i = 0; i < args1.Length; i++)
            {
                var testCase = Assert.IsAssignableFrom<TestCase>(actual[i]);
                Assert.Equal(new object[] { args1[i], args2[i], args3[i] }, testCase.Arguments);
                Assert.Equal(delegator, testCase.Delegator);
            }
        }

        [Fact]
        public void CreateWithExplicit2AndAuto1ReturnsCorrectTestCases()
        {
            var args1 = new[] { "a", "b", "c" };
            var args2 = new[] { 1, 2, 3 };

            var delegator = new Action<string, int, object>((x, y, z) => { });

            var actual = TestCases.WithArgs(args1, args2).WithAuto<object>().Create(delegator).ToArray();

            for (int i = 0; i < args1.Length; i++)
            {
                var testCase = Assert.IsAssignableFrom<TestCase>(actual[i]);
                Assert.Equal(new object[] { args1[i], args2[i] }, testCase.Arguments);
                Assert.Equal(delegator, testCase.Delegator);
            }
        }

        [Fact]
        public void CreateWithExplicit1AndAuto2ReturnsCorrectTestCases()
        {
            var args1 = new[] { "a", "b", "c" };
            
            var delegator = new Action<string, int, object>((x, y, z) => { });

            var actual = TestCases.WithArgs(args1).WithAuto<int, object>().Create(delegator).ToArray();

            for (int i = 0; i < args1.Length; i++)
            {
                var testCase = Assert.IsAssignableFrom<TestCase>(actual[i]);
                Assert.Equal(new object[] { args1[i] }, testCase.Arguments);
                Assert.Equal(delegator, testCase.Delegator);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class CompositeEnumerablesTest
    {
        [Fact]
        public void SutIsEnumerableOfTestCase()
        {
            var sut = new CompositeEnumerables();
            Assert.IsAssignableFrom<IEnumerable<ITestCase>>(sut);
        }

        [Fact]
        public void InitializeWithNullTestCaseSetThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new CompositeEnumerables(null));
        }

        [Fact]
        public void SutEnumeratesCorrectTestCases()
        {
            var testCase1 = new DelegatingTestCase();
            var testCase2 = new DelegatingTestCase();
            var testCase3 = new DelegatingTestCase();
            var testCase4 = new DelegatingTestCase();
            var testCase5 = new DelegatingTestCase();
            IEnumerable<ITestCase>[] testCaseSet =
            {
                new ITestCase[]{ testCase1 },
                new ITestCase[]{ testCase2, testCase3 },
                new ITestCase[]{ testCase4, testCase5 }
            };
            var sut = new CompositeEnumerables(testCaseSet);

            var actual = sut.ToArray();

            Assert.Equal(testCase1, actual[0]);
            Assert.Equal(testCase2, actual[1]);
            Assert.Equal(testCase3, actual[2]);
            Assert.Equal(testCase4, actual[3]);
            Assert.Equal(testCase5, actual[4]);
        }

        [Fact]
        public void TestCaseSetIsCorrect()
        {
            IEnumerable<ITestCase>[] testCaseSet =
            {
                new ITestCase[]{ new DelegatingTestCase() },
                new ITestCase[]{ new DelegatingTestCase(), new DelegatingTestCase() }
            };
            var sut = new CompositeEnumerables(testCaseSet);

            var actual = sut.TestCaseSet;

            Assert.Equal(testCaseSet, actual);
        }
    }
}
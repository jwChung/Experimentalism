using System;
using System.Collections.Generic;
using System.Linq;
using Ploeh.Albedo;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class IdiomaticTestCasesTest
    {
        [Fact]
        public void SutIsEnumerableOfTestCase()
        {
            var sut = new IdiomaticTestCases(new IReflectionElement[0], f => null);
            Assert.IsAssignableFrom<IEnumerable<ITestCase>>(sut);
        }

        [Fact]
        public void InitializeWithNullReflectionElementsThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new IdiomaticTestCases(null, f => null));
        }

        [Fact]
        public void InitializeWithNullAssertionFactoryThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new IdiomaticTestCases(new IReflectionElement[0], null));
        }

        [Fact]
        public void ReflectionElementsIsCorrect()
        {
            var elements = new IReflectionElement[0];
            var sut = new IdiomaticTestCases(elements, f => null);

            var actual = sut.ReflectionElements;

            Assert.Equal(elements, actual);
        }

        [Fact]
        public void AssertionFactoryIsCorrect()
        {
            Func<ITestFixture, IReflectionVisitor<object>> assertionFactory = f => null;
            var sut = new IdiomaticTestCases(new IReflectionElement[0], assertionFactory);

            var actual = sut.AssertionFactory;

            Assert.Equal(assertionFactory, actual);
        }

        [Fact]
        public void SutEnumeratesCorrectTestCases()
        {
            // Fixture setup
            Func<ITestFixture, IReflectionVisitor<object>> assertionFactory = f => null;
            var reflectionElements = new IReflectionElement[]
            {
                new TypeElement(typeof(object)),
                new TypeElement(typeof(int)),
                new TypeElement(typeof(string))
            };

            var sut = new IdiomaticTestCases(reflectionElements, assertionFactory);

            // Exercise system
            var actual = sut.Cast<IdiomaticTestCase>().ToArray();

            // Verify outcome
            Assert.Equal(reflectionElements[0], actual[0].ReflectionElement);
            Assert.Equal(reflectionElements[1], actual[1].ReflectionElement);
            Assert.Equal(reflectionElements[2], actual[2].ReflectionElement);

            Assert.Equal(assertionFactory, actual[0].AssertionFactory);
            Assert.Equal(assertionFactory, actual[1].AssertionFactory);
            Assert.Equal(assertionFactory, actual[2].AssertionFactory);
        }
    }
}
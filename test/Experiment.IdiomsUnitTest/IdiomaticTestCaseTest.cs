using System;
using Ploeh.Albedo;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class IdiomaticTestCaseTest
    {
        [Fact]
        public void SutIsTestCase()
        {
            var sut = new IdiomaticTestCase(new TypeElement(typeof(object)), f => null);
            Assert.IsAssignableFrom<ITestCase>(sut);
        }

        [Fact]
        public void InitializeWithNullReflectionElementThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new IdiomaticTestCase(null, f => null));
        }

        [Fact]
        public void InitializeWithNullAssertionFactoryThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new IdiomaticTestCase(new TypeElement(typeof(object)), null));
        }

        [Fact]
        public void ReflectionElementIsCorrect()
        {
            var expected = new TypeElement(typeof(object));
            var sut = new IdiomaticTestCase(expected, f => null);

            var actual = sut.ReflectionElement;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AssertionFactoryIsCorrect()
        {
            Func<ITestFixture, IReflectionVisitor<object>> expected = f => null;
            var sut = new IdiomaticTestCase(new TypeElement(typeof(object)), expected);

            var actual = sut.AssertionFactory;

            Assert.Equal(expected, actual);
        }
    }
}
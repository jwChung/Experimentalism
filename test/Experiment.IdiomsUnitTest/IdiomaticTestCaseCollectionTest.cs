using System;
using System.Collections.Generic;
using Ploeh.Albedo;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class IdiomaticTestCaseCollectionTest
    {
        [Fact]
        public void SutIsEnumerableOfTestCase()
        {
            var sut = new IdiomaticTestCaseCollection(new IReflectionElement[0], f => null);
            Assert.IsAssignableFrom<IEnumerable<ITestCase>>(sut);
        }

        [Fact]
        public void InitializeWithNullReflectionElementsThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new IdiomaticTestCaseCollection(null, f => null));
        }

        [Fact]
        public void InitializeWithNullAssertionFactoryThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new IdiomaticTestCaseCollection(new IReflectionElement[0], null));
        }
    }
}
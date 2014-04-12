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
    }
}
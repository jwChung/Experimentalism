using System;
using System.Reflection;
using Xunit;

namespace Jwc.Experiment.Xunit
{
    public class NotSupportedFixtureFactory2Test
    {
        [Fact]
        public void SutIsTestFixtureFactory()
        {
            var sut = new NotSupportedFixtureFactory2();
            Assert.IsAssignableFrom<ITestFixtureFactory>(sut);
        }

        [Fact]
        public void CreateWithNullTestMethodThrows()
        {
            var sut = new NotSupportedFixtureFactory2();
            Assert.Throws<ArgumentNullException>(() => sut.Create(null));
        }

        [Fact]
        public void CreateThrows()
        {
            var sut = new NotSupportedFixtureFactory2();
            var testMethod = (MethodInfo)MethodBase.GetCurrentMethod();
            Assert.Throws<NotSupportedException>(() => sut.Create(testMethod));
        }
    }
}
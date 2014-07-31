using System;
using System.Reflection;
using Xunit;

namespace Jwc.Experiment
{
    public class NotSupportedFixtureFactoryTest
    {
        [Fact]
        public void SutIsTestFixtureFactory()
        {
            var sut = new NotSupportedFixtureFactory();
            Assert.IsAssignableFrom<ITestFixtureFactory>(sut);
        }

        [Fact]
        public void CreateThrows()
        {
            var sut = new NotSupportedFixtureFactory();
            var testMethod = (MethodInfo)MethodBase.GetCurrentMethod();
            Assert.Throws<NotSupportedException>(() => sut.Create(testMethod));
        }
    }
}
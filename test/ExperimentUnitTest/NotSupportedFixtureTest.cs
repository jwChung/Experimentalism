using System;
using Xunit;

namespace Jwc.Experiment
{
    public class NotSupportedFixtureTest
    {
        [Fact]
        public void SutIsTestFixture()
        {
            var sut = new NotSupportedFixture();
            Assert.IsAssignableFrom<ITestFixture>(sut);
        }

        [Fact]
        public void CreateThrowsNotSupportedException()
        {
            var sut = new NotSupportedFixture();
            Assert.Throws<NotSupportedException>(() => sut.Create(null));
        }
    }
}
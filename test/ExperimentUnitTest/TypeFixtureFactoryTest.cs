using System;
using Xunit;

namespace Jwc.Experiment
{
    public class TypeFixtureFactoryTest
    {
        [Fact]
        public void SutIsTestFixtureFactory()
        {
            var sut = new TypeFixtureFactory(typeof(object));
            Assert.IsAssignableFrom<ITestFixtureFactory>(sut);
        }

        [Fact]
        public void TypeIsCorrect()
        {
            var fixtureType = typeof(string);
            var sut = new TypeFixtureFactory(fixtureType);

            var actual = sut.FixtureType;

            Assert.Equal(fixtureType, actual);
        }

        [Fact]
        public void InitializeWithNullFixtureTypeThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new TypeFixtureFactory(null));
        }
    }
}
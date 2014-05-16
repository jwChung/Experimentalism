using System;
using Ploeh.AutoFixture;
using Xunit;
using Xunit.Extensions;

namespace Jwc.Experiment.AutoFixture
{
    public class AutoFixtureTest
    {
        [Fact]
        public void SutIsTestFixture()
        {
            var sut = new AutoFixture(new Fixture());
            Assert.IsAssignableFrom<ITestFixture>(sut);
        }

        [Fact]
        public void CreateReturnsCorrectSpecimen()
        {
            var request = typeof(object);
            var fixture = new Fixture();
            var expected = fixture.Freeze<object>();
            var sut = new AutoFixture(fixture);

            var actual = sut.Create(request);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void InitializeWithNullFixtureThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new AutoFixture(null));
        }

        [Fact]
        public void FixtureIsCorrect()
        {
            var expected = new Fixture();
            var sut = new AutoFixture(expected);

            var actual = sut.Fixture;

            Assert.Same(expected, actual);
        }

        [Theory]
        [InlineData(typeof(ITestFixture))]
        [InlineData(typeof(AutoFixture))]
        public void CreateTestFixtureReturnsItself(Type testFixtureType)
        {
            var sut = new AutoFixture(new Fixture());
            var actual = sut.Create(testFixtureType);
            Assert.Same(sut, actual);
        }
    }
}
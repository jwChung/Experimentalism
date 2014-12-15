﻿namespace Jwc.Experiment.AutoFixture
{
    using System;
    using System.Linq;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoMoq;
    using global::Xunit;
    using global::Xunit.Extensions;

    public class TestFixtureTest
    {
        [Fact]
        public void SutIsTestFixture()
        {
            var sut = new TestFixture(new Fixture());
            Assert.IsAssignableFrom<ITestFixture>(sut);
        }
        
        [Fact]
        public void InitializeWithNullFixtureThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new TestFixture(null));
        }

        [Fact]
        public void FixtureIsCorrect()
        {
            var expected = new Fixture();
            var sut = new TestFixture(expected);

            var actual = sut.Fixture;

            Assert.Same(expected, actual);
        }
        
        [Fact]
        public void CreateReturnsCorrectSpecimen()
        {
            var request = typeof(object);
            var fixture = new Fixture();
            var expected = fixture.Freeze<object>();
            var sut = new TestFixture(fixture);

            var actual = sut.Create(request);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(typeof(ITestFixture))]
        [InlineData(typeof(TestFixture))]
        public void CreateTestFixtureReturnsItself(Type testFixtureType)
        {
            var sut = new TestFixture(new Fixture());
            var actual = sut.Create(testFixtureType);
            Assert.Same(sut, actual);
        }
    }
}
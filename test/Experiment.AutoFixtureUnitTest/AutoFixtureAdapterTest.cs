﻿using System;
using Ploeh.AutoFixture;
using Xunit;

namespace Jwc.Experiment
{
    public class AutoFixtureAdapterTest
    {
        [Fact]
        public void SutIsTestFixture()
        {
            var sut = new AutoFixtureAdapter(new Fixture());
            Assert.IsAssignableFrom<ITestFixture>(sut);
        }

        [Fact]
        public void CreateReturnsCorrectSpecimen()
        {
            var request = typeof(object);
            var fixture = new Fixture();
            var expected = fixture.Freeze<object>();
            var sut = new AutoFixtureAdapter(fixture);

            var actual = sut.Create(request);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void InitializeWithNullFixtureThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new AutoFixtureAdapter(null));
        }

        [Fact]
        public void FixtureIsCorrect()
        {
            var expected = new Fixture();
            var sut = new AutoFixtureAdapter(expected);

            var actual = sut.Fixture;

            Assert.Same(expected, actual);
        }
    }
}
using System;
using Jwc.Experiment;
using Xunit;

namespace Jwc.NuGetFiles
{
    public class AutoFixtureAdapterTest
    {
        [Fact]
        public void SutIsTestFixture()
        {
            var sut = new AutoFixtureAdapter(new DelegatingSpecimenContext());
            Assert.IsAssignableFrom<ITestFixture>(sut);
        }

        [Fact]
        public void SpecimenContextIsCorrect()
        {
            var expected = new DelegatingSpecimenContext();
            var sut = new AutoFixtureAdapter(expected);

            var actual = sut.SpecimenContext;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void InitializeWithNullContextThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new AutoFixtureAdapter(null));
        }

        [Fact]
        public void CreateReturnsCorrectSpecimen()
        {
            var request = new object();
            var expected = new object();
            var context = new DelegatingSpecimenContext
            {
                OnResolve = r =>
                {
                    Assert.Equal(request, r);
                    return expected;
                }
            };
            var sut = new AutoFixtureAdapter(context);

            var actual = sut.Create(request);

            Assert.Equal(expected, actual);
        }
    }
}
using System;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using Xunit;

namespace Jwc.Experiment
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
            Assert.Throws<ArgumentNullException>(() => new AutoFixtureAdapter((ISpecimenContext)null));
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

        [Fact]
        public void InitializeWithNullFixtureThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new AutoFixtureAdapter((IFixture)null));
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
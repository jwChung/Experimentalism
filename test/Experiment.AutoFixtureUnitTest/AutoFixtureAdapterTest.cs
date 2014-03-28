using System;
using Xunit;

namespace Jwc.Experiment
{
    public class AutoFixtureAdapterTest
    {
        [Fact]
        public void SutIsTestFixture()
        {
            var sut = new AutoFixtureAdapter(new FakeSpecimenContext());
            Assert.IsAssignableFrom<ITestFixture>(sut);
        }

        [Fact]
        public void SpecimenContextIsCorrect()
        {
            var expected = new FakeSpecimenContext();
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
            var context = new FakeSpecimenContext();
            var request = new object();
            var expected = new object();
            context.OnResolve = r =>
            {
                if (r == request)
                    return expected;
                throw new NotSupportedException();
            };
            var sut = new AutoFixtureAdapter(context);

            var actual = sut.Create(request);

            Assert.Equal(expected, actual);
        }
    }
}
using System;
using Xunit;

namespace Jwc.Experiment
{
    public class TestFixtureAdapterTest
    {
        [Fact]
        public void SutIsTestFixture()
        {
            var sut = new TestFixtureAdapter(new FakeSpecimenContext());
            Assert.IsAssignableFrom<ITestFixture>(sut);
        }

        [Fact]
        public void SpecimenContextIsCorrect()
        {
            var expected = new FakeSpecimenContext();
            var sut = new TestFixtureAdapter(expected);

            var actual = sut.SpecimenContext;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void InitializeWithNullContextThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new TestFixtureAdapter(null));
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
            var sut = new TestFixtureAdapter(context);

            var actual = sut.Create(request);

            Assert.Equal(expected, actual);
        }
    }
}
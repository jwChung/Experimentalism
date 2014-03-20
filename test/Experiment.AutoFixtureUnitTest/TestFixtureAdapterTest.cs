using System;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using Xunit;

namespace Jwc.Experiment
{
    public class TestFixtureAdapterTest
    {
        [Fact]
        public void SutIsTestFixture()
        {
            var sut = new TestFixtureAdapter();
            Assert.IsAssignableFrom<ITestFixture>(sut);
        }

        [Fact]
        public void SpecimenContextIsCorrect()
        {
            var sut = new TestFixtureAdapter();

            var actual = sut.SpecimenContext;

            var context = Assert.IsType<SpecimenContext>(actual);
            Assert.IsType<Fixture>(context.Builder);
        }

        [Fact]
        public void SpecimenContextInitializedFromCtorIsCorrect()
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
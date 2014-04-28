using System;
using Ploeh.AutoFixture.Kernel;
using Xunit;

namespace Jwc.Experiment
{
    public class SpecimenBuilderTest
    {
        [Fact]
        public void SutIsSpecimenBuilder()
        {
            var sut = new SpecimenBuilder(new DelegatingTestFixture());
            Assert.IsAssignableFrom<ISpecimenBuilder>(sut);
        }

        [Fact]
        public void TestFixtureIsCorrect()
        {
            var testFixture = new DelegatingTestFixture();
            var sut = new SpecimenBuilder(testFixture);

            var actual = sut.TestFixture;

            Assert.Equal(testFixture, actual);
        }

        [Fact]
        public void InitializeWithNullTestFixtureThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new SpecimenBuilder(null));
        }

        [Fact]
        public void CreateReturnsCorrectSpecimen()
        {
            var request = new object();
            var specimen = new object();
            var testFixture = new DelegatingTestFixture
            {
                OnCreate = r =>
                {
                    Assert.Equal(request, r);
                    return specimen;
                }
            };
            var sut = new SpecimenBuilder(testFixture);

            var actual = sut.Create(request, null);

            Assert.Equal(specimen, actual);
        }
    }
}

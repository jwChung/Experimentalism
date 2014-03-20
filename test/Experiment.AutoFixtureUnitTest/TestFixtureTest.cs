using System;
using Jwc.Experiment;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using Xunit;

namespace Experiment.AutoFixtureUnitTest
{
    public class TestFixtureTest
    {
        [Fact]
        public void SutIsTestFixture()
        {
            var sut = new TestFixture();
            Assert.IsAssignableFrom<ITestFixture>(sut);
        }

        [Fact]
        public void SpecimenContextIsCorrect()
        {
            var sut = new TestFixture();

            var actual = sut.SpecimenContext;

            var context = Assert.IsType<SpecimenContext>(actual);
            Assert.IsType<Fixture>(context.Builder);
        }

        [Fact]
        public void SpecimenContextInitializedFromCtorIsCorrect()
        {
            var expected = new SpecimenContext(new ArrayRelay());
            var sut = new TestFixture(expected);

            var actual = sut.SpecimenContext;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void InitializeWithNullContextThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new TestFixture(null));
        }
    }
}
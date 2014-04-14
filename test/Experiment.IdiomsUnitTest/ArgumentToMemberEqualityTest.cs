using System.Collections.Generic;
using Ploeh.Albedo;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class ArgumentToMemberEqualityTest
    {
        [Fact]
        public void SutIsEqualityComparer()
        {
            var sut = new ArgumentToMemberEquality(new DelegatingTestFixture());
            Assert.IsAssignableFrom<IEqualityComparer<IReflectionElement>>(sut);
        }

        [Fact]
        public void TestFixtureIsCorrect()
        {
            var testFixture = new DelegatingTestFixture();
            var sut = new ArgumentToMemberEquality(testFixture);

            var actual = sut.TestFixture;

            Assert.Equal(testFixture, actual);
        }

        [Fact]
        public void GetHashCodeReturnsAlwaysSameValue()
        {
            var sut = new ArgumentToMemberEquality(new DelegatingTestFixture());
            var actual = sut.GetHashCode(null);
            Assert.Equal(0, actual);
        }
    }
}
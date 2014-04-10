using Ploeh.AutoFixture;
using Xunit;

namespace Jwc.Experiment
{
    public class AutoFixtureFirstClassTheoremAttributeTest
    {
        [Fact]
        public void SutIsDefaultFirstClassTheoremAttribute()
        {
            var sut = new TestSpecificFirstClassTheoremAttribute();
            Assert.IsAssignableFrom<BaseFirstClassTheoremAttribute>(sut);
        }

        [Fact]
        public void CreatTestFixtureReturnsCorrectFixture()
        {
            var sut = new TestSpecificFirstClassTheoremAttribute();
            var dummyMethod = typeof(object).GetMethod("ToString");

            var actual = sut.CreateTestFixture(dummyMethod);

            var adapter = Assert.IsType<AutoFixtureAdapter>(actual);
            Assert.IsType<Fixture>(adapter.Fixture);
        }

        public class TestSpecificFirstClassTheoremAttribute : AutoFixtureFirstClassTheoremAttribute
        {
            protected override IFixture CreateFixture()
            {
                return new Fixture();
            }
        }
    }
}
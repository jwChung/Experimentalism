using System.Reflection;
using Jwc.Experiment;
using Ploeh.AutoFixture;
using Xunit;

namespace NuGet.Jwc.Experiment
{
    public class FirstClassTheoremAttributeTest
    {
        [Fact]
        public void SutIsDefaultFirstClassTheoremAttribute()
        {
            var sut = new TestSpecificFirstClassTheoremAttribute();
            Assert.IsAssignableFrom<BaseFirstClassTheoremAttribute>(sut);
        }

        [Fact]
        public void CreatTestFixtureReturnsCorrectTestFixture()
        {
            var sut = new TestSpecificFirstClassTheoremAttribute();
            var dummyMethod = typeof(object).GetMethod("ToString");

            var actual = sut.CallCreateTestFixture(dummyMethod);

            var adapter = Assert.IsType<AutoFixtureAdapter>(actual);
            Assert.IsType<Fixture>(adapter.Fixture);
        }

        [Fact]
        public void CreateFixtureReturnsCorrectFixture()
        {
            var sut = new TestSpecificFirstClassTheoremAttribute();
            var actual = sut.CallCreateFixture();
            Assert.IsType<Fixture>(actual);
        }

        public class TestSpecificFirstClassTheoremAttribute : FirstClassTheoremAttribute
        {
            public ITestFixture CallCreateTestFixture(MethodInfo testMethod)
            {
                return CreateTestFixture(testMethod);
            }

            public IFixture CallCreateFixture()
            {
                return CreateFixture();
            }
        }
    }
}
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
            var actual = sut.CallCreateFixture(null);
            Assert.IsType<Fixture>(actual);
        }

        [Fact]
        public void CreateTestFixturePassesCorrecTestMethodToCreateFixture()
        {
            var sut = new TestSpecificFirstClassTheoremAttribute();
            var testMethod = typeof(object).GetMethod("ToString");

            sut.CallCreateTestFixture(testMethod);

            var actual = sut.TestMethod;
            Assert.Equal(testMethod, actual);
        }

        public class TestSpecificFirstClassTheoremAttribute : FirstClassTheoremAttribute
        {
            public MethodInfo TestMethod
            {
                get;
                set;
            }

            public ITestFixture CallCreateTestFixture(MethodInfo testMethod)
            {
                return base.CreateTestFixture(testMethod);
            }

            public IFixture CallCreateFixture(MethodInfo testMethod)
            {
                return base.CreateFixture(testMethod);
            }

            protected override IFixture CreateFixture(MethodInfo testMethod)
            {
                TestMethod = testMethod;
                return base.CreateFixture(testMethod);
            }
        }
    }
}
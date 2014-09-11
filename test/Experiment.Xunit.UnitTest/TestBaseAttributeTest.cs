namespace Jwc.Experiment.Xunit
{
    using Moq.Protected;
    using global::Xunit;

    public class TestBaseAttributeTest
    {
        [Fact]
        public void SutIsFactAttribute()
        {
            var sut = Mocked.Of<TestBaseAttribute>();
            Assert.IsAssignableFrom<FactAttribute>(sut);
        }

        [Fact]
        public void SutIsTestFixtureFactory()
        {
            var sut = Mocked.Of<TestBaseAttribute>();
            Assert.IsAssignableFrom<ITestFixtureFactory>(sut);
        }

        [Fact]
        public void CreateReturnsCorrectTestFixture()
        {
            var sut = Mocked.Of<TestBaseAttribute>();
            var context = Mocked.Of<ITestMethodInfo>();
            var expected = Mocked.Of<ITestFixture>();
            sut.ToMock().Protected().Setup<ITestFixture>("Create", context).Returns(expected);

            var actual = ((ITestFixtureFactory)sut).Create(context);

            Assert.Equal(expected, actual);
        }
    }
}

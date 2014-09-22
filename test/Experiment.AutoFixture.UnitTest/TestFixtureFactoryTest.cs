namespace Jwc.Experiment.AutoFixture
{
    using global::Xunit;

    public class TestFixtureFactoryTest
    {
        [Fact]
        public void SutIsTestFixtureFactory()
        {
            var sut = new TestFixtureFactory();
            Assert.IsAssignableFrom<ITestFixtureFactory>(sut);
        }
    }
}
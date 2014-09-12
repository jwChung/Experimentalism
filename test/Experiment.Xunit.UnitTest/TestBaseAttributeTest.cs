namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Moq;
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
        public void InitializeWithNullFactoryThrows()
        {
            var e = Assert.Throws<TargetInvocationException>(
                () => new Mock<TestBaseAttribute>((ITestCommandFactory)null).Object);
            Assert.IsType<ArgumentNullException>(e.InnerException);
        }

        [Fact]
        public void InitializeDefaultCtorCorrectlyInitializesTestCommandFactory()
        {
            var sut = new Mock<TestBaseAttribute>().Object;

            var factory = Assert.IsAssignableFrom<CompositeTestCommandFactory>(sut.TestCommandFactory);
            Assert.Equal(
                new[]
                {
                    typeof(TestCaseCommandFactory),
                    typeof(ParameterizedCommandFactory),
                    typeof(FactCommandFactory)
                },
                factory.TestCommandFactories.Select(f => f.GetType()));
        }

        [Fact]
        public void InitializeGreedyCtorCorrectlyInitializesTestCommandFactory()
        {
            var factory = Mocked.Of<ITestCommandFactory>();
            var sut = new Mock<TestBaseAttribute>(factory).Object;
            Assert.Equal(factory, sut.TestCommandFactory);
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

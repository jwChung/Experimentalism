namespace Jwc.Experiment.Xunit
{
    using System;
    using Moq;
    using global::Xunit;
    using global::Xunit.Sdk;

    public class CompositeTestCommandFactoryTest
    {
        [Fact]
        public void SutIsTestCommandFactory()
        {
            var sut = new CompositeTestCommandFactory();
            Assert.IsAssignableFrom<ITestCommandFactory>(sut);
        }

        [Fact]
        public void InitializeWithNullFactoriesThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new CompositeTestCommandFactory(null));
        }

        [Fact]
        public void InitializeCorrectlyInitializesFactories()
        {
            var factories = new[]
            {
                Mocked.Of<ITestCommandFactory>(),
                Mocked.Of<ITestCommandFactory>()
            };

            var sut = new CompositeTestCommandFactory(factories);

            Assert.Equal(factories, sut.TestCommandFactories);
        }

        [Fact]
        public void CreateReturnsCorrectTestCommands()
        {
            var method = Mocked.Of<IMethodInfo>();
            var fixtureFactory = Mocked.Of<IFixtureFactory>();
            var expected = new[] { Mocked.Of<ITestCommand>(), Mocked.Of<ITestCommand>() };
            var factory1 = Mocked.Of<ITestCommandFactory>(
                f => f.Create(method, fixtureFactory) == new ITestCommand[0]);
            var factory2 = Mocked.Of<ITestCommandFactory>(
                f => f.Create(method, fixtureFactory) == expected);
            var factory3 = Mocked.Of<ITestCommandFactory>(
                f => f.Create(method, fixtureFactory) == new[]
                {
                    Mocked.Of<ITestCommand>(), Mocked.Of<ITestCommand>()
                });
            var sut = new CompositeTestCommandFactory(factory1, factory2, factory3);

            var actual = sut.Create(method, fixtureFactory);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateReturnsEmptyIfAllFactoriesReturnEmpty()
        {
            var method = Mocked.Of<IMethodInfo>();
            var fixtureFactory = Mocked.Of<IFixtureFactory>();
            var factory1 = Mocked.Of<ITestCommandFactory>(
                f => f.Create(method, fixtureFactory) == new ITestCommand[0]);
            var factory2 = Mocked.Of<ITestCommandFactory>(
                f => f.Create(method, fixtureFactory) == new ITestCommand[0]);
            var sut = new CompositeTestCommandFactory(factory1, factory2);

            var actual = sut.Create(method, fixtureFactory);

            Assert.Empty(actual);
        }

        [Fact]
        public void CreateShouldNotCreateAnyCommandsWhenReturningEnumerable()
        {
            var factory1 = Mocked.Of<ITestCommandFactory>();
            var factory2 = Mocked.Of<ITestCommandFactory>();
            var sut = new CompositeTestCommandFactory(factory1, factory2);

            sut.Create(Mocked.Of<IMethodInfo>(), Mocked.Of<IFixtureFactory>());

            factory1.ToMock().Verify(
                x => x.Create(It.IsAny<IMethodInfo>(), It.IsAny<IFixtureFactory>()), Times.Never());
            factory2.ToMock().Verify(
                x => x.Create(It.IsAny<IMethodInfo>(), It.IsAny<IFixtureFactory>()), Times.Never());
        }
    }
}
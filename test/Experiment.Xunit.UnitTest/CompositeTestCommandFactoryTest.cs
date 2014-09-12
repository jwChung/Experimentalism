namespace Jwc.Experiment.Xunit
{
    using System;
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
            var fixtureFactory = Mocked.Of<ITestFixtureFactory>();
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
    }
}

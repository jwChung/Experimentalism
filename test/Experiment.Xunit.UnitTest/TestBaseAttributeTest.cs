namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Moq;
    using Moq.Protected;
    using global::Xunit;
    using global::Xunit.Sdk;

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
            var methodInfo = Mocked.Of<ITestMethodInfo>();
            var expected = Mocked.Of<ITestFixture>();
            sut.ToMock().Protected().Setup<ITestFixture>("Create", methodInfo).Returns(expected);

            var actual = ((ITestFixtureFactory)sut).Create(methodInfo);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreatTestCommandReturnsCorrectCommands()
        {
            var expected = new[] { Mocked.Of<ITestCommand>(), Mocked.Of<ITestCommand>() };
            var method = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            var factory = Mocked.Of<ITestCommandFactory>();
            var sut = new Mock<TestBaseAttribute>(factory) { CallBase = true }.Object;
            factory.Of(f => f.Create(method, sut) == expected);

            var actual = sut.CreateTestCommands(method);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreatTestCommandReturnsCorrectCommandsWhenTestCommandFactoryThrows()
        {
            var factory = Mocked.Of<ITestCommandFactory>(
                f => f.Create(It.IsAny<IMethodInfo>(), It.IsAny<ITestFixtureFactory>()) == this.GetTestCommands());
            var sut = new Mock<TestBaseAttribute>(factory) { CallBase = true }.Object;

            var actual = sut.CreateTestCommands(Mocked.Of<IMethodInfo>()).ToArray();

            Assert.Equal(2, actual.Length);
            var command = Assert.IsAssignableFrom<ExceptionCommand>(actual[1]);
            Assert.IsType<InvalidOperationException>(command.Exception);
        }

        private IEnumerable<ITestCommand> GetTestCommands()
        {
            yield return Mocked.Of<ITestCommand>();
            throw new InvalidOperationException();
        }
    }
}

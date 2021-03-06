﻿namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Moq;
    using Moq.Protected;
    using Ploeh.AutoFixture.Kernel;
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
            Assert.IsAssignableFrom<ISpecimenBuilderFactory>(sut);
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
                    typeof(DataAttributeCommandFactory),
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
            var context = Mocked.Of<ITestMethodContext>();
            var expected = Mocked.Of<ISpecimenBuilder>();
            sut.ToMock().Protected().Setup<ISpecimenBuilder>("Create", context).Returns(expected);

            var actual = ((ISpecimenBuilderFactory)sut).Create(context);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateTestCommandReturnsCorrectCommands()
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
        public void CreateTestCommandReturnsCorrectCommandsWhenTestCommandFactoryThrows()
        {
            var factory = Mocked.Of<ITestCommandFactory>(
                f => f.Create(It.IsAny<IMethodInfo>(), It.IsAny<ISpecimenBuilderFactory>()) == this.GetTestCommands());
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

namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Moq;
    using Ploeh.AutoFixture;
    using global::Xunit;

    public class TestCommandContextTest
    {
        [Fact]
        public void SutIsTestCommandContext()
        {
            var sut = Mocked.Of<TestCommandContext>(
                Mocked.Of<ITestFixtureFactory>(),
                Mocked.Of<IEnumerable<object>>());
            Assert.IsAssignableFrom<ITestCommandContext>(sut);
        }

        [Fact]
        public void InitializeWithAnyNullArgumentsThrows()
        {
            var factory = Mocked.Of<ITestFixtureFactory>();
            var arguments = Mocked.Of<IEnumerable<object>>();

            Assert.IsType<ArgumentNullException>(Assert.Throws<TargetInvocationException>(
                () => Mocked.Of<TestCommandContext>(null, arguments)).InnerException);
            Assert.IsType<ArgumentNullException>(Assert.Throws<TargetInvocationException>(
                () => Mocked.Of<TestCommandContext>(factory, null)).InnerException);
        }

        [Fact]
        public void InitializeCorrectlyInitializes()
        {
            var factory = Mocked.Of<ITestFixtureFactory>();
            var arguments = Mocked.Of<IEnumerable<object>>();

            var sut = Mocked.Of<TestCommandContext>(factory, arguments);

            Assert.Equal(factory, sut.TestFixtureFactory);
            Assert.Equal(arguments, sut.ExplicitArguments);
        }

        [Fact]
        public void GetArgumentsWithNullContextThrows()
        {
            var sut = Mocked.Of<TestCommandContext>(
                Mocked.Of<ITestFixtureFactory>(),
                Mocked.Of<IEnumerable<object>>());
            Assert.Throws<ArgumentNullException>(() => sut.GetArguments(null));
        }

        [Fact]
        public void GetArgumentsThrowsWhenExplicitArgumentsAreMoreThanTestMethodParameters()
        {
            var sut = Mocked.Of<TestCommandContext>(
                Mocked.Of<ITestFixtureFactory>(),
                new[] { "1", 1, new object() });
            var actualMethod = new Action<string, int>((x, y) => { }).Method;
            var context = Mocked.Of<ITestMethodContext>(x => x.ActualMethod == actualMethod);

            Assert.Throws<InvalidOperationException>(() => sut.GetArguments(context));
        }

        [Fact]
        public void GetArgumentsReturnsCorrectValuesWhenExplicitArgumentsAreMatchedWithTestMethodParameters()
        {
            var arguments = new[] { "1", 1, new object() };
            var sut = Mocked.Of<TestCommandContext>(
                Mocked.Of<ITestFixtureFactory>(),
                arguments);
            var actualMethod = new Action<string, int, object>((x, y, z) => { }).Method;
            var context = Mocked.Of<ITestMethodContext>(x => x.ActualMethod == actualMethod);

            var actual = sut.GetArguments(context);

            Assert.Equal(arguments, actual);
        }

        [Fact]
        public void GetArgumentsReturnsCorrectValuesWhenExplicitArgumentsAreLessThanTestMethodParameters()
        {
            // Fixture setup
            var arguments = new object[] { new object() };

            var method = new Action<object, string, int>((a, b, c) => { }).Method;
            var context = Mocked.Of<ITestMethodContext>(x => x.ActualMethod == method);

            var fixture = new Fixture();
            var expected = arguments.Concat(
                new object[] { fixture.Freeze<string>(), fixture.Freeze<int>() });

            var factory = Mocked.Of<ITestFixtureFactory>(x => x.Create(context) == new FakeTestFixture(fixture));

            var sut = Mocked.Of<TestCommandContext>(factory, arguments);

            // Exercise system
            var actual = sut.GetArguments(context);

            // Verify outcome
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void GetArgumentsShouldNotCreateTestFixtureWhenDoesNotNeedAutoData()
        {
            var factory = Mocked.Of<ITestFixtureFactory>();
            var sut = Mocked.Of<TestCommandContext>(
                factory,
                new object[] { "1", 1, new object() });
            var method = new Action<string, int, object>((x, y, z) => { }).Method;
            var context = Mocked.Of<ITestMethodContext>(x => x.ActualMethod == method);

            sut.GetArguments(context);

            factory.ToMock().Verify(x => x.Create(It.IsAny<ITestMethodContext>()), Times.Never());
        }
    }
}
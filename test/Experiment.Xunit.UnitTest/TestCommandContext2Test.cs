namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Moq;
    using global::Xunit;
    
    public class TestCommandContext2Test
    {
        [Fact]
        public void SutIsTestCommandContext()
        {
            var sut = Mocked.Of<TestCommandContext2>(
                Mocked.Of<ITestFixtureFactory>(),
                Mocked.Of<IEnumerable<object>>());
            Assert.IsAssignableFrom<ITestCommandContext>(sut);
        }

        [Fact]
        public void InitializeCorrectlyInitializes()
        {
            ITestFixtureFactory factory = Mocked.Of<ITestFixtureFactory>();
            IEnumerable<object> arguments = Mocked.Of<IEnumerable<object>>();

            var sut = Mocked.Of<TestCommandContext2>(factory, arguments);

            Assert.Equal(factory, sut.TestFixtureFactory);
            Assert.Equal(arguments, sut.ExplicitArguments);
        }

        [Fact]
        public void GetArgumentsWithNullContextThrows()
        {
            var sut = Mocked.Of<TestCommandContext2>(
                Mocked.Of<ITestFixtureFactory>(),
                Mocked.Of<IEnumerable<object>>());
            Assert.Throws<ArgumentNullException>(() => sut.GetArguments(null));
        }

        [Fact]
        public void GetArgumentsThrowsWhenExplicitArgumentsAreMoreThanTestMethodParameters()
        {
            var sut = Mocked.Of<TestCommandContext2>(
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
            var sut = Mocked.Of<TestCommandContext2>(
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

            var fixture = new FakeTestFixture();

            var factory = Mocked.Of<ITestFixtureFactory>(x => x.Create(context) == fixture);

            var sut = Mocked.Of<TestCommandContext2>(factory, arguments);

            var expected = arguments.Concat(
                new object[] { fixture.Create(typeof(string)), fixture.Create(typeof(int)) });

            // Exercise system
            var actual = sut.GetArguments(context);

            // Verify outcome
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void GetArgumentsShouldNotCreateTestFixtureWhenDoesNotNeedAutoData()
        {
            var factory = Mocked.Of<ITestFixtureFactory>();
            var sut = Mocked.Of<TestCommandContext2>(
                factory,
                new object[] { "1", 1, new object() });
            var method = new Action<string, int, object>((x, y, z) => { }).Method;
            var context = Mocked.Of<ITestMethodContext>(x => x.ActualMethod == method);

            sut.GetArguments(context);

            factory.ToMock().Verify(x => x.Create(It.IsAny<ITestMethodContext>()), Times.Never());
        }
    }
}
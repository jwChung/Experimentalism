namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;
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
    }
}

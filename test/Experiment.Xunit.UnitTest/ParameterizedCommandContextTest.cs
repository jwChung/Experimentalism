namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;
    using global::Xunit;
    using global::Xunit.Sdk;

    public class ParameterizedCommandContextTest
    {
        [Fact]
        public void SutIsTestCommandContext()
        {
            var sut = new ParameterizedCommandContext(
                Mocked.Of<IMethodInfo>(),
                Mocked.Of<ITestFixtureFactory>(),
                Mocked.Of<IEnumerable<object>>());
            Assert.IsAssignableFrom<TestCommandContext2>(sut);
        }

        [Fact]
        public void InitializeWithAnyNullArgumentsThrows()
        {
            var testMethod = Mocked.Of<IMethodInfo>();
            var factory = Mocked.Of<ITestFixtureFactory>();
            var arguments = Mocked.Of<IEnumerable<object>>();

            Assert.Throws<ArgumentNullException>(() => new ParameterizedCommandContext(null, factory, arguments));
            Assert.Throws<ArgumentNullException>(() => new ParameterizedCommandContext(testMethod, null, arguments));
            Assert.Throws<ArgumentNullException>(() => new ParameterizedCommandContext(testMethod, factory, null));
        }

        [Fact]
        public void InitializeCorrectlyInitializesProperties()
        {
            var testMethod = Mocked.Of<IMethodInfo>();
            var factory = Mocked.Of<ITestFixtureFactory>();
            var arguments = Mocked.Of<IEnumerable<object>>();
            
            var sut = new ParameterizedCommandContext(testMethod, factory, arguments);

            Assert.Equal(testMethod, sut.TestMethod);
            Assert.Equal(factory, sut.TestFixtureFactory);
            Assert.Equal(arguments, sut.ExplicitArguments);
        }
    }
}
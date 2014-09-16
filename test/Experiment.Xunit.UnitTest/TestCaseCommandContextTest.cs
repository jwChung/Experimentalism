namespace Jwc.Experiment.Xunit
{
    using System;
    using global::Xunit;
    using global::Xunit.Sdk;

    public class TestCaseCommandContextTest
    {
        [Fact]
        public void SutIsTestCommandContext()
        {
            var sut = new TestCaseCommandContext(
                Mocked.Of<IMethodInfo>(),
                Mocked.Of<IMethodInfo>(),
                new object(),
                Mocked.Of<ITestFixtureFactory>(),
                new object[0]);
            Assert.IsAssignableFrom<TestCommandContext2>(sut);
        }

        [Fact]
        public void InitializeWithAnyNullArgumentsThrows()
        {
            var testMethod = Mocked.Of<IMethodInfo>();
            var actualMethod = Mocked.Of<IMethodInfo>();
            var testObject = new object();
            var factory = Mocked.Of<ITestFixtureFactory>();
            var arguments = new[] { new object(), new object() };

            Assert.Throws<ArgumentNullException>(
                () => new TestCaseCommandContext(null, actualMethod, testObject, factory, arguments));
            Assert.Throws<ArgumentNullException>(
                () => new TestCaseCommandContext(testMethod, null, testObject, factory, arguments));
            Assert.Throws<ArgumentNullException>(
                () => new TestCaseCommandContext(testMethod, actualMethod, null, null, arguments));
            Assert.Throws<ArgumentNullException>(
                () => new TestCaseCommandContext(testMethod, actualMethod, testObject, null, arguments));
            Assert.Throws<ArgumentNullException>(
                () => new TestCaseCommandContext(testMethod, actualMethod, testObject, factory, null));
        }

        [Fact]
        public void InitializeCorrectlyInitializes()
        {
            var testMethod = Mocked.Of<IMethodInfo>();
            var actualMethod = Mocked.Of<IMethodInfo>();
            var actualObject = new object();
            var factory = Mocked.Of<ITestFixtureFactory>();
            var arguments = new[] { new object(), new object() };

            var sut = new TestCaseCommandContext(testMethod, actualMethod, actualObject, factory, arguments);

            Assert.Equal(testMethod, sut.TestMethod);
            Assert.Equal(actualMethod, sut.ActualMethod);
            Assert.Equal(actualObject, sut.ActualObject);
            Assert.Equal(factory, sut.TestFixtureFactory);
            Assert.Equal(arguments, sut.ExplicitArguments);
        }
    }
}
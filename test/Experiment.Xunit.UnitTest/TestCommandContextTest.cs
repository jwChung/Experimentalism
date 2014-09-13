namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using global::Xunit;
    using global::Xunit.Sdk;

    public class TestCommandContextTest
    {
        [Fact]
        public void SutIsTestCommandContext()
        {
            var sut = new TestCommandContext(
                Mocked.Of<IMethodInfo>(),
                Mocked.Of<ITestFixtureFactory>(),
                new object[0]);
            Assert.IsAssignableFrom<ITestCommandContext>(sut);
        }

        [Fact]
        public void InitializeFirstCtorWithAnyNullArgumentsThrows()
        {
            var testMethod = Mocked.Of<IMethodInfo>();
            var factory = Mocked.Of<ITestFixtureFactory>();
            var arguments = new[] { new object(), new object() };

            Assert.Throws<ArgumentNullException>(() => new TestCommandContext(null, factory, arguments));
            Assert.Throws<ArgumentNullException>(() => new TestCommandContext(testMethod, null, arguments));
            Assert.Throws<ArgumentNullException>(() => new TestCommandContext(testMethod, factory, null));
        }

        [Fact]
        public void InitializeSecondCtorWithAnyNullArgumentsThrows()
        {
            var testMethod = Mocked.Of<IMethodInfo>();
            var actualMethod = Mocked.Of<IMethodInfo>();
            var factory = Mocked.Of<ITestFixtureFactory>();
            var arguments = new[] { new object(), new object() };

            Assert.Throws<ArgumentNullException>(
                () => new TestCommandContext(null, actualMethod, factory, arguments));
            Assert.Throws<ArgumentNullException>(
                () => new TestCommandContext(testMethod, null, factory, arguments));
            Assert.Throws<ArgumentNullException>(
                () => new TestCommandContext(testMethod, actualMethod, null, arguments));
            Assert.Throws<ArgumentNullException>(
                () => new TestCommandContext(testMethod, actualMethod, factory, null));
        }

        [Fact]
        public void InitializeThirdCtorWithAnyNullArgumentsThrows()
        {
            var testMethod = Mocked.Of<IMethodInfo>();
            var actualMethod = Mocked.Of<IMethodInfo>();
            var testObject = new object();
            var factory = Mocked.Of<ITestFixtureFactory>();
            var arguments = new[] { new object(), new object() };

            Assert.Throws<ArgumentNullException>(
                () => new TestCommandContext(null, actualMethod, testObject, factory, arguments));
            Assert.Throws<ArgumentNullException>(
                () => new TestCommandContext(testMethod, null, testObject, factory, arguments));
            Assert.Throws<ArgumentNullException>(
                () => new TestCommandContext(testMethod, actualMethod, null, null, arguments));
            Assert.Throws<ArgumentNullException>(
                () => new TestCommandContext(testMethod, actualMethod, testObject, null, arguments));
            Assert.Throws<ArgumentNullException>(
                () => new TestCommandContext(testMethod, actualMethod, testObject, factory, null));
        }

        [Fact]
        public void InitializeFirstCtorCorrectlyInitializesProperties()
        {
            var testMethod = Mocked.Of<IMethodInfo>();
            var factory = Mocked.Of<ITestFixtureFactory>();
            var arguments = new[] { new object(), new object() };

            var sut = new TestCommandContext(testMethod, factory, arguments);

            Assert.Equal(testMethod, sut.TestMethod);
            Assert.Equal(testMethod, sut.ActualMethod);
            Assert.Null(sut.TestObject);
            Assert.Equal(factory, sut.TestFixtureFactory);
            Assert.Equal(arguments, sut.ExplicitArguments);
        }

        [Fact]
        public void InitializeSecondCtorCorrectlyInitializesProperties()
        {
            var testMethod = Mocked.Of<IMethodInfo>();
            var actualMethod = Mocked.Of<IMethodInfo>();
            var factory = Mocked.Of<ITestFixtureFactory>();
            var arguments = new[] { new object(), new object() };

            var sut = new TestCommandContext(testMethod, actualMethod, factory, arguments);

            Assert.Equal(testMethod, sut.TestMethod);
            Assert.Equal(actualMethod, sut.ActualMethod);
            Assert.Null(sut.TestObject);
            Assert.Equal(factory, sut.TestFixtureFactory);
            Assert.Equal(arguments, sut.ExplicitArguments);
        }

        [Fact]
        public void InitializeThirdCtorCorrectlyInitializesProperties()
        {
            var testMethod = Mocked.Of<IMethodInfo>();
            var actualMethod = Mocked.Of<IMethodInfo>();
            var testObject = new object();
            var factory = Mocked.Of<ITestFixtureFactory>();
            var arguments = new[] { new object(), new object() };

            var sut = new TestCommandContext(testMethod, actualMethod, testObject, factory, arguments);

            Assert.Equal(testMethod, sut.TestMethod);
            Assert.Equal(actualMethod, sut.ActualMethod);
            Assert.Equal(testObject, sut.TestObject);
            Assert.Equal(factory, sut.TestFixtureFactory);
            Assert.Equal(arguments, sut.ExplicitArguments);
        }

        [Fact]
        public void GetMethodContextThroughFirstCtorReturnsCorrectContext()
        {
            var testMethod = Mocked.Of<IMethodInfo>(x => x.MethodInfo == Mocked.Of<MethodInfo>());
            var sut = new TestCommandContext(
                testMethod,
                Mocked.Of<ITestFixtureFactory>(),
                new[] { new object(), new object() });
            var actualObject = new object();

            var actual = sut.GetMethodContext(actualObject);

            actual.AssertHasCorrectValues(
                testMethod.MethodInfo,
                testMethod.MethodInfo,
                actualObject,
                actualObject);
        }

        [Fact]
        public void GetMethodContextThroughThirdCtorReturnsCorrectContext()
        {
            var testMethod = Mocked.Of<IMethodInfo>(x => x.MethodInfo == Mocked.Of<MethodInfo>());
            var actualMethod = Mocked.Of<IMethodInfo>(x => x.MethodInfo == Mocked.Of<MethodInfo>());
            var testObject = new object();
            var sut = new TestCommandContext(
                testMethod,
                actualMethod,
                testObject,
                Mocked.Of<ITestFixtureFactory>(),
                new[] { new object(), new object() });
            var actualObject = new object();

            var actual = sut.GetMethodContext(actualObject);

            actual.AssertHasCorrectValues(
                testMethod.MethodInfo,
                actualMethod.MethodInfo,
                testObject,
                actualObject);
        }
    }
}
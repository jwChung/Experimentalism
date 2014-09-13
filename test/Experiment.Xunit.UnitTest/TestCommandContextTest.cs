﻿namespace Jwc.Experiment.Xunit
{
    using System;
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
        public void InitializeModestCtorWithAnyNullArgumentsThrows()
        {
            var testMethod = Mocked.Of<IMethodInfo>();
            var factory = Mocked.Of<ITestFixtureFactory>();
            var arguments = new[] { new object(), new object() };

            Assert.Throws<ArgumentNullException>(() => new TestCommandContext(null, factory, arguments));
            Assert.Throws<ArgumentNullException>(() => new TestCommandContext(testMethod, null, arguments));
            Assert.Throws<ArgumentNullException>(() => new TestCommandContext(testMethod, factory, null));
        }

        [Fact]
        public void InitializeGreedyCtorWithAnyNullArgumentsThrows()
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
        public void InitializeModestCtorCorrectlyInitializesProperties()
        {
            var testMethod = Mocked.Of<IMethodInfo>();
            var factory = Mocked.Of<ITestFixtureFactory>();
            var arguments = new[] { new object(), new object() };

            var sut = new TestCommandContext(testMethod, factory, arguments);

            Assert.Equal(testMethod, sut.TestMethod);
            Assert.Equal(testMethod, sut.ActualMethod);
            Assert.Equal(factory, sut.TestFixtureFactory);
            Assert.Equal(arguments, sut.ExplicitArguments);
        }

        [Fact]
        public void InitializeGreedyCtorCorrectlyInitializesProperties()
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
    }
}
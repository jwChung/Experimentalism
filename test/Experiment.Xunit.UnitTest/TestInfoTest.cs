namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using global::Xunit;

    public class TestInfoTest
    {
        [Fact]
        public void SutIsTestMethodInfo()
        {
            var testMethod = Mocked.Of<MethodInfo>();
            var actualMethod = Mocked.Of<MethodInfo>();
            var actualObject = new object();
            var factory = Mocked.Of<ITestFixtureFactory>();
            var arguments = Mocked.Of<IEnumerable<object>>();

            var sut = new TestInfo(testMethod, actualMethod, actualMethod, factory, arguments);

            Assert.IsAssignableFrom<ITestMethodInfo>(sut);
        }

        [Fact]
        public void SutIsTestCommandInfo()
        {
            var testMethod = Mocked.Of<MethodInfo>();
            var actualMethod = Mocked.Of<MethodInfo>();
            var actualObject = new object();
            var factory = Mocked.Of<ITestFixtureFactory>();
            var arguments = Mocked.Of<IEnumerable<object>>();

            var sut = new TestInfo(testMethod, actualMethod, actualMethod, factory, arguments);

            Assert.IsAssignableFrom<ITestCommandInfo>(sut);
        }

        [Fact]
        public void InitializeModestCtorWithAnyNullArgumentsThrows()
        {
            var testMethod = Mocked.Of<MethodInfo>();
            var factory = Mocked.Of<ITestFixtureFactory>();
            var arguments = new[] { new object(), new object() };

            Assert.Throws<ArgumentNullException>(() => new TestInfo(null, factory, arguments));
            Assert.Throws<ArgumentNullException>(() => new TestInfo(testMethod, null, arguments));
            Assert.Throws<ArgumentNullException>(() => new TestInfo(testMethod, factory, null));
        }

        [Fact]
        public void InitializeGreedyCtorWithAnyNullArgumentsThrows()
        {
            var testMethod = Mocked.Of<MethodInfo>();
            var actualMethod = Mocked.Of<MethodInfo>();
            var actualObject = new object();
            var factory = Mocked.Of<ITestFixtureFactory>();
            var arguments = new[] { new object(), new object() };

            Assert.Throws<ArgumentNullException>(
                () => new TestInfo(null, actualMethod, actualObject, factory, arguments));
            Assert.Throws<ArgumentNullException>(
                () => new TestInfo(testMethod, null, actualObject, factory, arguments));
            Assert.Throws<ArgumentNullException>(
                () => new TestInfo(testMethod, actualMethod, null, factory, arguments));
            Assert.Throws<ArgumentNullException>(
                () => new TestInfo(testMethod, actualMethod, actualObject, null, arguments));
            Assert.Throws<ArgumentNullException>(
                () => new TestInfo(testMethod, actualMethod, actualObject, factory, null));
        }

        [Fact]
        public void InitializeModestCtorCorrectInitializesProperties()
        {
            var testMethod = Mocked.Of<MethodInfo>();
            var factory = Mocked.Of<ITestFixtureFactory>();
            var arguments = new[] { new object(), new object() };

            var sut = new TestInfo(testMethod, factory, arguments);

            Assert.Equal(testMethod, sut.TestMethod);
            Assert.Equal(testMethod, sut.ActualMethod);
            Assert.Null(sut.ActualObject);
            Assert.Equal(factory, sut.TestFixtureFactory);
            Assert.Equal(arguments, sut.Arguments);
            Assert.Null(sut.TestObject);
            Assert.Equal(testMethod, ((ITestCommandInfo)sut).TestMethod.MethodInfo);
        }

        [Fact]
        public void InitializeGreedyCtorCorrectInitializesProperties()
        {
            var testMethod = Mocked.Of<MethodInfo>();
            var actualMethod = Mocked.Of<MethodInfo>();
            var actualObject = new object();
            var factory = Mocked.Of<ITestFixtureFactory>();
            var arguments = new[] { new object(), new object() };

            var sut = new TestInfo(testMethod, actualMethod, actualObject, factory, arguments);

            Assert.Equal(testMethod, sut.TestMethod);
            Assert.Equal(actualMethod, sut.ActualMethod);
            Assert.Equal(actualObject, sut.ActualObject);
            Assert.Equal(factory, sut.TestFixtureFactory);
            Assert.Equal(arguments, sut.Arguments);
            Assert.Null(sut.TestObject);
            Assert.Equal(actualMethod, ((ITestCommandInfo)sut).TestMethod.MethodInfo);
        }
    }
}

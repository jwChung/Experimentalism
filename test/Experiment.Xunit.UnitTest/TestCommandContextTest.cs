namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Moq;
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
            Assert.Null(sut.ActualMethod);
            Assert.Null(sut.ActualObject);
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
            Assert.Null(sut.ActualObject);
            Assert.Equal(factory, sut.TestFixtureFactory);
            Assert.Equal(arguments, sut.ExplicitArguments);
        }

        [Fact]
        public void InitializeThirdCtorCorrectlyInitializesProperties()
        {
            var testMethod = Mocked.Of<IMethodInfo>();
            var actualMethod = Mocked.Of<IMethodInfo>();
            var actualObject = new object();
            var factory = Mocked.Of<ITestFixtureFactory>();
            var arguments = new[] { new object(), new object() };

            var sut = new TestCommandContext(testMethod, actualMethod, actualObject, factory, arguments);

            Assert.Equal(testMethod, sut.TestMethod);
            Assert.Equal(actualMethod, sut.ActualMethod);
            Assert.Equal(actualObject, sut.ActualObject);
            Assert.Equal(factory, sut.TestFixtureFactory);
            Assert.Equal(arguments, sut.ExplicitArguments);
        }

        [Fact]
        [Obsolete]
        public void TestObjectThrowsNotSupportedException()
        {
            var sut = new TestCommandContext(
                Mocked.Of<IMethodInfo>(),
                Mocked.Of<ITestFixtureFactory>(),
                new object[0]);
            Assert.Throws<NotSupportedException>(() => sut.TestObject);
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
            var actualObject = new object();
            var sut = new TestCommandContext(
                testMethod,
                actualMethod,
                actualObject,
                Mocked.Of<ITestFixtureFactory>(),
                new[] { new object(), new object() });
            var testObject = new object();

            var actual = sut.GetMethodContext(testObject);

            actual.AssertHasCorrectValues(
                testMethod.MethodInfo,
                actualMethod.MethodInfo,
                testObject,
                actualObject);
        }

        [Fact]
        public void GetArgumentsWithNullContextThrows()
        {
            var sut = new TestCommandContext(
                Mocked.Of<IMethodInfo>(),
                Mocked.Of<ITestFixtureFactory>(),
                new object[0]);
            Assert.Throws<ArgumentNullException>(() => sut.GetArguments(null));
        }

        [Fact]
        public void GetArgumentsThrowsWhenExplicitArgumentsAreMoreThanTestMethodParameters()
        {
            var sut = new TestCommandContext(
                Mocked.Of<IMethodInfo>(),
                Mocked.Of<ITestFixtureFactory>(),
                new object[] { "1", 1, new object() });
            var actualMethod = new Action<string, int>((x, y) => { }).Method;
            var context = Mocked.Of<ITestMethodContext>(x => x.ActualMethod == actualMethod);

            Assert.Throws<InvalidOperationException>(() => sut.GetArguments(context));
        }

        [Fact]
        public void GetArgumentsReturnsCorrectValuesWhenExplicitArgumentsAreMatchedWithTestMethodParameters()
        {
            var arguments = new object[] { "1", 1, new object() };
            var sut = new TestCommandContext(
                Mocked.Of<IMethodInfo>(),
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

            var actualMethod = new Action<object, string, int>((x, y, z) => { }).Method;
            
            var context = Mocked.Of<ITestMethodContext>(x => x.ActualMethod == actualMethod);
            
            var fixture = new FakeTestFixture();
            
            var factory = Mocked.Of<ITestFixtureFactory>(x => x.Create(context) == fixture);
            
            var sut = new TestCommandContext(
                Mocked.Of<IMethodInfo>(),
                factory,
                arguments);

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
            var sut = new TestCommandContext(
                Mocked.Of<IMethodInfo>(),
                factory,
                new object[] { "1", 1, new object() });
            var actualMethod = new Action<string, int, object>((x, y, z) => { }).Method;
            var context = Mocked.Of<ITestMethodContext>(x => x.ActualMethod == actualMethod);

            sut.GetArguments(context);

            factory.ToMock().Verify(x => x.Create(It.IsAny<ITestMethodContext>()), Times.Never());
        }
    }
}
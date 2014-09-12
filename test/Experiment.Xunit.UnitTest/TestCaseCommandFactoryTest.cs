namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Ploeh.Albedo;
    using global::Xunit;
    using global::Xunit.Extensions;
    using global::Xunit.Sdk;

    public class TestCaseCommandFactoryTest
    {
        [Fact]
        public void SutIsTestCommandFactory()
        {
            var sut = new TestCaseCommandFactory();
            Assert.IsAssignableFrom<ITestCommandFactory>(sut);
        }

        [Fact]
        public void CreateWithNullTestMethodThrows()
        {
            var sut = new TestCaseCommandFactory();
            Assert.Throws<ArgumentNullException>(() => sut.Create(null, null).ToArray());
        }

        [Theory]
        [InlineData(typeof(void))]
        [InlineData(typeof(object))]
        public void CreateReturnsEmptyIfReturnTypeIsIncorrect(Type returnType)
        {
            var sut = new TestCaseCommandFactory();
            var testMethod = Mocked.Of<IMethodInfo>(m => m.MethodInfo
                == Mocked.Of<MethodInfo>(i => i.ReturnType == returnType));

            var actual = sut.Create(testMethod, null);

            Assert.Empty(actual);
        }

        [Theory]
        [InlineData(typeof(IEnumerable<ITestCase2>))]
        [InlineData(typeof(ITestCase2[]))]
        public void CreateReturnsNonEmptyIfReturnTypeIsCorrect(Type returnType)
        {
            var sut = new TestCaseCommandFactory();
            var testMethod = new Methods<TestClass>().Select(x => x.TestMethod());
            var factory = Mocked.Of<ITestFixtureFactory>();

            var actual = sut.Create(Reflector.Wrap(testMethod), factory);

            Assert.NotEmpty(actual);
        }

        [Fact]
        public void CreateReturnsCorrectCommands()
        {
            var testMethod = new Methods<TestClass>().Select(x => x.TestMethod());
            var sut = new TestCaseCommandFactory();
            var factory = Mocked.Of<ITestFixtureFactory>();

            var actual = sut.Create(Reflector.Wrap(testMethod), factory);

            Assert.Equal(3, actual.Count());

            Assert.True(actual.All(x =>
            {
                var command = Assert.IsAssignableFrom<ParameterizedCommand>(x);
                var testInfo = Assert.IsAssignableFrom<TestInfo>(command.TestCommandInfo);
                return HasValues(
                    testInfo,
                    testMethod,
                    TestClass.Method,
                    TestClass.TestObject,
                    TestClass.Arguments,
                    factory);
            }));
        }

        [Fact]
        public void CreateWithStaticTestCommandReturnsCorrectCommands()
        {
            var testMethod = Methods.Select(() => TestClass.StaticTestMethod());
            var sut = new TestCaseCommandFactory();
            var factory = Mocked.Of<ITestFixtureFactory>();

            var actual = sut.Create(Reflector.Wrap(testMethod), factory).Single();

            var command = Assert.IsAssignableFrom<ParameterizedCommand>(actual);
            var testInfo = Assert.IsAssignableFrom<TestInfo>(command.TestCommandInfo);
            Assert.True(HasValues(
                testInfo,
                testMethod,
                TestClass.Method,
                TestClass.TestObject,
                TestClass.Arguments,
                factory));
        }

        [Fact]
        public void CreateWithStaticTestClassReturnsCorrectCommands()
        {
            var testMethod = Methods.Select(() => StaticTestClass.StaticTestMethod());
            var sut = new TestCaseCommandFactory();
            var factory = Mocked.Of<ITestFixtureFactory>();

            var actual = sut.Create(Reflector.Wrap(testMethod), factory).Single();

            var command = Assert.IsAssignableFrom<ParameterizedCommand>(actual);
            var testInfo = Assert.IsAssignableFrom<TestInfo>(command.TestCommandInfo);
            Assert.True(HasValues(
                testInfo,
                testMethod,
                StaticTestClass.Method,
                StaticTestClass.TestObject,
                StaticTestClass.Arguments,
                factory));
        }

        [Fact]
        public void CreateReturnsEmptyIfTestMethodIsParameterized()
        {
            var sut = new TestCaseCommandFactory();
            var @delegate = new Func<object, int, IEnumerable<ITestCase2>>((x, y) => new ITestCase2[0]);

            var actual = sut.Create(Reflector.Wrap(@delegate.Method), null);

            Assert.Empty(actual);
        }

        private static bool HasValues(
            TestInfo m,
            MethodInfo testMethod,
            MethodInfo actualMethod,
            object actualObject,
            IEnumerable<object> arguments,
            ITestFixtureFactory factory)
        {
            return m.TestMethod == testMethod
                && m.ActualMethod == actualMethod
                && m.ActualObject == actualObject
                && m.Arguments.SequenceEqual(arguments)
                && m.TestFixtureFactory == factory;
        }

        private class TestClass
        {
            public static readonly MethodInfo Method = new Methods<object>().Select(x => x.ToString());
            public static readonly object[] Arguments = new object[] { 123, "string" };
            public static object TestObject = new object();

            public IEnumerable<ITestCase2> TestMethod()
            {
                yield return Mocked.Of<ITestCase2>(t =>
                    t.TestMethod == Method
                    && t.Arguments == Arguments
                    && t.TestObject == TestObject);

                yield return Mocked.Of<ITestCase2>(t =>
                    t.TestMethod == Method
                    && t.Arguments == Arguments
                    && t.TestObject == TestObject);

                yield return Mocked.Of<ITestCase2>(t =>
                    t.TestMethod == Method
                    && t.Arguments == Arguments
                    && t.TestObject == TestObject);
            }

            public static IEnumerable<ITestCase2> StaticTestMethod()
            {
                yield return Mocked.Of<ITestCase2>(t =>
                    t.TestMethod == Method
                    && t.Arguments == Arguments
                    && t.TestObject == TestObject);
            }
        }

        private static class StaticTestClass
        {
            public static readonly MethodInfo Method = new Methods<object>().Select(x => x.ToString());
            public static readonly object[] Arguments = new object[] { 123, "string" };
            public static object TestObject = new object();

            public static IEnumerable<ITestCase2> StaticTestMethod()
            {
                yield return Mocked.Of<ITestCase2>(t =>
                    t.TestMethod == Method
                    && t.Arguments == Arguments
                    && t.TestObject == TestObject);
            }
        }
    }
}

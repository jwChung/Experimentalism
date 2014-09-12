namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Moq;
    using Ploeh.Albedo;
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
        public void InitializeModestCtorCorrectlyInitializesProperties()
        {
            var testMethod = Mocked.Of<MethodInfo>();
            var factory = Mocked.Of<ITestFixtureFactory>();
            var arguments = new[] { new object(), new object() };

            var sut = new TestInfo(testMethod, factory, arguments);

            Assert.Equal(testMethod, sut.TestMethod);
            Assert.Equal(testMethod, sut.ActualMethod);
            Assert.Null(sut.TestObject);
            Assert.Null(sut.ActualObject);
            Assert.Equal(factory, sut.TestFixtureFactory);
            Assert.Equal(arguments, sut.ExplicitArguments);
            Assert.Equal(testMethod, ((ITestCommandInfo)sut).TestMethod.MethodInfo);
        }

        [Fact]
        public void InitializeGreedyCtorCorrectlyInitializesProperties()
        {
            var testMethod = Mocked.Of<MethodInfo>();
            var actualMethod = Mocked.Of<MethodInfo>();
            var actualObject = new object();
            var factory = Mocked.Of<ITestFixtureFactory>();
            var arguments = new[] { new object(), new object() };

            var sut = new TestInfo(testMethod, actualMethod, actualObject, factory, arguments);

            Assert.Equal(testMethod, sut.TestMethod);
            Assert.Equal(actualMethod, sut.ActualMethod);
            Assert.Null(sut.TestObject);
            Assert.Equal(actualObject, sut.ActualObject);
            Assert.Equal(factory, sut.TestFixtureFactory);
            Assert.Equal(arguments, sut.ExplicitArguments);
            Assert.Equal(actualMethod, ((ITestCommandInfo)sut).TestMethod.MethodInfo);
        }

        [Fact]
        public void GetArgumentsThrowsIfNubmerOfArgumentsIsGreatThanNumberOfTestArguments()
        {
            var testMethod = new Methods<TestInfoTest>().Select(x => x.TestMethod(null, null));
            var actualMethod = (MethodInfo)MethodBase.GetCurrentMethod();
            var arguments = new[] { new object(), new object() };
            var sut = new TestInfo(
                testMethod, actualMethod, new object(), Mocked.Of<ITestFixtureFactory>(), arguments);

            Assert.Throws<InvalidOperationException>(() => sut.GetArguments(new object()));
        }

        [Fact]
        public void GetArgumentsReturnsCorrectValuesWhenNubmerOfArgumentsEqualsToNumberOfTestArguments()
        {
            var testMethod = new Methods<TestInfoTest>().Select(x => x.TestMethod(null, null));
            var arguments = new[] { new object(), new object() };
            var sut = new TestInfo(
                testMethod, Mocked.Of<ITestFixtureFactory>(), arguments);

            var actual = sut.GetArguments(new object());

            Assert.Equal(arguments, actual);
        }

        [Fact]
        public void GetArgumentsWithModestCtorReturnsCorrectValuesWhenNubmerOfArgumentsIsLessThanNumberOfTestArguments()
        {
            var testMethod = new Methods<TestInfoTest>().Select(x => x.TestMethod(null, null, 0));
            var arguments = new[] { new object() };
            var arg1 = "string";
            var arg2 = 123;
            var testObject = new object();
            var fixture = Mocked.Of<ITestFixture>(
                        t => t.Create(typeof(string)) == (object)arg1 && t.Create(typeof(int)) == (object)arg2);
            var factory = Mocked.Of<ITestFixtureFactory>(
                f => f.Create(It.Is<ITestMethodInfo>(
                    p => HasValues(p, testMethod, testMethod, testObject, testObject))) == fixture);
            var sut = new TestInfo(testMethod, factory, arguments);

            var actual = sut.GetArguments(testObject);

            Assert.Equal(arguments.Concat(new object[] { arg1, arg2 }), actual);
        }

        [Fact]
        public void GetArgumentsWithGreedyCtorReturnsCorrectValuesWhenNubmerOfArgumentsIsLessThanNumberOfTestArguments()
        {
            var testMethod = Mocked.Of<MethodInfo>();
            var actualMethod = new Methods<TestInfoTest>().Select(x => x.TestMethod(null, null, 0));
            var testObject = new object();
            var actualObject = new object();
            var arguments = new[] { new object() };
            var arg1 = "string";
            var arg2 = 123;
            var fixture = Mocked.Of<ITestFixture>(
                        t => t.Create(typeof(string)) == (object)arg1 && t.Create(typeof(int)) == (object)arg2);
            var factory = Mocked.Of<ITestFixtureFactory>(
                f => f.Create(It.Is<ITestMethodInfo>(
                    p => HasValues(p, testMethod, actualMethod, testObject, actualObject))) == fixture);
            var sut = new TestInfo(testMethod, actualMethod, actualObject, factory, arguments);

            var actual = sut.GetArguments(testObject);

            Assert.Equal(arguments.Concat(new object[] { arg1, arg2 }), actual);
        }

        private static bool HasValues(
            ITestMethodInfo m,
            MethodInfo testMethod,
            MethodInfo actualMethod,
            object testObject,
            object actualObject)
        {
            return m.TestMethod == testMethod
                    && m.ActualMethod == actualMethod
                    && m.TestObject == testObject
                    && m.ActualObject == actualObject;
        }

        private void TestMethod(object arg1, object arg2)
        {
        }

        private void TestMethod(object arg1, string arg2, int arg3)
        {
        }
    }
}

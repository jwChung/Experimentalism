namespace Jwc.Experiment.Xunit
{
    using System;
    using global::Xunit;
    using global::Xunit.Sdk;

    public class StaticTestCaseCommandContextTest
    {
        [Fact]
        public void SutIsTestCommandContext()
        {
            var sut = new StaticTestCaseCommandContext(
                Mocked.Of<IMethodInfo>(),
                Mocked.Of<IMethodInfo>(),
                Mocked.Of<ISpecimenBuilderFactory>(),
                new object[0]);
            Assert.IsAssignableFrom<TestCommandContext>(sut);
        }

        [Fact]
        public void InitializeWithAnyNullArgumentsThrows()
        {
            var testMethod = Mocked.Of<IMethodInfo>();
            var actualMethod = Mocked.Of<IMethodInfo>();
            var factory = Mocked.Of<ISpecimenBuilderFactory>();
            var arguments = new[] { new object(), new object() };

            Assert.Throws<ArgumentNullException>(
                () => new StaticTestCaseCommandContext(null, actualMethod, factory, arguments));
            Assert.Throws<ArgumentNullException>(
                () => new StaticTestCaseCommandContext(testMethod, null, factory, arguments));
            Assert.Throws<ArgumentNullException>(
                () => new StaticTestCaseCommandContext(testMethod, actualMethod, null, arguments));
            Assert.Throws<ArgumentNullException>(
                () => new StaticTestCaseCommandContext(testMethod, actualMethod, factory, null));
        }

        [Fact]
        public void InitializeCorrectlyInitializes()
        {
            var testMethod = Mocked.Of<IMethodInfo>();
            var actualMethod = Mocked.Of<IMethodInfo>();
            var factory = Mocked.Of<ISpecimenBuilderFactory>();
            var arguments = new[] { new object(), new object() };

            var sut = new StaticTestCaseCommandContext(testMethod, actualMethod, factory, arguments);

            Assert.Equal(testMethod, sut.TestMethod);
            Assert.Equal(actualMethod, sut.ActualMethod);
            Assert.Equal(factory, sut.BuilderFactory);
            Assert.Equal(arguments, sut.ExplicitArguments);
        }

        [Fact]
        public void GetMethodContextWithNullTestObjectThrows()
        {
            var sut = new StaticTestCaseCommandContext(
                Mocked.Of<IMethodInfo>(),
                Mocked.Of<IMethodInfo>(),
                Mocked.Of<ISpecimenBuilderFactory>(),
                new object[0]);
            Assert.Throws<ArgumentNullException>(() => sut.GetMethodContext(null));
        }

        [Fact]
        public void GetMethodContextReturnsCorrectResult()
        {
            var testMethod = Mocked.Of<IMethodInfo>();
            var actualMethod = Mocked.Of<IMethodInfo>();
            var sut = new StaticTestCaseCommandContext(
                testMethod,
                actualMethod,
                Mocked.Of<ISpecimenBuilderFactory>(),
                new object[0]);
            var testObject = new object();

            var actual = sut.GetMethodContext(testObject);

            actual.AssertHasCorrectValues(testMethod.MethodInfo, actualMethod.MethodInfo, testObject, null);
        }

        [Fact]
        public void GetStaticMethodContextReturnsCorrectResult()
        {
            var testMethod = Mocked.Of<IMethodInfo>();
            var actualMethod = Mocked.Of<IMethodInfo>();
            var sut = new StaticTestCaseCommandContext(
                testMethod,
                actualMethod,
                Mocked.Of<ISpecimenBuilderFactory>(),
                new object[0]);

            var actual = sut.GetStaticMethodContext();

            actual.AssertHasCorrectValues(testMethod.MethodInfo, actualMethod.MethodInfo, null, null);
        }
    }
}
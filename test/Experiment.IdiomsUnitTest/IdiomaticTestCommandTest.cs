using System;
using System.Reflection;
using Ploeh.Albedo;
using Xunit;
using Xunit.Sdk;

namespace Jwc.Experiment
{
    public class IdiomaticTestCommandTest
    {
        [Fact]
        public void SutIsTestCommand()
        {
            var dummyMethod = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            var sut = new IdiomaticTestCommand(
                dummyMethod, new TypeElement(typeof(object)), new DelegatingReflectionVisitor<object>());
            Assert.IsAssignableFrom<TestCommand>(sut);
        }

        [Fact]
        public void InitializeWithNullMethodThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new IdiomaticTestCommand(
                    null, new TypeElement(typeof(object)), new DelegatingReflectionVisitor<object>()));
        }

        [Fact]
        public void InitializeWithNullReflectionElementThrows()
        {
            var dummyMethod = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            Assert.Throws<ArgumentNullException>(
                () => new IdiomaticTestCommand(
                    dummyMethod, null, new DelegatingReflectionVisitor<object>()));
        }

        [Fact]
        public void InitializeWithNullAssertionThrows()
        {
            var dummyMethod = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            Assert.Throws<ArgumentNullException>(
                () => new IdiomaticTestCommand(
                    dummyMethod, new TypeElement(typeof(object)), null));
        }

        [Fact]
        public void TimeoutIsCorrect()
        {
            var dummyMethod = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            var sut = new IdiomaticTestCommand(
                dummyMethod, new TypeElement(typeof(object)), new DelegatingReflectionVisitor<object>());

            var actual = sut.Timeout;

            Assert.Equal(0, actual);
        }

        [Fact]
        public void DisplayNameIsCorrect()
        {
            var dummyMethod = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            var typeElement = new TypeElement(typeof(object));
            var sut = new IdiomaticTestCommand(
                dummyMethod, typeElement, new DelegatingReflectionVisitor<object>());
            string expected = string.Format(
                "Jwc.Experiment.IdiomaticTestCommandTest.DisplayNameIsCorrect('{0}')",
                typeElement);

            var actual = sut.DisplayName;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MethodIsCorrect()
        {
            var method = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            var sut = new IdiomaticTestCommand(
                method, new TypeElement(typeof(object)), new DelegatingReflectionVisitor<object>());

            var actual = sut.Method;

            Assert.Equal(method, actual);
        }

        [Fact]
        public void ReflectionElementIsCorrect()
        {
            var dummyMethod = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            var reflectionElement = new TypeElement(typeof(object));
            var sut = new IdiomaticTestCommand(
                dummyMethod, reflectionElement, new DelegatingReflectionVisitor<object>());

            var actual = sut.ReflectionElement;

            Assert.Equal(reflectionElement, actual);
        }

        [Fact]
        public void AssertionIsCorrect()
        {
            var dummyMethod = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            var assertion = new DelegatingReflectionVisitor<object>();
            var sut = new IdiomaticTestCommand(
                dummyMethod, new TypeElement(typeof(object)), assertion);

            var actual = sut.Assertion;

            Assert.Equal(assertion, actual);
        }

        [Fact]
        public void ShouldCreateInstanceIsFalse()
        {
            var dummyMethod = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            var sut = new IdiomaticTestCommand(
                dummyMethod, new TypeElement(typeof(object)), new DelegatingReflectionVisitor<object>());

            var actual = sut.ShouldCreateInstance;

            Assert.False(actual, "ShouldCreateInstance.");
        }

        [Fact]
        public void ExecuteVerifiesAssertion()
        {
            bool verify = false;
            var element = new TypeElement(typeof(object));
            var assertion = new DelegatingReflectionVisitor<object>
            {
                OnVisitTypeElement = e =>
                {
                    Assert.Equal(element, e);
                    verify = true;
                    return new DelegatingReflectionVisitor<object>();
                }
            };
            var sut = new IdiomaticTestCommand(
                Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()), element, assertion);

            sut.Execute(null);

            Assert.True(verify, "verify.");
        }

        [Fact]
        public void ExecuteReturnsCorrectMethodResult()
        {
            var dummyMethod = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            var sut = new IdiomaticTestCommand(
                dummyMethod, new TypeElement(typeof(object)), new DelegatingReflectionVisitor<object>());

            var actual = sut.Execute(null);

            Assert.Equal(sut.DisplayName, actual.DisplayName);
            Assert.Equal(sut.MethodName, actual.MethodName);
        }
    }
}
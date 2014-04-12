using System;
using System.Reflection;
using Ploeh.Albedo;
using Xunit;
using Xunit.Sdk;

namespace Jwc.Experiment.Idioms
{
    public class IdiomaticTestCommandTest
    {
        [Fact]
        public void SutIsTestCommand()
        {
            var dummyMethod = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            var sut = new IdiomaticTestCommand(
                dummyMethod, new TypeElement(typeof(object)), new DelegatingReflectionVisitor());
            Assert.IsAssignableFrom<TestCommand>(sut);
        }

        [Fact]
        public void InitializeWithNullMethodThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new IdiomaticTestCommand(
                    null, new TypeElement(typeof(object)), new DelegatingReflectionVisitor()));
        }

        [Fact]
        public void InitializeWithNullReflectionElementThrows()
        {
            var dummyMethod = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            Assert.Throws<ArgumentNullException>(
                () => new IdiomaticTestCommand(
                    dummyMethod, null, new DelegatingReflectionVisitor()));
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
                dummyMethod, new TypeElement(typeof(object)), new DelegatingReflectionVisitor());

            var actual = sut.Timeout;

            Assert.Equal(0, actual);
        }

        [Fact]
        public void DisplayNameIsCorrect()
        {
            var dummyMethod = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            var sut = new IdiomaticTestCommand(
                dummyMethod, new TypeElement(typeof(object)), new DelegatingReflectionVisitor());
            const string expected = "Jwc.Experiment.Idioms.IdiomaticTestCommandTest.DisplayNameIsCorrect";

            var actual = sut.DisplayName;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MethodIsCorrect()
        {
            var method = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            var sut = new IdiomaticTestCommand(
                method, new TypeElement(typeof(object)), new DelegatingReflectionVisitor());

            var actual = sut.Method;

            Assert.Equal(method, actual);
        }

        [Fact]
        public void ReflectionElementIsCorrect()
        {
            var dummyMethod = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            var reflectionElement = new TypeElement(typeof(object));
            var sut = new IdiomaticTestCommand(
                dummyMethod, reflectionElement, new DelegatingReflectionVisitor());

            var actual = sut.ReflectionElement;

            Assert.Equal(reflectionElement, actual);
        }

        [Fact]
        public void AssertionIsCorrect()
        {
            var dummyMethod = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            var assertion = new DelegatingReflectionVisitor();
            var sut = new IdiomaticTestCommand(
                dummyMethod, new TypeElement(typeof(object)), assertion);

            var actual = sut.Assertion;

            Assert.Equal(assertion, actual);
        }
    }
}
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
    }
}
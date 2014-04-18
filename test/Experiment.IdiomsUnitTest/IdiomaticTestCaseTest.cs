using System;
using System.Reflection;
using Ploeh.Albedo;
using Xunit;
using Xunit.Sdk;

namespace Jwc.Experiment
{
    public class IdiomaticTestCaseTest
    {
        [Fact]
        public void SutIsTestCase()
        {
            var sut = new IdiomaticTestCase(new TypeElement(typeof(object)), new DelegatingAssertionFactory());
            Assert.IsAssignableFrom<ITestCase>(sut);
        }

        [Fact]
        public void InitializeWithNullReflectionElementThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new IdiomaticTestCase(null, new DelegatingAssertionFactory()));
        }

        [Fact]
        public void InitializeWithNullAssertionFactoryThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new IdiomaticTestCase(new TypeElement(typeof(object)), null));
        }

        [Fact]
        public void ReflectionElementIsCorrect()
        {
            var expected = new TypeElement(typeof(object));
            var sut = new IdiomaticTestCase(expected, new DelegatingAssertionFactory());

            var actual = sut.ReflectionElement;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void AssertionFactoryIsCorrect()
        {
            IAssertionFactory expected = new DelegatingAssertionFactory();
            var sut = new IdiomaticTestCase(new TypeElement(typeof(object)), expected);

            var actual = sut.AssertionFactory;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ConvertToTestCommandReturnsCorrectCommand()
        {
            // Fixture setup
            var fakeTestFixture = new DelegatingTestFixture();
            Func<MethodInfo, ITestFixture> fixtureFactory = mi => fakeTestFixture;

            var assertion = new DelegatingReflectionVisitor();
            var assetionFactory = new DelegatingAssertionFactory
            {
                OnCreate = f =>
                {
                    Assert.Equal(fakeTestFixture, f);
                    return assertion;
                }
            };

            var sut = new IdiomaticTestCase(new TypeElement(typeof(object)), assetionFactory);
            var method = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());

            // Exercise system
            var actual = sut.ConvertToTestCommand(method, fixtureFactory);

            // Verify outcome
            var command = Assert.IsAssignableFrom<IdiomaticTestCommand>(actual);
            Assert.Equal(method, command.Method);
            Assert.Equal(sut.ReflectionElement, command.ReflectionElement);
            Assert.Equal(assertion, command.Assertion);
        }

        [Fact]
        public void ConvertToTestCommandPassesCorrectMethodToFixtureFactory()
        {
            bool verify = false;
            var assetionFactory = new DelegatingAssertionFactory
            {
                OnCreate = f => new DelegatingReflectionVisitor()
            };
            var sut = new IdiomaticTestCase(
                new TypeElement(typeof(object)), assetionFactory);
            var method = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            Func<MethodInfo, ITestFixture> fixtureFactory = mi =>
            {
                Assert.Equal(method.MethodInfo, mi);
                verify = true;
                return new DelegatingTestFixture();
            };

            sut.ConvertToTestCommand(method, fixtureFactory);

            Assert.True(verify, "Verify.");
        }

        [Fact]
        public void ConvertToTestCommandCreatesFixtureOnlyOnce()
        {
            int createCount = 0;
            var assetionFactory = new DelegatingAssertionFactory
            {
                OnCreate = f => new DelegatingReflectionVisitor()
            };
            var sut = new IdiomaticTestCase(
                new TypeElement(typeof(object)), assetionFactory);
            var method = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            Func<MethodInfo, ITestFixture> fixtureFactory = mi =>
            {
                createCount++;
                return new DelegatingTestFixture();
            };

            sut.ConvertToTestCommand(method, fixtureFactory);

            Assert.Equal(1, createCount);
        }

        [Fact]
        public void ConvertNullMethodToTestCommandThrows()
        {
            var sut = new IdiomaticTestCase(
                new TypeElement(typeof(object)), new DelegatingAssertionFactory());
            Func<MethodInfo, ITestFixture> fixtureFactory = mi => new DelegatingTestFixture();
            Assert.Throws<ArgumentNullException>(() => sut.ConvertToTestCommand(null, fixtureFactory));
        }

        [Fact]
        public void ConvertToTestCommandWithNullFixtureFactoryThrows()
        {
            var sut = new IdiomaticTestCase(
                new TypeElement(typeof(object)), new DelegatingAssertionFactory());
            var method = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            Assert.Throws<ArgumentNullException>(() => sut.ConvertToTestCommand(method, null));
        }
    }
}
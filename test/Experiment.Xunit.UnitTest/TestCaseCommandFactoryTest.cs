namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Moq;
    using Ploeh.Albedo;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;
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
            var testMethod = Mocked.Of<IMethodInfo>(
                m => m.MethodInfo == Mocked.Of<MethodInfo>(i => i.ReturnType == returnType));

            var actual = sut.Create(testMethod, null);

            Assert.Empty(actual);
        }

        [Theory]
        [InlineData(typeof(IEnumerable<ITestCase>))]
        [InlineData(typeof(ITestCase[]))]
        [InlineData(typeof(IList<ITestCase>))]
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
            // Fixture setup
            var testMethod = Reflector.Wrap(new Methods<TestClass>().Select(x => x.TestMethod()));
            var sut = new TestCaseCommandFactory();
            var factory = Mocked.Of<ITestFixtureFactory>();

            // Exercise system
            var actual = sut.Create(testMethod, factory).ToArray();
            
            // Verify outcome
            Assert.Equal(3, actual.Length);
            foreach (var testCommand in actual)
            {
                var parameterizedCommand = Assert.IsAssignableFrom<ParameterizedCommand>(testCommand);
                var context = Assert.IsAssignableFrom<TestCaseCommandContext>(
                    parameterizedCommand.TestCommandContext);

                Assert.Equal(testMethod, context.TestMethod);
                Assert.Equal(TestClass.Method, context.ActualMethod.MethodInfo);
                Assert.Equal(TestClass.TestObject, context.ActualObject);
                Assert.Equal(factory, context.TestFixtureFactory);
                Assert.Equal(TestClass.Arguments, context.ExplicitArguments);
            }
        }

        [Fact]
        public void CreateWithStaticMethodReturnsCorrectCommands()
        {
            // Fixture setup
            var testMethod = Reflector.Wrap(new Methods<TestClass>().Select(x => x.StaticActualTestMethod()));
            var sut = new TestCaseCommandFactory();
            var factory = Mocked.Of<ITestFixtureFactory>();

            // Exercise system
            var actual = sut.Create(testMethod, factory).ToArray();

            // Verify outcome
            Assert.Equal(2, actual.Length);
            foreach (var testCommand in actual)
            {
                var parameterizedCommand = Assert.IsAssignableFrom<ParameterizedCommand>(testCommand);
                var context = Assert.IsAssignableFrom<StaticTestCaseCommandContext>(
                    parameterizedCommand.TestCommandContext);

                Assert.Equal(testMethod, context.TestMethod);
                Assert.Equal(TestClass.Method, context.ActualMethod.MethodInfo);
                Assert.Equal(factory, context.TestFixtureFactory);
                Assert.Equal(TestClass.Arguments, context.ExplicitArguments);
            }
        }

        [Fact]
        public void CreateWithStaticClassReturnsCorrectCommands()
        {
            var sut = new TestCaseCommandFactory();
            var testMethod = Reflector.Wrap(Methods.Select(() => StaticTestClass.TestMethod()));

            var actual = sut.Create(testMethod, Mocked.Of<ITestFixtureFactory>()).ToArray();

            Assert.Equal(1, actual.Length);
        }

        [Fact]
        public void CreateLazilyReturnsCommand()
        {
            var sut = new TestCaseCommandFactory();
            var testMethod = Reflector.Wrap(new Methods<TestClass>().Select(x => x.ThrowMethod()));
            Assert.DoesNotThrow(() =>
            {
                sut.Create(testMethod, Mocked.Of<ITestFixtureFactory>());
            });
        }

        [Fact]
        public void CreateReturnsCorrectCommandForParameterizedMethod()
        {
            var sut = new TestCaseCommandFactory();
            var testMethod = Reflector.Wrap(
                new Methods<TestClass>().Select(x => x.TestMethod(null, 0, null)));

            var actual = sut.Create(testMethod, new FakeTestFixtureFactory());

            Assert.True(actual.Any());
        }

        [Fact]
        public void CreatePassesCorrectMethodContextForParameterizedMethod()
        {
            var sut = new TestCaseCommandFactory();
            var method = new Methods<TestClass>().Select(x => x.TestMethod(null, 0, null));
            var factory = Mocked.Of<ITestFixtureFactory>();
            factory.ToMock()
                .Setup(x => x.Create(It.IsAny<ITestMethodContext>()))
                .Returns(new FakeTestFixture())
                .Callback<ITestMethodContext>(c =>
                {
                    Assert.Equal(method, c.TestMethod);
                    Assert.Equal(method, c.ActualMethod);
                    Assert.IsAssignableFrom<TestClass>(c.TestObject);
                    Assert.IsAssignableFrom<TestClass>(c.ActualObject);
                });

            sut.Create(Reflector.Wrap(method), factory).ToArray();

            factory.ToMock().VerifyAll();
        }

        [Fact]
        public void CreateDoesNotCreateTestFixtureForNonParameterizedMethod()
        {
            var sut = new TestCaseCommandFactory();
            var method = new Methods<TestClass>().Select(x => x.TestMethod());
            var factory = Mocked.Of<ITestFixtureFactory>();

            sut.Create(Reflector.Wrap(method), factory);

            factory.ToMock().Verify(x => x.Create(It.IsAny<ITestMethodContext>()), Times.Never());
        }

        private static class StaticTestClass
        {
            public static IEnumerable<ITestCase> TestMethod()
            {
                yield return Mocked.Of<ITestCase>();
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "To test members of test type.")]
        private class TestClass
        {
            public static readonly MethodInfo Method = new Methods<object>().Select(x => x.ToString());
            public static readonly object[] Arguments = new object[] { 123, "string" };
            public static object TestObject = new object();

            public IEnumerable<ITestCase> StaticActualTestMethod()
            {
                yield return Mocked.Of<ITestCase>(
                    t => t.Target == null && t.TestMethod == Method && t.Arguments == Arguments);
                yield return Mocked.Of<ITestCase>(
                    t => t.Target == null && t.TestMethod == Method && t.Arguments == Arguments);
            }

            public IEnumerable<ITestCase> TestMethod()
            {
                yield return Mocked.Of<ITestCase>(
                    t => t.Target == TestObject && t.TestMethod == Method && t.Arguments == Arguments);
                yield return Mocked.Of<ITestCase>(
                    t => t.Target == TestObject && t.TestMethod == Method && t.Arguments == Arguments);
                yield return Mocked.Of<ITestCase>(
                    t => t.Target == TestObject && t.TestMethod == Method && t.Arguments == Arguments);
            }

            public IEnumerable<ITestCase> ThrowMethod()
            {
                var testCase = Mocked.Of<ITestCase>();
                testCase.ToMock().SetupGet(x => x.TestMethod).Throws<Exception>();
                yield return testCase;
            }

            public IEnumerable<ITestCase> TestMethod(string arg1, int arg2, object arg3)
            {
                yield return Mocked.Of<ITestCase>();
            }
        }

        private class FakeTestFixtureFactory : ITestFixtureFactory
        {
            public ITestFixture Create(ITestMethodContext context)
            {
                var specimenContext = new SpecimenContext(new Fixture());
                var fixture = Mocked.Of<ITestFixture>();
                fixture.ToMock().Setup(x => x.Create(It.IsAny<object>()))
                    .Returns<object>(a => specimenContext.Resolve(a));
                return fixture;
            }
        }
    }
}
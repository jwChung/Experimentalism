﻿namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Ploeh.Albedo;
    using global::Xunit;
    using global::Xunit.Extensions;
    using global::Xunit.Sdk;

    public class TestCaseCommandFactory2Test
    {
        [Fact]
        public void SutIsTestCommandFactory()
        {
            var sut = new TestCaseCommandFactory2();
            Assert.IsAssignableFrom<ITestCommandFactory>(sut);
        }

        [Fact]
        public void CreateWithNullTestMethodThrows()
        {
            var sut = new TestCaseCommandFactory2();
            Assert.Throws<ArgumentNullException>(() => sut.Create(null, null).ToArray());
        }

        [Theory]
        [InlineData(typeof(void))]
        [InlineData(typeof(object))]
        public void CreateReturnsEmptyIfReturnTypeIsIncorrect(Type returnType)
        {
            var sut = new TestCaseCommandFactory2();
            var testMethod = Mocked.Of<IMethodInfo>(
                m => m.MethodInfo == Mocked.Of<MethodInfo>(i => i.ReturnType == returnType));

            var actual = sut.Create(testMethod, null);

            Assert.Empty(actual);
        }

        [Theory]
        [InlineData(typeof(IEnumerable<ITestCase2>))]
        [InlineData(typeof(ITestCase2[]))]
        [InlineData(typeof(IList<ITestCase2>))]
        public void CreateReturnsNonEmptyIfReturnTypeIsCorrect(Type returnType)
        {
            var sut = new TestCaseCommandFactory2();
            var testMethod = new Methods<TestClass>().Select(x => x.TestMethod());
            var factory = Mocked.Of<ITestFixtureFactory>();

            var actual = sut.Create(Reflector.Wrap(testMethod), factory);

            Assert.NotEmpty(actual);
        }

        [Fact]
        public void CreateReturnsEmptyIfTestMethodIsParameterized()
        {
            var sut = new TestCaseCommandFactory2();
            var delegator = new Func<object, int, IEnumerable<ITestCase2>>((x, y) => new ITestCase2[0]);

            var actual = sut.Create(Reflector.Wrap(delegator.Method), null);

            Assert.Empty(actual);
        }

        [Fact]
        public void CreateReturnsCorrectCommands()
        {
            // Fixture setup
            var testMethod = Reflector.Wrap(new Methods<TestClass>().Select(x => x.TestMethod()));
            var sut = new TestCaseCommandFactory2();
            var factory = Mocked.Of<ITestFixtureFactory>();

            // Exercise system
            var actual = sut.Create(testMethod, factory).ToArray();
            
            // Verify outcome
            Assert.Equal(3, actual.Length);
            foreach (var testCommand in actual)
            {
                var parameterizedCommand = Assert.IsAssignableFrom<ParameterizedCommand2>(testCommand);
                var context = Assert.IsAssignableFrom<TestCommandContext>(parameterizedCommand.TestCommandContext);

                Assert.Equal(testMethod, context.TestMethod);
                Assert.Equal(TestClass.Method, context.ActualMethod.MethodInfo);
                Assert.Equal(TestClass.TestObject, context.TestObject);
                Assert.Equal(factory, context.TestFixtureFactory);
                Assert.Equal(TestClass.Arguments, context.ExplicitArguments);
            }
        }

        [Fact]
        public void CreateWithStaticMethodReturnsCorrectCommands()
        {
            // Fixture setup
            var testMethod = Reflector.Wrap(Methods.Select(() => TestClass.StaticTestMethod()));
            var sut = new TestCaseCommandFactory2();
            var factory = Mocked.Of<ITestFixtureFactory>();

            // Exercise system
            var actual = sut.Create(testMethod, factory).ToArray();

            // Verify outcome
            Assert.Equal(2, actual.Length);
            foreach (var testCommand in actual)
            {
                var parameterizedCommand = Assert.IsAssignableFrom<ParameterizedCommand2>(testCommand);
                var context = Assert.IsAssignableFrom<TestCommandContext>(parameterizedCommand.TestCommandContext);

                Assert.Equal(testMethod, context.TestMethod);
                Assert.Equal(TestClass.Method, context.ActualMethod.MethodInfo);
                Assert.Equal(null, context.TestObject);
                Assert.Equal(factory, context.TestFixtureFactory);
                Assert.Equal(TestClass.Arguments, context.ExplicitArguments);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "To test members of test type.")]
        private class TestClass
        {
            public static readonly MethodInfo Method = new Methods<object>().Select(x => x.ToString());
            public static readonly object[] Arguments = new object[] { 123, "string" };
            public static object TestObject;

            public TestClass()
            {
                TestObject = this;
            }

            public static IEnumerable<ITestCase2> StaticTestMethod()
            {
                yield return Mocked.Of<ITestCase2>(t => t.TestMethod == Method && t.Arguments == Arguments);
                yield return Mocked.Of<ITestCase2>(t => t.TestMethod == Method && t.Arguments == Arguments);
            }

            public IEnumerable<ITestCase2> TestMethod()
            {
                yield return Mocked.Of<ITestCase2>(t => t.TestMethod == Method && t.Arguments == Arguments);
                yield return Mocked.Of<ITestCase2>(t => t.TestMethod == Method && t.Arguments == Arguments);
                yield return Mocked.Of<ITestCase2>(t => t.TestMethod == Method && t.Arguments == Arguments);
            }
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Extensions;
using Xunit.Sdk;

namespace Jwc.Experiment.Xunit
{
    public class FirstClassTestAttributeTest
    {
        [Fact]
        public void SutIsFactAttribute()
        {
            var sut = new TssFirstClassTestAttribute();
            Assert.IsAssignableFrom<FactAttribute>(sut);
        }

        [Fact]
        public void CreateTestCommandsReturnsCorrectCommands()
        {
            var sut = new TssFirstClassTestAttribute();
            const string methodName = "TestCasesTest";
            var method = Reflector.Wrap(GetType().GetMethod(methodName));

            var actual = sut.CreateTestCommands(method).ToArray();

            Assert.Equal(3, actual.Length);
            Array.ForEach(actual, c =>
            {
                var command = Assert.IsType<FactCommand>(c);
                Assert.Equal(methodName, command.MethodName);
            });
        }

        [Fact]
        public void CreateTestCommandsFromStaticReturnsCorrectCommands()
        {
            var sut = new TssFirstClassTestAttribute();
            const string methodName = "StaticTestCasesTest";
            var method = Reflector.Wrap(GetType().GetMethod(methodName));

            var actual = sut.CreateTestCommands(method).ToArray();

            Assert.Equal(3, actual.Length);
            Array.ForEach(actual, c =>
            {
                var command = Assert.IsType<FactCommand>(c);
                Assert.Equal(methodName, command.MethodName);
            });
        }

        [Fact]
        public void CreateTestCommandsWithNullMethodInfoThrows()
        {
            var sut = new TssFirstClassTestAttribute();
            Assert.Throws<ArgumentNullException>(() => sut.CreateTestCommands(null).ToArray());
        }

        [Fact]
        public void CreateTestCommandsPassesCorrectTestFixtureToTestCase()
        {
            const string methodName = "PassTestFixtureTest";
            var method = Reflector.Wrap(GetType().GetMethod(methodName));
            var sut = new TssFirstClassTestAttribute
            {
                OnCreateTestFixture = mi =>
                {
                    Assert.Equal(method.MethodInfo, mi);
                    return new FakeTestFixture();
                }
            };

            var actual = sut.CreateTestCommands(method).Single();

            Assert.IsType<FactCommand>(actual);
        }

        [Fact]
        public void CreateTestCommandsReturnsExceptionCommandWhenCreatingTestCaseThrows()
        {
            var sut = new TssFirstClassTestAttribute();
            var method = Reflector.Wrap(GetType().GetMethod("ExceptionFromCreatingTestCaseTest"));

            var actual = sut.CreateTestCommands(method).ToArray();

            Assert.Equal(2, actual.Length);
            var command = Assert.IsType<ExceptionCommand>(actual[1]);
            Assert.IsType<NotSupportedException>(command.Exception);
        }

        [Fact]
        public void CreateTestCommandsReturnsExceptionCommandWhenCreatingTestFixtureThrows()
        {
            var sut = new TssFirstClassTestAttribute
            {
                OnCreateTestFixture = mi => { throw new NotSupportedException(); }
            };
            var method = Reflector.Wrap(GetType().GetMethod("CallFixtureFactoryTest"));

            var actual = sut.CreateTestCommands(method).Single();

            var command = Assert.IsType<ExceptionCommand>(actual);
            Assert.IsType<NotSupportedException>(command.Exception);
        }

        [Theory]
        [InlineData("VoidReturnTypeTest")]
        [InlineData("InvalidReturnTypeTest")]
        public void CreateTestCommandsReturnsExceptionCommandWhenMethodReturnTypeIsInvalid(string methodName)
        {
            var sut = new TssFirstClassTestAttribute();
            var method = Reflector.Wrap(GetType().GetMethod(methodName));

            var actual = sut.CreateTestCommands(method).Single();

            var command = Assert.IsType<ExceptionCommand>(actual);
            Assert.IsType<ArgumentException>(command.Exception);
        }

        [Fact]
        public void CreateTestCommandsDoesNotThrowWhenMethodReturnTypeIsValid()
        {
            var sut = new TssFirstClassTestAttribute();
            var method = Reflector.Wrap(GetType().GetMethod("ValidReturnTypeTest"));

            var actual = sut.CreateTestCommands(method).Single();

            Assert.IsType<FactCommand>(actual);
        }

        [Fact]
        public void CreateTestCommandsReturnsExceptionCommandWhenMethodIsParameterized()
        {
            var sut = new TssFirstClassTestAttribute();
            var method = Reflector.Wrap(GetType().GetMethod("ParameterizedTest"));

            var actual = sut.CreateTestCommands(method).Single();

            var command = Assert.IsType<ExceptionCommand>(actual);
            Assert.IsType<ArgumentException>(command.Exception);
        }

        [Fact]
        public void CreateTestCommandsOfAbstractBaseClassReturnsCorrectTestCommand()
        {
            var sut = new TssFirstClassTestAttribute();
            var method = Reflector.Wrap(typeof(SubTestClass).GetMethod("FirstClassTest"));

            var actual = sut.CreateTestCommands(method).Single();

            Assert.IsType<FirstClassCommand>(actual);
        }

        [Fact]
        public void CreateTestCommandsReturnsCorrectExceptionCommandsWhenManyTestCasesThrows()
        {
            var sut = new TssFirstClassTestAttribute();
            var method = Reflector.Wrap(GetType().GetMethod("ManyExceptionTest"));

            var actual = sut.CreateTestCommands(method).ToArray();

            Assert.Equal(3, actual.Length);
            Array.ForEach(actual, c =>
            {
                var command = Assert.IsAssignableFrom<ExceptionCommand>(c);
                Assert.Equal(method.MethodInfo.Name, command.MethodName);
                Assert.IsType<NotSupportedException>(command.Exception);
            });
        }

        [Fact]
        public void CreateTestCommandsWithoutValidTestFixtureFactoryRetrunsExceptionCommand()
        {
            var sut = new FirstClassTestAttribute();
            const string methodName = "PassTestFixtureTest";
            var method = Reflector.Wrap(GetType().GetMethod(methodName));

            var actual = sut.CreateTestCommands(method).Single();

            var command = Assert.IsType<ExceptionCommand>(actual);
            var exception = Assert.IsType<NotSupportedException>(command.Exception);
            Assert.Contains("TestFixtureFactory.SetCurrent", exception.Message);
        }

        [Theory]
        [InlineData("CreateTestCommandsSetsUpFixtureOnlyOnceOnAssemblyLevel")]
        public void RunTestWithStaticFixture(string testMethod)
        {
            GetType().GetMethod(testMethod).Execute();
        }

        public void CreateTestCommandsSetsUpFixtureOnlyOnceOnAssemblyLevel()
        {
            var sut = new FirstClassTestAttribute();
            var method = Reflector.Wrap(GetType().GetMethod("PassTestFixtureTest"));

            sut.CreateTestCommands(method).ToArray();

            Assert.Equal(1, SpyInitalizer.SetupCount);
        }

        [FirstClassTest]
        public IEnumerable<ITestCase> CreateTestCommandsUsesCorrectTestFixtureFactory()
        {
            // Fixture setup
            var expected = new FakeTestFixture();
            var testMethod = (MethodInfo)MethodBase.GetCurrentMethod();
            var factory = new DelegatingTestFixtureFactory
            {
                OnCreate = m =>
                {
                    Assert.Equal(testMethod, m);
                    return expected;
                }
            };
            DefaultFixtureFactory.SetCurrent(factory);

            // Exercise system and Verify outcome
            yield return new DelegatingTestCase
            {
                OnConvertToTestCommand = (m, f) =>
                {
                    Assert.Equal(expected, f.Create(testMethod));
                    return new FactCommand(m);
                }
            };

            // Fixture teardown
            DefaultFixtureFactory.SetCurrent(null);
        }

        public IEnumerable<ITestCase> TestCasesTest()
        {
            yield return new DelegatingTestCase { OnConvertToTestCommand = (m, f) => new FactCommand(m) };
            yield return new DelegatingTestCase { OnConvertToTestCommand = (m, f) => new FactCommand(m) };
            yield return new DelegatingTestCase { OnConvertToTestCommand = (m, f) => new FactCommand(m) };
        }

        public IEnumerable<ITestCase> CallFixtureFactoryTest()
        {
            yield return new DelegatingTestCase
            {
                OnConvertToTestCommand = (m, f) =>
                {
                    f.Create(m.MethodInfo);
                    return new FactCommand(m);
                }
            };
        }

        public static IEnumerable<ITestCase> StaticTestCasesTest()
        {
            yield return new DelegatingTestCase { OnConvertToTestCommand = (m, f) => new FactCommand(m) };
            yield return new DelegatingTestCase { OnConvertToTestCommand = (m, f) => new FactCommand(m) };
            yield return new DelegatingTestCase { OnConvertToTestCommand = (m, f) => new FactCommand(m) };
        }

        public IEnumerable<ITestCase> PassTestFixtureTest()
        {
            yield return new DelegatingTestCase
            {
                OnConvertToTestCommand = (m, f) =>
                {
                    Assert.IsType<FakeTestFixture>(f.Create(m.MethodInfo));
                    return new FactCommand(m);
                }
            };
        }

        public IEnumerable<ITestCase> ExceptionFromCreatingTestCaseTest()
        {
            yield return new DelegatingTestCase { OnConvertToTestCommand = (m, f) => new FactCommand(m) };
            throw new NotSupportedException();
        }

        public void VoidReturnTypeTest()
        {
        }

        public ITestCase InvalidReturnTypeTest()
        {
            return null;
        }

        public IEnumerable<DelegatingTestCase> ValidReturnTypeTest()
        {
            yield return new DelegatingTestCase { OnConvertToTestCommand = (m, f) => new FactCommand(m) };
        }

        public IEnumerable<ITestCase> ParameterizedTest(object arg)
        {
            yield break;
        }

        public IEnumerable<ITestCase> ManyExceptionTest()
        {
            yield return new DelegatingTestCase
            {
                OnConvertToTestCommand = (m, f) => { throw new NotSupportedException(); }
            };
            yield return new DelegatingTestCase
            {
                OnConvertToTestCommand = (m, f) => { throw new NotSupportedException(); }
            };
            yield return new DelegatingTestCase
            {
                OnConvertToTestCommand = (m, f) => { throw new NotSupportedException(); }
            };
        }

        public IEnumerable<ITestCase> EmptyTestCases()
        {
            yield break;
        }

        private class TssFirstClassTestAttribute : FirstClassTestAttribute
        {
            public Func<MethodInfo, ITestFixture> OnCreateTestFixture
            {
                get;
                set;
            }

            protected override ITestFixture CreateTestFixture(MethodInfo testMethod)
            {
                return OnCreateTestFixture(testMethod);
            }
        }

        private abstract class BaseTestClass
        {
            public IEnumerable<ITestCase> FirstClassTest()
            {
                yield return new TestCase(() => { });
            }
        }

        private class SubTestClass : BaseTestClass
        {
        }
    }
}
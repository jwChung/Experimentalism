using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Extensions;
using Xunit.Sdk;

namespace Jwc.Experiment
{
    public class FirstClassTestAttributeTest : MarshalByRefObject
    {
        [Fact]
        public void SutIsFactAttribute()
        {
            var sut = new TestSpecificFirstClassTheoremAttribute();
            Assert.IsAssignableFrom<FactAttribute>(sut);
        }

        [Fact]
        public void CreateTestCommandsReturnsCorrectCommands()
        {
            var sut = new TestSpecificFirstClassTheoremAttribute();
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
            var sut = new TestSpecificFirstClassTheoremAttribute();
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
            var sut = new TestSpecificFirstClassTheoremAttribute();
            Assert.Throws<ArgumentNullException>(() => sut.CreateTestCommands(null).ToArray());
        }

        [Fact]
        public void CreateTestCommandsPassesCorrectTestFixtureToTestCase()
        {
            const string methodName = "PassTestFixtureTest";
            var method = Reflector.Wrap(GetType().GetMethod(methodName));
            var sut = new TestSpecificFirstClassTheoremAttribute
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
            var sut = new TestSpecificFirstClassTheoremAttribute();
            var method = Reflector.Wrap(GetType().GetMethod("ExceptionFromCreatingTestCaseTest"));

            var actual = sut.CreateTestCommands(method).ToArray();

            Assert.Equal(2, actual.Length);
            var command = Assert.IsType<ExceptionCommand>(actual[1]);
            Assert.IsType<NotSupportedException>(command.Exception);
        }

        [Fact]
        public void CreateTestCommandsReturnsExceptionCommandWhenCreatingTestFixtureThrows()
        {
            var sut = new TestSpecificFirstClassTheoremAttribute
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
            var sut = new TestSpecificFirstClassTheoremAttribute();
            var method = Reflector.Wrap(GetType().GetMethod(methodName));

            var actual = sut.CreateTestCommands(method).Single();

            var command = Assert.IsType<ExceptionCommand>(actual);
            Assert.IsType<ArgumentException>(command.Exception);
        }

        [Fact]
        public void CreateTestCommandsDoesNotThrowWhenMethodReturnTypeIsValid()
        {
            var sut = new TestSpecificFirstClassTheoremAttribute();
            var method = Reflector.Wrap(GetType().GetMethod("ValidReturnTypeTest"));

            var actual = sut.CreateTestCommands(method).Single();

            Assert.IsType<FactCommand>(actual);
        }

        [Fact]
        public void CreateTestCommandsReturnsExceptionCommandWhenMethodIsParameterized()
        {
            var sut = new TestSpecificFirstClassTheoremAttribute();
            var method = Reflector.Wrap(GetType().GetMethod("ParameterizedTest"));

            var actual = sut.CreateTestCommands(method).Single();

            var command = Assert.IsType<ExceptionCommand>(actual);
            Assert.IsType<ArgumentException>(command.Exception);
        }

        [Fact]
        public void CreateTestCommandsOfAbstractBaseClassReturnsCorrectTestCommand()
        {
            var sut = new TestSpecificFirstClassTheoremAttribute();
            var method = Reflector.Wrap(typeof(SubTestClass).GetMethod("FirstClassTest"));

            var actual = sut.CreateTestCommands(method).Single();

            Assert.IsType<FirstClassCommand>(actual);
        }

        [Fact]
        public void CreateTestCommandsReturnsCorrectExceptionCommandsWhenManyTestCasesThrows()
        {
            var sut = new TestSpecificFirstClassTheoremAttribute();
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

        [Fact(Skip = "Explicitly run this test without TestFixtureDeclarationAttribute in which the line should be commented out.")]
        public void CreateTestCommandsWithoutTestFixtureFactoryAttributeRetrunsExceptionCommand()
        {
            var sut = new FirstClassTestAttribute();
            const string methodName = "PassTestFixtureTest";
            var method = Reflector.Wrap(GetType().GetMethod(methodName));

            var actual = sut.CreateTestCommands(method).Single();

            var command = Assert.IsType<ExceptionCommand>(actual);
            var exception = Assert.IsType<NotSupportedException>(command.Exception);
            Assert.Contains("TestFixtureDeclarationAttribute", exception.Message);
        }

        [Theory]
        [InlineData("CreateTestCommandsWithTestFixtureFactoryAttributePassesCorrectTestFixtureToTestCase")]
        [InlineData("CreateTestCommandsSeveralTimesCreatesTestFixtureOnlyOnce")]
        public void RunTestInIndependentAppDomin(string testMethod)
        {
            var appDomainSetup = new AppDomainSetup { ApplicationBase = Environment.CurrentDirectory };
            var appDomain = AppDomain.CreateDomain(testMethod, null, appDomainSetup);
            try
            {
                var target = appDomain.CreateInstanceAndUnwrap(
                    Assembly.GetExecutingAssembly().FullName,
                    GetType().FullName);
                GetType().GetMethod(testMethod).Invoke(target, new object[0]);
            }
            finally
            {
                AppDomain.Unload(appDomain);
            }
        }

        public void CreateTestCommandsWithTestFixtureFactoryAttributePassesCorrectTestFixtureToTestCase()
        {
            var sut = new FirstClassTestAttribute();
            const string methodName = "PassTestFixtureTest";
            var method = Reflector.Wrap(GetType().GetMethod(methodName));
            DelegatingStaticTestFixtureFactory.OnCreate = mi =>
            {
                Assert.Equal(method.MethodInfo, mi);
                return new FakeTestFixture();
            };

            var actual = sut.CreateTestCommands(method).Single();

            Assert.IsType<FactCommand>(actual);
        }

        public void CreateTestCommandsSeveralTimesCreatesTestFixtureOnlyOnce()
        {
            var sut = new FirstClassTestAttribute();
            const string methodName = "PassTestFixtureTest";
            var method = Reflector.Wrap(GetType().GetMethod(methodName));

            sut.CreateTestCommands(method).Single();
            sut.CreateTestCommands(method).Single();

            Assert.Equal(1, DelegatingStaticTestFixtureFactory.ConstructCount);
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

        private class TestSpecificFirstClassTheoremAttribute : FirstClassTestAttribute
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
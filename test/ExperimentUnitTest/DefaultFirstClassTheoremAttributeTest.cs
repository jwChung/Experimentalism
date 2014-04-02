using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Extensions;
using Xunit.Sdk;

namespace Jwc.Experiment
{
    public class DefaultFirstClassTheoremAttributeTest
    {
        [Fact]
        public void SutIsFactAttribute()
        {
            var sut = new DefaultFirstClassTheoremAttribute();
            Assert.IsAssignableFrom<FactAttribute>(sut);
        }

        [Fact]
        public void CreateTestCommandsReturnsCorrectCommands()
        {
            var sut = new DefaultFirstClassTheoremAttribute();
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
            var sut = new DefaultFirstClassTheoremAttribute();
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
        public void FixtureFactoryIsCorrectWhenInitializedWithDefaultCtor()
        {
            var sut = new DefaultFirstClassTheoremAttribute();

            var actual = sut.FixtureFactory;

            var factory = Assert.IsType<TypeFixtureFactory>(actual);
            Assert.Equal(typeof(NotSupportedFixture), factory.FixtureType);
        }

        [Fact]
        public void FixtureFactoryIsCorrectWhenInitializedWithType()
        {
            Type fixtureType = typeof(FakeTestFixture);
            var sut = new DefaultFirstClassTheoremAttribute(fixtureType);

            var actual = sut.FixtureFactory;

            var fixtureFactory = Assert.IsType<TypeFixtureFactory>(actual);
            Assert.Equal(fixtureType, fixtureFactory.FixtureType);
        }

        [Fact]
        public void FixtureFactoryIsCorrectWhenInitializedWithFixtureFactory()
        {
            var fixtureFactory = new DelegatingFixtureFactory();
            var sut = new DerivedFirstClassTheoremAttribute(fixtureFactory);

            var actual = sut.FixtureFactory;

            Assert.Equal(fixtureFactory, actual);
        }

        [Fact]
        public void CreateTestCommandsWithNullMethodInfoThrows()
        {
            var sut = new DefaultFirstClassTheoremAttribute();
            Assert.Throws<ArgumentNullException>(() => sut.CreateTestCommands(null));
        }

        [Fact]
        public void FixtureTypeIsCorrectWhenInitializedWithDefaultCtor()
        {
            var sut = new DefaultFirstClassTheoremAttribute();
            var actual = sut.FixtureType;
            Assert.Equal(typeof(NotSupportedFixture), actual);
        }

        [Fact]
        public void FixtureTypeIsCorrectWhenInitializedWithFixtureType()
        {
            var fixtureType = typeof(FakeTestFixture);
            var sut = new DefaultFirstClassTheoremAttribute(fixtureType);

            var actual = sut.FixtureType;

            Assert.Equal(fixtureType, actual);
        }

        [Fact]
        public void FixtureTypeIsCorrectWhenInitializedWithFixtureFactory()
        {
            var fixtureFactory = new DelegatingFixtureFactory
            {
                OnCreate = mi =>
                {
                    Assert.NotNull(mi);
                    return new FakeTestFixture();
                }
            };
            var sut = new DerivedFirstClassTheoremAttribute(fixtureFactory);

            var actual = sut.FixtureType;

            Assert.Equal(typeof(FakeTestFixture), actual);
        }

        [Fact]
        public void InitializeWithNullFixtureTypeThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new DefaultFirstClassTheoremAttribute(null));
        }

        [Fact]
        public void InitializeWithNullFixtureFactoryThrows()
        {
            ITestFixtureFactory fixtureFactory = null;
            Assert.Throws<ArgumentNullException>(
                () => new DerivedFirstClassTheoremAttribute(fixtureFactory));
        }

        [Fact]
        public void CreateTestCommandsPassesTestFixtureToTestCase()
        {
            // Fixture setup
            var sut = new DerivedFirstClassTheoremAttribute(
                new DelegatingFixtureFactory { OnCreate = mi => new FakeTestFixture() });
            const string methodName = "PassTestFixtureTest";
            var method = Reflector.Wrap(GetType().GetMethod(methodName));

            // Exercise system
            var actual = sut.CreateTestCommands(method).Single();

            // Verify outcome
            Assert.IsType<FactCommand>(actual);
        }

        [Fact]
        public void CreateTestCommandsReturnsExceptionCommandWhenCreatingTestCaseThrows()
        {
            var sut = new DefaultFirstClassTheoremAttribute();
            var method = Reflector.Wrap(GetType().GetMethod("ExceptionFromCreatingTestCaseTest"));

            var actual = sut.CreateTestCommands(method).Single();

            var command = Assert.IsType<ExceptionCommand>(actual);
            Assert.IsType<NotSupportedException>(command.Exception);
        }

        [Fact]
        public void CreateTestCommandsReturnsExceptionCommandWhenCreatingTestFixtureThrows()
        {
            var sut = new DerivedFirstClassTheoremAttribute(new DelegatingFixtureFactory
            {
                OnCreate = mi =>
                {
                    throw new NotSupportedException();
                }
            });
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
            var sut = new DefaultFirstClassTheoremAttribute();
            var method = Reflector.Wrap(GetType().GetMethod(methodName));

            var actual = sut.CreateTestCommands(method).Single();

            var command = Assert.IsType<ExceptionCommand>(actual);
            Assert.IsType<ArgumentException>(command.Exception);
        }

        [Fact]
        public void CreateTestCommandsDoesNotThrowWhenMethodReturnTypeIsValid()
        {
            var sut = new DefaultFirstClassTheoremAttribute();
            var method = Reflector.Wrap(GetType().GetMethod("ValidReturnTypeTest"));

            var actual = sut.CreateTestCommands(method).Single();

            Assert.IsType<FactCommand>(actual);
        }

        [Fact]
        public void CreateTestCommandsThrowsWhenMethodIsParameterized()
        {
            var sut = new DefaultFirstClassTheoremAttribute();
            var method = Reflector.Wrap(GetType().GetMethod("ParameterizedTest"));

            var actual = sut.CreateTestCommands(method).Single();

            var command = Assert.IsType<ExceptionCommand>(actual);
            Assert.IsType<ArgumentException>(command.Exception);
        }

        [Fact]
        public void CreateTestCommandsOfAbstractBaseClassReturnsCorrectTestCommand()
        {
            var sut = new DefaultFirstClassTheoremAttribute();
            var method = Reflector.Wrap(typeof(SubTestClass).GetMethod("FirstClassTest"));

            var actual = sut.CreateTestCommands(method).Single();

            Assert.IsType<FirstClassCommand>(actual);
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

        private class DerivedFirstClassTheoremAttribute : DefaultFirstClassTheoremAttribute
        {
            public DerivedFirstClassTheoremAttribute(ITestFixtureFactory fixtureFactory)
                : base(fixtureFactory)
            {
            }
        }

        private abstract class BaseTestClass
        {
            public IEnumerable<ITestCase> FirstClassTest()
            {
                yield return TestCase.New(() => { });
            }
        }

        private class SubTestClass : BaseTestClass
        {
        }
    }
}
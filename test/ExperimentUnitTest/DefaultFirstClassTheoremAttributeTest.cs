using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        public void FixtureFactoryIsCorrect()
        {
            var sut = new DefaultFirstClassTheoremAttribute();
            var actual = sut.FixtureFactory;
            Assert.IsType<NotSupportedFixture>(actual.Invoke(null));
        }

        [Fact]
        public void FixtureFactoryInitializedWithTypeIsCorrect()
        {
            var sut = new DefaultFirstClassTheoremAttribute(typeof(FakeTestFixture));

            var actual = sut.FixtureFactory;

            Assert.IsType<FakeTestFixture>(actual(null));
            Assert.NotSame(actual(null), actual(null));
        }

        [Fact]
        public void FixtureFactoryInitializedWithFuncOfITestFixtureIsCorrect()
        {
            var sut = new DerivedFirstClassTheoremAttribute(() => new FakeTestFixture());
            var actual = sut.FixtureFactory;
            Assert.IsType<FakeTestFixture>(actual.Invoke(null));
        }

        [Fact]
        public void FixtureFactoryInitializedWithFuncOfMethodInfoAndITestFixtureIsCorrect()
        {
            Func<MethodInfo, ITestFixture> expected = mi => new FakeTestFixture();
            var sut = new DerivedFirstClassTheoremAttribute(expected);

            var actual = sut.FixtureFactory;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateTestCommandsWithNullMethodInfoThrows()
        {
            var sut = new DefaultFirstClassTheoremAttribute();
            Assert.Throws<ArgumentNullException>(() => sut.CreateTestCommands(null));
        }

        [Fact]
        public void FixtureTypeIsCorrect()
        {
            var sut = new DefaultFirstClassTheoremAttribute();
            var actual = sut.FixtureType;
            Assert.Equal(typeof(NotSupportedFixture), actual);
        }

        [Fact]
        public void FixtureTypeInitializedWithFixtureTypeIsCorrect()
        {
            var fixtureType = typeof(FakeTestFixture);
            var sut = new DefaultFirstClassTheoremAttribute(fixtureType);

            var actual = sut.FixtureType;

            Assert.Equal(fixtureType, actual);
        }

        [Fact]
        public void FixtureTypeInitializedWithFuncOfITestFixtureIsCorrect()
        {
            var sut = new DerivedFirstClassTheoremAttribute(() => new FakeTestFixture());
            var actual = sut.FixtureType;
            Assert.Equal(typeof(FakeTestFixture), actual);
        }

        [Fact]
        public void FixtureTypeInitializedWithFuncOfMethodInfoAndITestFixtureIsCorrect()
        {
            Func<MethodInfo, ITestFixture> fixtureFactory = mi =>
            {
                if (mi == null)
                    throw new ArgumentNullException("mi");
                return new FakeTestFixture();
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
        public void InitializeWithNullFuncOfITestFixtureThrows()
        {
            Func<ITestFixture> fixtureFactory = null;
            Assert.Throws<ArgumentNullException>(
                () => new DerivedFirstClassTheoremAttribute(fixtureFactory));
        }

        [Fact]
        public void InitializeWithNullFuncOfMethodInfoAndITestFixtureThrows()
        {
            Func<MethodInfo, ITestFixture> fixtureFactory = null;
            Assert.Throws<ArgumentNullException>(
                () => new DerivedFirstClassTheoremAttribute(fixtureFactory));
        }

        [Fact]
        public void CreateTestCommandsPassesTestFixtureToTestCase()
        {
            // Fixture setup
            var sut = new DefaultFirstClassTheoremAttribute(typeof(FakeTestFixture));
            const string methodName = "PassTestFixtureTest";
            var method = Reflector.Wrap(GetType().GetMethod(methodName));

            // Exercise system and Verify outcome
            Assert.DoesNotThrow(() => sut.CreateTestCommands(method).Single());
        }

        [Fact]
        public void CreateTestCommandsCreatesTestFixtureForEachTestCase()
        {
            int creatCount = 0;
            var sut = new DerivedFirstClassTheoremAttribute(() =>
            {
                creatCount++;
                return new FakeTestFixture();
            });
            const string methodName = "TestCasesTest";
            var method = Reflector.Wrap(GetType().GetMethod(methodName));

            sut.CreateTestCommands(method);

            Assert.Equal(3, creatCount);
        }

        [Fact]
        public void CreateTestCommandsReturnsExceptionCommandIfCreatingTestCaseThrows()
        {
            var sut = new DefaultFirstClassTheoremAttribute();
            var method = Reflector.Wrap(GetType().GetMethod("ExceptionFromCreatingTestCaseTest"));

            var actual = sut.CreateTestCommands(method).Single();

            var command = Assert.IsType<ExceptionCommand>(actual);
            Assert.IsType<NotSupportedException>(command.Exception);
        }

        [Fact]
        public void CreateTestCommandsReturnsExceptionCommandIfCreatingTestFixtureThrows()
        {
            var sut = new DerivedFirstClassTheoremAttribute(() =>
            {
                throw new NotSupportedException();
            });
            var method = Reflector.Wrap(GetType().GetMethod("TestCasesTest"));

            var actual = sut.CreateTestCommands(method).Single();

            var command = Assert.IsType<ExceptionCommand>(actual);
            Assert.IsType<NotSupportedException>(command.Exception);
        }

        [Theory]
        [InlineData("VoidReturnTypeTest")]
        [InlineData("InvalidReturnTypeTest")]
        public void CreateTestCommandsReturnsExceptionCommandIfMethodReturnTypeIsInvalid(string methodName)
        {
            var sut = new DefaultFirstClassTheoremAttribute();
            var method = Reflector.Wrap(GetType().GetMethod(methodName));

            var actual = sut.CreateTestCommands(method).Single();

            var command = Assert.IsType<ExceptionCommand>(actual);
            Assert.IsType<ArgumentException>(command.Exception);
        }

        [Fact]
        public void CreateTestCommandsDoesNotThrowIfMethodReturnTypeIsValid()
        {
            var sut = new DefaultFirstClassTheoremAttribute();
            var method = Reflector.Wrap(GetType().GetMethod("ValidReturnTypeTest"));

            var actual = sut.CreateTestCommands(method).Single();

            Assert.IsType<FactCommand>(actual);
        }

        [Fact]
        public void CreateTestCommandsThrowsIfMethodHasParemeters()
        {
            var sut = new DefaultFirstClassTheoremAttribute();
            var method = Reflector.Wrap(GetType().GetMethod("ParameterizedTest"));

            var actual = sut.CreateTestCommands(method).Single();

            var command = Assert.IsType<ExceptionCommand>(actual);
            Assert.IsType<ArgumentException>(command.Exception);
        }

        public IEnumerable<ITestCase> TestCasesTest()
        {
            yield return new FakeTestCase { OnConvertToTestCommand = (m, f) => new FactCommand(m) };
            yield return new FakeTestCase { OnConvertToTestCommand = (m, f) => new FactCommand(m) };
            yield return new FakeTestCase { OnConvertToTestCommand = (m, f) => new FactCommand(m) };
        }

        public static IEnumerable<ITestCase> StaticTestCasesTest()
        {
            yield return new FakeTestCase { OnConvertToTestCommand = (m, f) => new FactCommand(m) };
            yield return new FakeTestCase { OnConvertToTestCommand = (m, f) => new FactCommand(m) };
            yield return new FakeTestCase { OnConvertToTestCommand = (m, f) => new FactCommand(m) };
        }

        public IEnumerable<ITestCase> PassTestFixtureTest()
        {
            yield return new FakeTestCase
            {
                OnConvertToTestCommand = (m, f) =>
                {
                    Assert.IsType<FakeTestFixture>(f);
                    return null;
                }
            };
        }

        public IEnumerable<ITestCase> ExceptionFromCreatingTestCaseTest()
        {
            yield return new FakeTestCase { OnConvertToTestCommand = (m, f) => new FactCommand(m) };
            throw new NotSupportedException();
        }

        public void VoidReturnTypeTest()
        {
        }

        public ITestCase InvalidReturnTypeTest()
        {
            return null;
        }

        public IEnumerable<FakeTestCase> ValidReturnTypeTest()
        {
            yield return new FakeTestCase { OnConvertToTestCommand = (m, f) => new FactCommand(m) };
        }

        public IEnumerable<ITestCase> ParameterizedTest(object arg)
        {
            yield break;
        }

        private class DerivedFirstClassTheoremAttribute : DefaultFirstClassTheoremAttribute
        {
            public DerivedFirstClassTheoremAttribute(Func<ITestFixture> fixtureFactory)
                : base(fixtureFactory)
            {
            }

            public DerivedFirstClassTheoremAttribute(Func<MethodInfo, ITestFixture> fixtureFactory)
                : base(fixtureFactory)
            {
            }
        }
    }
}
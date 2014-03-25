using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Extensions;
using Xunit.Sdk;

namespace Jwc.Experiment
{
    public class NaiveTheoremAttributeTest
    {
        [Fact]
        public void SutIsFactAttribute()
        {
            var sut = new NaiveTheoremAttribute();
            Assert.IsAssignableFrom<FactAttribute>(sut);
        }

        [Fact]
        public void CreateNonParameterizedReturnsFactCommand()
        {
            var sut = new NaiveTheoremAttribute();
            
            var actual = sut.CreateTestCommands(Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()));

            var factCommand = Assert.IsType<FactCommand>(actual.Single());
            Console.WriteLine(factCommand.MethodName);
        }

        [Theory]
        [InlineData("dummy", 1, null)]
        [InlineData("dummy", 1, null)]
        [InlineData("dummy", 1, null)]
        public void CreateParameterizedReturnsThoeryCommands(string arg1, int arg2, object arg3)
        {
            var sut = new NaiveTheoremAttribute();

            var actual = sut.CreateTestCommands(Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod())).ToArray();

            Assert.Equal(3, actual.Length);
            Array.ForEach(actual, c =>
            {
                var theoryCommand = Assert.IsType<TheoryCommand>(c);
                Assert.Equal(new[] { arg1, arg2, arg3 }, theoryCommand.Parameters);
            });
        }

        [Fact]
        public void InitializeWithNullFuncOfITestFixtureThrows()
        {
            Func<ITestFixture> fixtureFactory = null;
            Assert.Throws<ArgumentNullException>(() => new DerivedTheoremAttribute(fixtureFactory));
        }

        [Fact]
        public void FixtureFactoryIsCorrect()
        {
            var sut = new NaiveTheoremAttribute();
            var actual = sut.FixtureFactory;
            Assert.IsType<NotSupportedFixture>(actual.Invoke(null));
        }

        [Fact]
        public void FixtureFactoryInitializedWithFuncOfITestFixtureIsCorrect()
        {
            var sut = new DerivedTheoremAttribute(() => new FakeTestFixture());
            var actual = sut.FixtureFactory;
            Assert.IsType<FakeTestFixture>(actual.Invoke(null));
        }

        [Fact]
        public void CreateParameterizedWithAutoDataReturnsCorrectCommands()
        {
            var fixture = new FakeTestFixture();
            var sut = new DerivedTheoremAttribute(() => fixture);
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithAutoData"));

            var actual = sut.CreateTestCommands(method).ToArray();

            Assert.Equal(2, actual.Length);
            Array.ForEach(actual, c =>
            {
                var theoryCommand = Assert.IsType<TheoryCommand>(c);
                Assert.Equal(
                    new object[] { fixture.StringValue, fixture.IntValue },
                    theoryCommand.Parameters);
            });
        }

        [Fact]
        public void CreateParameterizedWithMixedDataReturnsCorrectCommands()
        {
            var fixture = new FakeTestFixture();
            var sut = new DerivedTheoremAttribute(() => fixture);
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithMixedData"));

            var actual = sut.CreateTestCommands(method);

            var theoryCommand = Assert.IsType<TheoryCommand>(actual.Single());
            Assert.Equal(
                new object[] { "expected", fixture.IntValue },
                theoryCommand.Parameters);
        }

        [Fact]
        public void CreateParameterizedWithInvalidCountDataThrows()
        {
            var sut = new NaiveTheoremAttribute();
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithInvalidCountData"));

            var actual = sut.CreateTestCommands(method);

            var theoryCommand = Assert.IsType<TheoryCommand>(actual.Single());
            Assert.Throws<InvalidOperationException>(() => theoryCommand.Execute(this));
        }

        [Fact]
        public void CreateParameterizedWithInvalidTypeDataThrows()
        {
            var sut = new NaiveTheoremAttribute();
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithInvalidTypeData"));

            var actual = sut.CreateTestCommands(method);

            var theoryCommand = Assert.IsType<TheoryCommand>(actual.Single());
            Assert.Throws<ArgumentException>(() => theoryCommand.Execute(this));
        }

        [Fact]
        public void FixtureFactoryInitializedWithTypeIsCorrect()
        {
            var sut = new NaiveTheoremAttribute(typeof(FakeTestFixture));

            var actual = sut.FixtureFactory;

            Assert.IsType<FakeTestFixture>(actual(null));
            Assert.NotSame(actual(null), actual(null));
        }

        [Fact]
        public void InitializeWithNullFactoryTypeThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new NaiveTheoremAttribute(null));
        }

        [Fact]
        public void CreateParameterizedWithAutoDataNotUsingDataAttributeReturnsCorrectCommand()
        {
            var fixture = new FakeTestFixture();
            var sut = new DerivedTheoremAttribute(() => fixture);
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithAutoDataNotUsingDataAttribute"));

            var actual = sut.CreateTestCommands(method);

            var theoryCommand = Assert.IsType<TheoryCommand>(actual.Single());
            Assert.Equal(
                new object[] { fixture.StringValue, fixture.IntValue },
                theoryCommand.Parameters);
        }

        [Fact]
        public void CreateParameterizedPassesCorrectParameterTypes()
        {
            var sut = new NaiveTheoremAttribute();
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedForParameterTypes"));
            Assert.DoesNotThrow(() => sut.CreateTestCommands(method).Single());
        }

        [Theory]
        [InlineData("dummy", 1, null)]
        public void CreateParameterizedDoesNotInitializeFixture(string arg1, int arg2, object arg3)
        {
            Func<ITestFixture> fixtureFactory = () =>
            {
                throw new NotSupportedException();
            };
            var sut = new DerivedTheoremAttribute(fixtureFactory);
            Assert.DoesNotThrow(() => sut.CreateTestCommands(
                Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod())).ToArray());
        }

        [Fact]
        public void CreateParameterizedWithAutoDataInitializesFixtureForEachTestCase()
        {
            var fixture = new FakeTestFixture();
            int callCount = 0;
            Func<ITestFixture> fixtureFactory = () =>
            {
                callCount++;
                return fixture;
            };
            var sut = new DerivedTheoremAttribute(fixtureFactory);
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithAutoData"));

            sut.CreateTestCommands(method).ToArray();

            Assert.Equal(2, callCount);
        }

        [Fact]
        public void CreateParameterizedWithMixedDataInitializesFixtureForEachTestCase()
        {
            var fixture = new FakeTestFixture();
            int callCount = 0;
            Func<ITestFixture> fixtureFactory = () =>
            {
                callCount++;
                return fixture;
            };
            var sut = new DerivedTheoremAttribute(fixtureFactory);
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithMixedData"));

            sut.CreateTestCommands(method).ToArray();

            Assert.Equal(1, callCount);
        }

        [Fact]
        public void CreateParameterizedWithAutoDataNotUsingDataAttributeInitializesFixtureForEachTestCase()
        {
            var fixture = new FakeTestFixture();
            int callCount = 0;
            Func<ITestFixture> fixtureFactory = () =>
            {
                callCount++;
                return fixture;
            };
            var sut = new DerivedTheoremAttribute(fixtureFactory);
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithAutoDataNotUsingDataAttribute"));

            sut.CreateTestCommands(method).ToArray();

            Assert.Equal(1, callCount);
        }

        [Fact]
        public void CreateNonParameterizedDoesNotInitializeFixture()
        {
            Func<ITestFixture> fixtureFactory = () =>
            {
                throw new NotSupportedException();
            };
            var sut = new DerivedTheoremAttribute(fixtureFactory);
            Assert.DoesNotThrow(() => sut.CreateTestCommands(
                Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod())).ToArray());
        }

        [Fact]
        public void CreateParameterizedForSingleIfExceptionIsThrownReturnsCorrectExceptionCommand()
        {
            var exception = new NotSupportedException();
            Func<ITestFixture> fixtureFactory = () =>
            {
                throw exception;
            };
            var sut = new DerivedTheoremAttribute(fixtureFactory);
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithAutoDataNotUsingDataAttribute"));

            var actual = sut.CreateTestCommands(method).Single();

            var command = Assert.IsAssignableFrom<ExceptionCommand>(actual);
            Assert.Equal(method.MethodInfo.Name, command.MethodName);
            Assert.Equal(exception, command.Exception);
        }

        [Fact]
        public void CreateParameterizedForManyIfExceptionIsThrownReturnsCorrectExceptionCommand()
        {
            var exception = new NotSupportedException();
            Func<ITestFixture> fixtureFactory = () =>
            {
                throw exception;
            };
            var sut = new DerivedTheoremAttribute(fixtureFactory);
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithAutoData"));

            var actual = sut.CreateTestCommands(method).ToArray();

            Assert.Equal(2, actual.Length);
            Array.ForEach(actual, c =>
            {
                var command = Assert.IsAssignableFrom<ExceptionCommand>(c);
                Assert.Equal(method.MethodInfo.Name, command.MethodName);
                Assert.Equal(exception, command.Exception);
            });
        }

        [Fact]
        public void FixtureTypeInitializedWithFixtureTypeIsCorrect()
        {
            var fixtureType = typeof(FakeTestFixture);
            var sut = new NaiveTheoremAttribute(fixtureType);

            var actual = sut.FixtureType;

            Assert.Equal(fixtureType, actual);
        }

        [Fact]
        public void FixtureTypeInitializedWithFuncOfITestFixtureIsCorrect()
        {
            var sut = new DerivedTheoremAttribute(() => new FakeTestFixture());
            var actual = sut.FixtureType;
            Assert.Equal(typeof(FakeTestFixture), actual);
        }

        [Fact]
        public void InitializeWithNullFuncOfMethodInfoAndITestFixtureThrows()
        {
            Func<MethodInfo, ITestFixture> fixtureFactory = null;
            Assert.Throws<ArgumentNullException>(() => new DerivedTheoremAttribute(fixtureFactory));
        }

        [Fact]
        public void FixtureTypeIsCorrect()
        {
            var sut = new NaiveTheoremAttribute();
            var actual = sut.FixtureType;
            Assert.Equal(typeof(NotSupportedFixture), actual);
        }

        [Fact]
        public void FixtureFactoryInitializedWithFuncOfMethodInfoAndITestFixtureIsCorrect()
        {
            Func<MethodInfo, ITestFixture> expected = mi => new FakeTestFixture();
            var sut = new DerivedTheoremAttribute(expected);

            var actual = sut.FixtureFactory;

            Assert.Equal(expected, actual);
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
            var sut = new DerivedTheoremAttribute(fixtureFactory);

            var actual = sut.FixtureType;

            Assert.Equal(typeof(FakeTestFixture), actual);
        }

        [Fact]
        public void CreateTestCommandsPassesCorrectMethodInfoToFixtureFactory()
        {
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithMixedData"));
            bool verified = false;
            Func<MethodInfo, ITestFixture> fixtureFactory = mi =>
            {
                Assert.Equal(method.MethodInfo, mi);
                verified = true;
                return new FakeTestFixture();
            };
            var sut = new DerivedTheoremAttribute(fixtureFactory);

            sut.CreateTestCommands(method).Single();

            Assert.True(verified, "verified");
        }
        
        [InlineData]
        [InlineData]
        public void ParameterizedWithAutoData(string arg1, int arg2)
        {
        }

        [InlineData("expected")]
        public void ParameterizedWithMixedData(string arg1, int arg2)
        {
        }

        [InlineData("expected", 1234)]
        public void ParameterizedWithInvalidCountData(string arg)
        {
        }

        [InlineData("expected")]
        public void ParameterizedWithInvalidTypeData(int arg)
        {
        }

        public void ParameterizedWithAutoDataNotUsingDataAttribute(string arg1, int arg2)
        {
        }

        [ParameterTypeData]
        public void ParameterizedForParameterTypes(string arg1, int arg2)
        {
        }

        private class DerivedTheoremAttribute : NaiveTheoremAttribute
        {
            public DerivedTheoremAttribute(Func<ITestFixture> fixtureFactory) : base(fixtureFactory)
            {
            }

            public DerivedTheoremAttribute(Func<MethodInfo, ITestFixture> fixtureFactory) : base(fixtureFactory)
            {
            }
        }

        private class ParameterTypeDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
            {
                Assert.Equal(new[] { typeof(string), typeof(int) }, parameterTypes);
                yield return new object[] { "dummy", 1 };
            }
        }
    }
}
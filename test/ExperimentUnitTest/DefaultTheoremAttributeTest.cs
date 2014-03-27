using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Extensions;
using Xunit.Sdk;

namespace Jwc.Experiment
{
    public class DefaultTheoremAttributeTest
    {
        [Fact]
        public void SutIsFactAttribute()
        {
            var sut = new DefaultTheoremAttribute();
            Assert.IsAssignableFrom<FactAttribute>(sut);
        }

        [Fact]
        public void CreateNonParameterizedTestReturnsFactCommand()
        {
            var sut = new DefaultTheoremAttribute();
            
            var actual = sut.CreateTestCommands(Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()));

            var factCommand = Assert.IsType<FactCommand>(actual.Single());
            Console.WriteLine(factCommand.MethodName);
        }

        [Theory]
        [InlineData("dummy", 1, null)]
        [InlineData("dummy", 1, null)]
        [InlineData("dummy", 1, null)]
        public void CreateParameterizedTestReturnsThoeryCommands(string arg1, int arg2, object arg3)
        {
            var sut = new DefaultTheoremAttribute();

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
        public void FixtureFactoryIsCorrectWhenInitializedWithDefaultCtor()
        {
            var sut = new DefaultTheoremAttribute();
            var actual = sut.FixtureFactory;
            Assert.IsType<NotSupportedFixture>(actual.Invoke(null));
        }

        [Fact]
        public void FixtureFactoryIsCorrectWhenInitializedWithFuncOfITestFixture()
        {
            var sut = new DerivedTheoremAttribute(() => new FakeTestFixture());
            var actual = sut.FixtureFactory;
            Assert.IsType<FakeTestFixture>(actual.Invoke(null));
        }

        [Fact]
        public void CreateParameterizedTestWithAutoDataReturnsCorrectCommands()
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
        public void CreateParameterizedTestWithMixedDataReturnsCorrectCommands()
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
        public void CreateParameterizedTestWithInvalidCountDataThrows()
        {
            var sut = new DefaultTheoremAttribute();
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithInvalidCountData"));

            var actual = sut.CreateTestCommands(method);

            var theoryCommand = Assert.IsType<TheoryCommand>(actual.Single());
            Assert.Throws<InvalidOperationException>(() => theoryCommand.Execute(this));
        }

        [Fact]
        public void CreateParameterizedTestWithInvalidTypeDataThrows()
        {
            var sut = new DefaultTheoremAttribute();
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithInvalidTypeData"));

            var actual = sut.CreateTestCommands(method);

            var theoryCommand = Assert.IsType<TheoryCommand>(actual.Single());
            Assert.Throws<ArgumentException>(() => theoryCommand.Execute(this));
        }

        [Fact]
        public void FixtureFactoryIsCorrectWhenInitializedWithType()
        {
            var sut = new DefaultTheoremAttribute(typeof(FakeTestFixture));

            var actual = sut.FixtureFactory;

            Assert.IsType<FakeTestFixture>(actual(null));
            Assert.NotSame(actual(null), actual(null));
        }

        [Fact]
        public void InitializeWithNullFactoryTypeThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new DefaultTheoremAttribute(null));
        }

        [Fact]
        public void CreateParameterizedTestWithAutoDataNotUsingDataAttributeReturnsCorrectCommand()
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
        public void CreateParameterizedTestPassesCorrectParameterTypes()
        {
            var sut = new DefaultTheoremAttribute();
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedForParameterTypes"));
            Assert.DoesNotThrow(() => sut.CreateTestCommands(method).Single());
        }

        [Theory]
        [InlineData("dummy", 1, null)]
        public void CreateParameterizedTestDoesNotInitializeFixture(string arg1, int arg2, object arg3)
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
        public void CreateParameterizedTestWithAutoDataInitializesFixtureForEachTestCase()
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
        public void CreateParameterizedTestWithMixedDataInitializesFixtureForEachTestCase()
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
        public void CreateParameterizedTestWithAutoDataNotUsingDataAttributeInitializesFixtureForEachTestCase()
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
        public void CreateNonParameterizedTestDoesNotInitializeFixture()
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
        public void CreateParameterizedTestForSingleReturnsExceptionCommandWhenThrowingException()
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
        public void CreateParameterizedTestForManyReturnsExceptionCommandWhenThrowingException()
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
        public void FixtureTypeIsCorrectWhenInitializedWithFixtureType()
        {
            var fixtureType = typeof(FakeTestFixture);
            var sut = new DefaultTheoremAttribute(fixtureType);

            var actual = sut.FixtureType;

            Assert.Equal(fixtureType, actual);
        }

        [Fact]
        public void FixtureTypeIsCorrectWhenInitializedWithFuncOfITestFixture()
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
        public void FixtureTypeIsCorrectWhenInitializedWithDefaultCtor()
        {
            var sut = new DefaultTheoremAttribute();
            var actual = sut.FixtureType;
            Assert.Equal(typeof(NotSupportedFixture), actual);
        }

        [Fact]
        public void FixtureFactoryIsCorrectWhenInitializedWithFuncOfMethodInfoAndITestFixture()
        {
            Func<MethodInfo, ITestFixture> expected = mi => new FakeTestFixture();
            var sut = new DerivedTheoremAttribute(expected);

            var actual = sut.FixtureFactory;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FixtureTypeIsCorrectWhenInitializedWithFuncOfMethodInfoAndITestFixture()
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

        private class DerivedTheoremAttribute : DefaultTheoremAttribute
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
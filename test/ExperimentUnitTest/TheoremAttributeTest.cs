using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Extensions;
using Xunit.Sdk;

namespace Jwc.Experiment
{
    public class TheoremAttributeTest
    {
        [Fact]
        public void SutIsFactAttribute()
        {
            var sut = new TheoremAttribute();
            Assert.IsAssignableFrom<FactAttribute>(sut);
        }

        [Fact]
        public void CreateNonParameterizedReturnsFactCommand()
        {
            var sut = new TheoremAttribute();
            
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
            var sut = new TheoremAttribute();

            var actual = sut.CreateTestCommands(Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod())).ToArray();

            Assert.Equal(3, actual.Length);
            Array.ForEach(actual, c =>
            {
                var theoryCommand = Assert.IsType<TheoryCommand>(c);
                Assert.Equal(new[] { arg1, arg2, arg3 }, theoryCommand.Parameters);
            });
        }

        [Fact]
        public void InitializeWithNullFixtureFactoryThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new AutoDataTheoremAttribute(null));
        }

        [Fact]
        public void FixtureFactoryInitializedFromDefaultIsCorrect()
        {
            var sut = new TheoremAttribute();
            var actual = sut.FixtureFactory;
            Assert.IsType<NotSupportedFixture>(actual.Invoke());
        }

        [Fact]
        public void FixtureFactoryInitializedWithFuncIsCorrect()
        {
            Func<ITestFixture> expected = () => null;
            var sut = new AutoDataTheoremAttribute(expected);

            var actual = sut.FixtureFactory;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateParameterizedWithAutoDataReturnsCorrectCommands()
        {
            var fixture = new FakeTestFixture();
            var sut = new AutoDataTheoremAttribute(() => fixture);
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
            var sut = new AutoDataTheoremAttribute(() => fixture);
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
            var sut = new TheoremAttribute();
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithInvalidCountData"));

            var actual = sut.CreateTestCommands(method);

            var theoryCommand = Assert.IsType<TheoryCommand>(actual.Single());
            Assert.Throws<InvalidOperationException>(() => theoryCommand.Execute(this));
        }

        [Fact]
        public void CreateParameterizedWithInvalidTypeDataThrows()
        {
            var sut = new TheoremAttribute();
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithInvalidTypeData"));

            var actual = sut.CreateTestCommands(method);

            var theoryCommand = Assert.IsType<TheoryCommand>(actual.Single());
            Assert.Throws<ArgumentException>(() => theoryCommand.Execute(this));
        }

        [Fact]
        public void FixtureFactoryInitializedWithTypeIsCorrect()
        {
            var sut = new TheoremAttribute(typeof(FakeTestFixture));

            var actual = sut.FixtureFactory;

            Assert.IsType<FakeTestFixture>(actual());
            Assert.NotSame(actual(), actual());
        }

        [Fact]
        public void InitializeWithNullFactoryTypeThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new TheoremAttribute(null));
        }

        [Fact]
        public void CreateParameterizedWithAutoDataNotUsingDataAttributeReturnsCorrectCommand()
        {
            var fixture = new FakeTestFixture();
            var sut = new AutoDataTheoremAttribute(() => fixture);
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
            var sut = new TheoremAttribute();
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
            var sut = new AutoDataTheoremAttribute(fixtureFactory);
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
            var sut = new AutoDataTheoremAttribute(fixtureFactory);
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
            var sut = new AutoDataTheoremAttribute(fixtureFactory);
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
            var sut = new AutoDataTheoremAttribute(fixtureFactory);
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
            var sut = new AutoDataTheoremAttribute(fixtureFactory);
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
            var sut = new AutoDataTheoremAttribute(fixtureFactory);
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
            var sut = new AutoDataTheoremAttribute(fixtureFactory);
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

        private class AutoDataTheoremAttribute : TheoremAttribute
        {
            public AutoDataTheoremAttribute(Func<ITestFixture> fixtureFactory) : base(fixtureFactory)
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
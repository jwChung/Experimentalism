using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Extensions;
using Xunit.Sdk;

namespace Jwc.Experiment
{
    public class BaseTheoremAttributeTest
    {
        [Fact]
        public void SutIsFactAttribute()
        {
            var sut = new BaseTheoremAttribute();
            Assert.IsAssignableFrom<FactAttribute>(sut);
        }

        [Fact]
        public void CreateNonParameterizedTestReturnsCorrectFactCommand()
        {
            var sut = new BaseTheoremAttribute();
            IMethodInfo method = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());

            var actual = sut.CreateTestCommands(method);

            var factCommand = Assert.IsType<FactCommand>(actual.Single());
            Assert.Equal(method.Name, factCommand.MethodName);
        }

        [Theory]
        [InlineData("dummy", 1, null)]
        [InlineData("dummy", 1, null)]
        [InlineData("dummy", 1, null)]
        public void CreateParameterizedTestReturnsThoeryCommands(string arg1, int arg2, object arg3)
        {
            var sut = new BaseTheoremAttribute();

            var actual = sut.CreateTestCommands(Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod())).ToArray();

            Assert.Equal(3, actual.Length);
            Array.ForEach(actual, c =>
            {
                var theoryCommand = Assert.IsType<TheoryCommand>(c);
                Assert.Equal(new[] { arg1, arg2, arg3 }, theoryCommand.Parameters);
            });
        }

        [Fact]
        public void CreateParameterizedTestWithAutoDataReturnsCorrectCommands()
        {
            var fixture = new FakeTestFixture();
            var sut = new DerivedTheoremAttribute(new DelegatingFixtureFactory { OnCreate = mi => fixture });
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
            var sut = new DerivedTheoremAttribute(new DelegatingFixtureFactory { OnCreate = mi => fixture });
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
            var sut = new BaseTheoremAttribute();
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithInvalidCountData"));

            var actual = sut.CreateTestCommands(method);

            var theoryCommand = Assert.IsType<TheoryCommand>(actual.Single());
            Assert.Throws<InvalidOperationException>(() => theoryCommand.Execute(this));
        }

        [Fact]
        public void CreateParameterizedTestWithInvalidTypeDataThrows()
        {
            var sut = new BaseTheoremAttribute();
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithInvalidTypeData"));

            var actual = sut.CreateTestCommands(method);

            var theoryCommand = Assert.IsType<TheoryCommand>(actual.Single());
            Assert.Throws<ArgumentException>(() => theoryCommand.Execute(this));
        }

        [Fact]
        public void InitializeWithNullFactoryTypeThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new BaseTheoremAttribute(null));
        }

        [Fact]
        public void CreateParameterizedTestWithAutoDataNotUsingDataAttributeReturnsCorrectCommand()
        {
            var fixture = new FakeTestFixture();
            var sut = new DerivedTheoremAttribute(new DelegatingFixtureFactory { OnCreate = mi => fixture });
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
            var sut = new BaseTheoremAttribute();
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedForParameterTypes"));
            Assert.DoesNotThrow(() => sut.CreateTestCommands(method).Single());
        }

        [Theory]
        [InlineData("dummy", 1, null)]
        public void CreateParameterizedTestWithNoAutoDataDoesNotInitializeFixture(
            string arg1, int arg2, object arg3)
        {
            var sut = new DerivedTheoremAttribute(
                new DelegatingFixtureFactory
                {
                    OnCreate = mi => { throw new NotSupportedException(); }
                });
            Assert.DoesNotThrow(() => sut.CreateTestCommands(
                Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod())).ToArray());
        }

        [Fact]
        public void CreateParameterizedTestWithAutoDataInitializesFixtureForEachTestCase()
        {
            var fixture = new FakeTestFixture();
            int callCount = 0;
            var sut = new DerivedTheoremAttribute(
                new DelegatingFixtureFactory
                {
                    OnCreate = mi =>
                    {
                        callCount++;
                        return fixture;
                    }
                });
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithAutoData"));

            sut.CreateTestCommands(method).ToArray();

            Assert.Equal(2, callCount);
        }

        [Fact]
        public void CreateParameterizedTestWithMixedDataInitializesFixtureForEachTestCase()
        {
            var fixture = new FakeTestFixture();
            int callCount = 0;
            var sut = new DerivedTheoremAttribute(
                new DelegatingFixtureFactory
                {
                    OnCreate = mi =>
                    {
                        callCount++;
                        return fixture;
                    }
                });
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithMixedData"));

            sut.CreateTestCommands(method).ToArray();

            Assert.Equal(1, callCount);
        }

        [Fact]
        public void CreateParameterizedTestWithAutoDataNotUsingDataAttributeInitializesFixtureForEachTestCase()
        {
            var fixture = new FakeTestFixture();
            int callCount = 0;
            var sut = new DerivedTheoremAttribute(
                new DelegatingFixtureFactory
                {
                    OnCreate = mi =>
                    {
                        callCount++;
                        return fixture;
                    }
                });
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithAutoDataNotUsingDataAttribute"));

            sut.CreateTestCommands(method).ToArray();

            Assert.Equal(1, callCount);
        }

        [Fact]
        public void CreateNonParameterizedTestDoesNotInitializeFixture()
        {
            var sut = new DerivedTheoremAttribute(
                new DelegatingFixtureFactory
                {
                    OnCreate = mi =>
                    {
                        throw new NotSupportedException();
                    }
                });
            Assert.DoesNotThrow(() => sut.CreateTestCommands(
                Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod())).ToArray());
        }

        [Fact]
        public void CreateParameterizedTestForSingleReturnsExceptionCommandWhenThrowingException()
        {
            var exception = new NotSupportedException();
            var sut = new DerivedTheoremAttribute(
                new DelegatingFixtureFactory
                {
                    OnCreate = mi => { throw exception; }
                });
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
            var sut = new DerivedTheoremAttribute(
                new DelegatingFixtureFactory
                {
                    OnCreate = mi => { throw exception; }
                });
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
        public void InitializeWithNullFixtureFactoryThrows()
        {
            ITestFixtureFactory fixtureFactory = null;
            Assert.Throws<ArgumentNullException>(() => new DerivedTheoremAttribute(fixtureFactory));
        }

        [Fact]
        public void CreateTestCommandsPassesCorrectMethodInfoToFixtureFactory()
        {
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithMixedData"));
            bool verified = false;
            var fixtureFactory = new DelegatingFixtureFactory
            {
                OnCreate = mi =>
                {
                    Assert.Equal(method.MethodInfo, mi);
                    verified = true;
                    return new FakeTestFixture();
                }
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

        private class DerivedTheoremAttribute : BaseTheoremAttribute
        {
            public DerivedTheoremAttribute(ITestFixtureFactory fixtureFactory)
                : base(fixtureFactory)
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
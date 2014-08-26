﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jwc.Experiment.Xunit;
using Moq;
using Xunit;
using Xunit.Extensions;
using Xunit.Sdk;

namespace Jwc.Experiment.Xunit
{
    public class TestAttributeTest : IDisposable
    {
        [Fact]
        public void SutIsFactAttribute()
        {
            var sut = new TssTestAttribute();
            Assert.IsAssignableFrom<FactAttribute>(sut);
        }

        [Fact]
        public void CreateNonParameterizedTestReturnsCorrectFactCommand()
        {
            var sut = new TssTestAttribute();
            IMethodInfo method = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());

            var actual = sut.CreateTestCommands(method);

            var factCommand = Assert.IsType<FactCommand>(actual.Single());
            Assert.Equal(method.Name, factCommand.MethodName);
        }

        [Theory]
        [InlineData("dummy", 1, null)]
        [InlineData("dummy", 1, null)]
        [InlineData("dummy", 1, null)]
        public void CreateParameterizedTestReturnsTheoryCommands(string arg1, int arg2, object arg3)
        {
            var sut = new TssTestAttribute();

            var actual = sut.CreateTestCommands(Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod())).ToArray();

            Assert.Equal(3, actual.Length);
            Array.ForEach(
                actual,
                c =>
                {
                    var theoryCommand = Assert.IsType<TheoryCommand>(c);
                    Assert.Equal(new[] { arg1, arg2, arg3 }, theoryCommand.Parameters);
                });
        }

        [Fact]
        public void CreateParameterizedTestWithAutoDataReturnsCorrectCommands()
        {
            var fixture = new FakeTestFixture();
            var sut = new TssTestAttribute { OnCreateTestFixture = mi => fixture };
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithAutoData"));

            var actual = sut.CreateTestCommands(method).ToArray();

            Assert.Equal(2, actual.Length);
            Array.ForEach(
                actual,
                c =>
                {
                    var theoryCommand = Assert.IsType<TheoryCommand>(c);
                    Assert.Equal(
                        new[] { fixture.Create(typeof(string)), fixture.Create(typeof(int)) },
                        theoryCommand.Parameters);
                });
        }

        [Fact]
        public void CreateParameterizedTestWithMixedDataReturnsCorrectCommands()
        {
            var fixture = new FakeTestFixture();
            var sut = new TssTestAttribute { OnCreateTestFixture = mi => fixture };
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithMixedData"));

            var actual = sut.CreateTestCommands(method);

            var theoryCommand = Assert.IsType<TheoryCommand>(actual.Single());
            Assert.Equal(
                new[] { "expected", fixture.Create(typeof(int)) },
                theoryCommand.Parameters);
        }

        [Fact]
        public void CreateParameterizedTestWithInvalidCountDataThrows()
        {
            var sut = new TssTestAttribute();
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithInvalidCountData"));

            var actual = sut.CreateTestCommands(method);

            var theoryCommand = Assert.IsType<TheoryCommand>(actual.Single());
            Assert.Throws<InvalidOperationException>(() => theoryCommand.Execute(this));
        }

        [Fact]
        public void CreateParameterizedTestWithInvalidTypeDataThrows()
        {
            var sut = new TssTestAttribute();
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithInvalidTypeData"));

            var actual = sut.CreateTestCommands(method);

            var theoryCommand = Assert.IsType<TheoryCommand>(actual.Single());
            Assert.Throws<ArgumentException>(() => theoryCommand.Execute(this));
        }

        [Fact]
        public void CreateParameterizedTestWithAutoDataNotUsingDataAttributeReturnsCorrectCommand()
        {
            var fixture = new FakeTestFixture();
            var sut = new TssTestAttribute { OnCreateTestFixture = mi => fixture };
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithAutoDataNotUsingDataAttribute"));

            var actual = sut.CreateTestCommands(method);

            var theoryCommand = Assert.IsType<TheoryCommand>(actual.Single());
            Assert.Equal(
                new[] { fixture.Create(typeof(string)), fixture.Create(typeof(int)) },
                theoryCommand.Parameters);
        }

        [Fact]
        public void CreateParameterizedTestPassesCorrectParameterTypes()
        {
            var sut = new TssTestAttribute();
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedForParameterTypes"));
            Assert.DoesNotThrow(() => sut.CreateTestCommands(method).Single());
        }

        [Theory]
        [InlineData("dummy", 1, null)]
        public void CreateParameterizedTestWithNoAutoDataDoesNotInitializeFixture(
            string arg1, int arg2, object arg3)
        {
            var sut = new TssTestAttribute();
            var actual = sut.CreateTestCommands(
                Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod())).Single();
            Assert.IsNotType<ExceptionCommand>(actual);
        }

        [Fact]
        public void CreateParameterizedTestWithAutoDataInitializesFixtureForEachTestCase()
        {
            var fixture = new FakeTestFixture();
            int callCount = 0;
            var sut = new TssTestAttribute
            {
                OnCreateTestFixture = mi =>
                {
                    callCount++;
                    return fixture;
                }
            };
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithAutoData"));

            sut.CreateTestCommands(method).ToArray();

            Assert.Equal(2, callCount);
        }

        [Fact]
        public void CreateParameterizedTestWithMixedDataInitializesFixtureForEachTestCase()
        {
            var fixture = new FakeTestFixture();
            int callCount = 0;
            var sut = new TssTestAttribute
            {
                OnCreateTestFixture = mi =>
                {
                    callCount++;
                    return fixture;
                }
            };
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithMixedData"));

            sut.CreateTestCommands(method).ToArray();

            Assert.Equal(1, callCount);
        }

        [Fact]
        public void CreateParameterizedTestWithAutoDataNotUsingDataAttributeInitializesFixtureForEachTestCase()
        {
            var fixture = new FakeTestFixture();
            int callCount = 0;
            var sut = new TssTestAttribute
            {
                OnCreateTestFixture = mi =>
                {
                    callCount++;
                    return fixture;
                }
            };
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithAutoDataNotUsingDataAttribute"));

            sut.CreateTestCommands(method).ToArray();

            Assert.Equal(1, callCount);
        }

        [Fact]
        public void CreateNonParameterizedTestDoesNotInitializeFixture()
        {
            var sut = new TssTestAttribute
            {
                OnCreateTestFixture = mi => { throw new NotSupportedException(); }  
            };
            Assert.DoesNotThrow(() => sut.CreateTestCommands(
                Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod())).ToArray());
        }

        [Fact]
        public void CreateParameterizedTestForSingleReturnsExceptionCommandWhenThrowingException()
        {
            var exception = new NotSupportedException();
            var sut = new TssTestAttribute
            {
                OnCreateTestFixture = mi => { throw exception; }
            };
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
            var sut = new TssTestAttribute
            {
                OnCreateTestFixture = mi => { throw exception; }
            };
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithAutoData"));

            var actual = sut.CreateTestCommands(method).ToArray();

            Assert.Equal(2, actual.Length);
            Array.ForEach(
                actual,
                c =>
                {
                    var command = Assert.IsAssignableFrom<ExceptionCommand>(c);
                    Assert.Equal(method.MethodInfo.Name, command.MethodName);
                    Assert.Equal(exception, command.Exception);
                });
        }

        [Fact]
        public void CreateTestCommandsPassesCorrectMethodInfoToFixtureFactory()
        {
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithMixedData"));
            bool verified = false;
            var sut = new TssTestAttribute
            {
                OnCreateTestFixture = mi =>
                {
                    Assert.Equal(method.MethodInfo, mi);
                    verified = true;
                    return new FakeTestFixture();
                }
            };

            sut.CreateTestCommands(method).Single();

            Assert.True(verified, "verified");
        }

        [Fact]
        public void CreateParameterizedTestWithExceptionDataReturnsCorrectCommands()
        {
            var sut = new TssTestAttribute();
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithExceptionData"));

            var actual = sut.CreateTestCommands(method).ToArray();

            Assert.Equal(2, actual.Length);
            var command = Assert.IsAssignableFrom<ExceptionCommand>(actual[1]);
            Assert.IsType<NotSupportedException>(command.Exception);
        }

        [Fact]
        public void CreateParameterizedTestWithoutValidTestFixtureFactoryReturnsExceptionCommand()
        {
            var sut = new TestAttribute();
            IMethodInfo method = Reflector.Wrap(typeof(object).GetMethod("Equals", new[] { typeof(object) }));

            var actual = sut.CreateTestCommands(method).Single();

            var command = Assert.IsAssignableFrom<ExceptionCommand>(actual);
            Assert.IsType<NotSupportedException>(command.Exception);
        }

        [Fact]
        public void CreateTestCommandsUsesCorrectTestFixtureFactory()
        {
            // Fixture setup
            var sut = new TestAttribute();
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithAutoData"));
            int callCount = 0;
            var factory = new DelegatingTestFixtureFactory
            {
                OnCreate = m =>
                {
                    callCount++;
                    Assert.Equal(method.MethodInfo, m);
                    return new FakeTestFixture();
                }
            };
            DefaultFixtureFactory.SetCurrent(factory);

            // Exercise system 
            var actual = sut.CreateTestCommands(method).ToArray();

            // Verify outcome
            Assert.Equal(2, callCount);
            Assert.True(actual.All(c => c is TheoryCommand));
        }

        [Fact]
        public void CreateParameterizedTestCommandWithAutoDataNotUsingDataAttributePassesCorrectTestMethodToTheCommand()
        {
            var sut = new TssTestAttribute
            {
                OnCreateTestFixture = mi => new FakeTestFixture()
            };
            var method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithAutoDataNotUsingDataAttribute"));
            var actual = sut.CreateTestCommands(method).Single();

            var theoryCommand = Assert.IsType<TheoryCommand>(actual);
            Assert.Equal(method.MethodInfo.Name, theoryCommand.MethodName);
        }

        [Fact]
        public void CreateParameterizedTestCommandWithAutoDataUsingDataAttributePassesCorrectTestMethodToTheCommand()
        {
            var sut = new TssTestAttribute
            {
                OnCreateTestFixture = mi => new FakeTestFixture()
            };
            var method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithAutoData"));
            var actual = sut.CreateTestCommands(method).First();

            var theoryCommand = Assert.IsType<TheoryCommand>(actual);
            Assert.Equal(method.MethodInfo.Name, theoryCommand.MethodName);
        }

        [Fact]
        public void CreateTestCommandsCorrectlyConfiguresAllFixturesInTestAssembly()
        {
            var sut = new TestAttribute();
            IMethodInfo method = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());

            sut.CreateTestCommands(method).ToArray();

            Assert.Equal(1, SpyTestAssemblyConfigurationAttribute.SetUpCount);
        }

        [Fact]
        public void CreateTestCommandsReturnsExceptionCommandWhenTestAssemblyConfigurationThrows()
        {
            var sut = new TestAttribute();
            var assembly = new DelegatingAssembly
            {
                OnGetCustomAttributesWithType =
                    (t, i) => new object[] { new TestAssemblyExceptionConfigurationAttribute() }
            };
            var method = Mock.Of<IMethodInfo>(
                x => x.MethodInfo == Mock.Of<MethodInfo>(
                    m => m.ReflectedType == Mock.Of<Type>(
                        t => t.Assembly == assembly)));

            var actual = sut.CreateTestCommands(method).Single();

            var command = Assert.IsType<ExceptionCommand>(actual);
            Assert.IsType<InvalidOperationException>(command.Exception);
        }

        public void Dispose()
        {
            SpyTestAssemblyConfigurationAttribute.SetUpCount = 0;
            DefaultFixtureFactory.SetCurrent(null);
            typeof(TestAssemblyConfigurationAttribute)
                .GetField("configured", BindingFlags.NonPublic | BindingFlags.Static)
                .SetValue(null, false);
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

        [ExceptionData]
        public void ParameterizedWithExceptionData(string arg1, int arg2)
        {
        }

        private class TssTestAttribute : TestAttribute
        {
            public Func<MethodInfo, ITestFixture> OnCreateTestFixture { get; set; }

            protected override ITestFixture CreateTestFixture(MethodInfo testMethod)
            {
                return this.OnCreateTestFixture(testMethod);
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

        private class ExceptionDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo methodUnderTest, Type[] parameterTypes)
            {
                yield return new object[] { "dummy", 1 };
                throw new NotSupportedException();
            }
        }
    }
}
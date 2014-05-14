using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using Xunit;
using Xunit.Extensions;
using Xunit.Sdk;

namespace Jwc.Experiment
{
    public class TestAttributeTest
    {
        [Fact]
        public void SutIsFactAttribute()
        {
            var sut = new TestSpecificTheoremAttribute();
            Assert.IsAssignableFrom<FactAttribute>(sut);
        }

        [Fact]
        public void CreateNonParameterizedTestReturnsCorrectFactCommand()
        {
            var sut = new TestSpecificTheoremAttribute();
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
            var sut = new TestSpecificTheoremAttribute();

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
            var sut = new TestSpecificTheoremAttribute { OnCreateTestFixture = mi => fixture };
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithAutoData"));

            var actual = sut.CreateTestCommands(method).ToArray();

            Assert.Equal(2, actual.Length);
            Array.ForEach(actual, c =>
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
            var sut = new TestSpecificTheoremAttribute { OnCreateTestFixture = mi => fixture };
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
            var sut = new TestSpecificTheoremAttribute();
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithInvalidCountData"));

            var actual = sut.CreateTestCommands(method);

            var theoryCommand = Assert.IsType<TheoryCommand>(actual.Single());
            Assert.Throws<InvalidOperationException>(() => theoryCommand.Execute(this));
        }

        [Fact]
        public void CreateParameterizedTestWithInvalidTypeDataThrows()
        {
            var sut = new TestSpecificTheoremAttribute();
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithInvalidTypeData"));

            var actual = sut.CreateTestCommands(method);

            var theoryCommand = Assert.IsType<TheoryCommand>(actual.Single());
            Assert.Throws<ArgumentException>(() => theoryCommand.Execute(this));
        }

        [Fact]
        public void CreateParameterizedTestWithAutoDataNotUsingDataAttributeReturnsCorrectCommand()
        {
            var fixture = new FakeTestFixture();
            var sut = new TestSpecificTheoremAttribute { OnCreateTestFixture = mi => fixture };
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
            var sut = new TestSpecificTheoremAttribute();
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedForParameterTypes"));
            Assert.DoesNotThrow(() => sut.CreateTestCommands(method).Single());
        }

        [Theory]
        [InlineData("dummy", 1, null)]
        public void CreateParameterizedTestWithNoAutoDataDoesNotInitializeFixture(
            string arg1, int arg2, object arg3)
        {
            var sut = new TestSpecificTheoremAttribute();
            var actual = sut.CreateTestCommands(
                Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod())).Single();
            Assert.IsNotType<ExceptionCommand>(actual);
        }

        [Fact]
        public void CreateParameterizedTestWithAutoDataInitializesFixtureForEachTestCase()
        {
            var fixture = new FakeTestFixture();
            int callCount = 0;
            var sut = new TestSpecificTheoremAttribute
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
            var sut = new TestSpecificTheoremAttribute
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
            var sut = new TestSpecificTheoremAttribute
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
            var sut = new TestSpecificTheoremAttribute
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
            var sut = new TestSpecificTheoremAttribute
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
            var sut = new TestSpecificTheoremAttribute
            {
                OnCreateTestFixture = mi => { throw exception; }
            };
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
        public void CreateTestCommandsPassesCorrectMethodInfoToFixtureFactory()
        {
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithMixedData"));
            bool verified = false;
            var sut = new TestSpecificTheoremAttribute
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
            var sut = new TestSpecificTheoremAttribute();
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithExceptionData"));

            var actual = sut.CreateTestCommands(method).ToArray();

            Assert.Equal(2, actual.Length);
            var command = Assert.IsAssignableFrom<ExceptionCommand>(actual[1]);
            Assert.IsType<NotSupportedException>(command.Exception);
        }

        [Theory]
        [InlineData("CreateParameterizedTestWithoutTestFixtureFactoryAttributeReturnsExceptionCommand")]
        [InlineData("CreateParameterizedTestWithTestFixtureFactoryAttributeReturnsCorrectCommand")]
        [InlineData("CreateParameterizedTestSeveralTimesCreatesTestFixtureFactoryOnlyOnce")]
        [InlineData("CreateTestCommandsCreatesTestFixtureFactoryAsSingletonWhenAccessedByMultiThreads")]
        public void RunTestWithStaticFixture(string testMethod)
        {
            GetType().GetMethod(testMethod).Execute();
        }

        public void CreateParameterizedTestWithoutTestFixtureFactoryAttributeReturnsExceptionCommand()
        {
            var sut = new TestAttribute();
            IMethodInfo method = Reflector.Wrap(typeof(object).GetMethod("Equals", new[] { typeof(object) }));

            var actual = sut.CreateTestCommands(method).Single();

            var command = Assert.IsAssignableFrom<ExceptionCommand>(actual);
            Assert.IsType<NotSupportedException>(command.Exception);
        }

        public void CreateParameterizedTestWithTestFixtureFactoryAttributeReturnsCorrectCommand()
        {
            // Fixture setup
            var sut = new TestAttribute();
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithAutoData"));
            var fixture = new FakeTestFixture();
            DelegatingStaticTestFixtureFactory.OnCreate = mi =>
            {
                Assert.Equal(method.MethodInfo, mi);
                return fixture;
            };

            // Exercise system
            var actual = sut.CreateTestCommands(method).ToArray();

            // Verify outcome
            Assert.Equal(2, actual.Length);
            Array.ForEach(actual, c =>
            {
                var theoryCommand = Assert.IsType<TheoryCommand>(c);
                Assert.Equal(
                    new[] { fixture.Create(typeof(string)), fixture.Create(typeof(int)) },
                    theoryCommand.Parameters);
            });
        }

        public void CreateParameterizedTestSeveralTimesCreatesTestFixtureFactoryOnlyOnce()
        {
            var sut = new TestAttribute();
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithAutoData"));

            sut.CreateTestCommands(method).ToArray();

            Assert.Equal(1, DelegatingStaticTestFixtureFactory.ConstructCount);
        }

        public void CreateTestCommandsCreatesTestFixtureFactoryAsSingletonWhenAccessedByMultiThreads()
        {
            // Fixture setup
            var sut = new TestAttribute();
            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithAutoData"));

            var threads = new Thread[20];
            for (int i = 0; i < threads.Length; i++)
                threads[i] = new Thread(() => sut.CreateTestCommands(method).ToArray());

            // Exercise system
            foreach (var thread in threads)
                thread.Start();

            foreach (var thread in threads)
                thread.Join();

            // Verify outcome
            Assert.Equal(1, DelegatingStaticTestFixtureFactory.ConstructCount);
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

        private class TestSpecificTheoremAttribute : TestAttribute
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
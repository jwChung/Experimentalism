using System;
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
            // Fixture setup
            var fixture = new FakeTestFixture
            {
                OnCreate = r =>
                {
                    var type = r as Type;
                    if (type != null)
                    {
                        if (type == typeof(string))
                        {
                            return "expected";
                        }
                        if (type == typeof(int))
                        {
                            return 1234;
                        }
                    }

                    throw new NotSupportedException();
                }
            };

            var sut = new AutoDataTheoremAttribute(() => fixture);

            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithAutoData"));

            // Excercise system
            var actual = sut.CreateTestCommands(method).ToArray();

            // Verify outcome
            Assert.Equal(2, actual.Length);
            Array.ForEach(actual, c =>
            {
                var theoryCommand = Assert.IsType<TheoryCommand>(c);
                Assert.Equal(new object[] { "expected", 1234 }, theoryCommand.Parameters);
            });
        }

        [Fact]
        public void CreateParameterizedWithMixedDataReturnsCorrectCommands()
        {
            // Fixture setup
            var fixture = new FakeTestFixture
            {
                OnCreate = r =>
                {
                    var type = r as Type;
                    if (type != null)
                    {
                        if (type == typeof(int))
                        {
                            return 1234;
                        }
                    }

                    throw new NotSupportedException();
                }
            };

            var sut = new AutoDataTheoremAttribute(() => fixture);

            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithMixedData"));

            // Excercise system
            var actual = sut.CreateTestCommands(method);

            // Verify outcome
            var theoryCommand = Assert.IsType<TheoryCommand>(actual.Single());
            Assert.Equal(new object[] { "expected", 1234 }, theoryCommand.Parameters);
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
            // Fixture setup
            var fixture = new FakeTestFixture
            {
                OnCreate = r =>
                {
                    var type = r as Type;
                    if (type != null)
                    {
                        if (type == typeof(string))
                        {
                            return "expected";
                        }
                        if (type == typeof(int))
                        {
                            return 1234;
                        }
                    }

                    throw new NotSupportedException();
                }
            };

            var sut = new AutoDataTheoremAttribute(() => fixture);

            IMethodInfo method = Reflector.Wrap(GetType().GetMethod("ParameterizedWithAutoDataNotUsingDataAttribute"));

            // Excercise system
            var actual = sut.CreateTestCommands(method);

            // Verify outcome
            var theoryCommand = Assert.IsType<TheoryCommand>(actual.Single());
            Assert.Equal(new object[] { "expected", 1234 }, theoryCommand.Parameters);
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

        private class AutoDataTheoremAttribute : TheoremAttribute
        {
            public AutoDataTheoremAttribute(Func<ITestFixture> fixtureFactory) : base(fixtureFactory)
            {
            }
        }
    }
}
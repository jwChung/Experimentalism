﻿using System;
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
        public void CreateNonParameterizedTestCommandReturnsFactCommand()
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
        public void CreateParameterizedTestCommandsReturnsThoeryCommands(string arg1, int arg2, object arg3)
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
        public void FixtureFactoryInitializedFromGreedyIsCorrect()
        {
            Func<ITestFixture> expected = () => null;
            var sut = new AutoDataTheoremAttribute(expected);

            var actual = sut.FixtureFactory;

            Assert.Equal(expected, actual);
        }

        private class AutoDataTheoremAttribute : TheoremAttribute
        {
            public AutoDataTheoremAttribute(Func<ITestFixture> fixtureFactory) : base(fixtureFactory)
            {
            }
        }
    }
}
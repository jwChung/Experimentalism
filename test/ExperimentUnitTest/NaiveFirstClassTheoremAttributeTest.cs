﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Sdk;

namespace Jwc.Experiment
{
    public class NaiveFirstClassTheoremAttributeTest
    {
        [Fact]
        public void SutIsFactAttribute()
        {
            var sut = new NaiveFirstClassTheoremAttribute();
            Assert.IsAssignableFrom<FactAttribute>(sut);
        }

        [Fact]
        public void CreateTestCommandsReturnsCorrectCommands()
        {
            var sut = new NaiveFirstClassTheoremAttribute();
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
            var sut = new NaiveFirstClassTheoremAttribute();
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
            var sut = new NaiveFirstClassTheoremAttribute();
            var actual = sut.FixtureFactory;
            Assert.IsType<NotSupportedFixture>(actual.Invoke(null));
        }

        [Fact]
        public void FixtureFactoryInitializedWithTypeIsCorrect()
        {
            var sut = new NaiveFirstClassTheoremAttribute(typeof(FakeTestFixture));

            var actual = sut.FixtureFactory;

            Assert.IsType<FakeTestFixture>(actual(null));
            Assert.NotSame(actual(null), actual(null));
        }

        [Fact]
        public void FixtureFactoryInitializedWithFuncOfITestFixtureIsCorrect()
        {
            var sut = new DerivedNaiveFirstClassTheoremAttribute(() => new FakeTestFixture());
            var actual = sut.FixtureFactory;
            Assert.IsType<FakeTestFixture>(actual.Invoke(null));
        }

        [Fact]
        public void FixtureFactoryInitializedWithFuncOfMethodInfoAndITestFixtureIsCorrect()
        {
            Func<MethodInfo, ITestFixture> expected = mi => new FakeTestFixture();
            var sut = new DerivedNaiveFirstClassTheoremAttribute(expected);

            var actual = sut.FixtureFactory;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateTestCommandsWithNullMethodInfoThrows()
        {
            var sut = new NaiveFirstClassTheoremAttribute();
            Assert.Throws<ArgumentNullException>(() => sut.CreateTestCommands(null));
        }

        [Fact]
        public void FixtureTypeIsCorrect()
        {
            var sut = new NaiveFirstClassTheoremAttribute();
            var actual = sut.FixtureType;
            Assert.Equal(typeof(NotSupportedFixture), actual);
        }

        [Fact]
        public void FixtureTypeInitializedWithFixtureTypeIsCorrect()
        {
            var fixtureType = typeof(FakeTestFixture);
            var sut = new NaiveFirstClassTheoremAttribute(fixtureType);

            var actual = sut.FixtureType;

            Assert.Equal(fixtureType, actual);
        }

        [Fact]
        public void FixtureTypeInitializedWithFuncOfITestFixtureIsCorrect()
        {
            var sut = new DerivedNaiveFirstClassTheoremAttribute(() => new FakeTestFixture());
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
            var sut = new DerivedNaiveFirstClassTheoremAttribute(fixtureFactory);

            var actual = sut.FixtureType;

            Assert.Equal(typeof(FakeTestFixture), actual);
        }

        [Fact]
        public void InitializeWithNullFixtureTypeThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new NaiveFirstClassTheoremAttribute(null));
        }

        [Fact]
        public void InitializeWithNullFuncOfITestFixtureThrows()
        {
            Func<ITestFixture> fixtureFactory = null;
            Assert.Throws<ArgumentNullException>(
                () => new DerivedNaiveFirstClassTheoremAttribute(fixtureFactory));
        }

        [Fact]
        public void InitializeWithNullFuncOfMethodInfoAndITestFixtureThrows()
        {
            Func<MethodInfo, ITestFixture> fixtureFactory = null;
            Assert.Throws<ArgumentNullException>(
                () => new DerivedNaiveFirstClassTheoremAttribute(fixtureFactory));
        }

        [Fact]
        public void CreateTestCommandsPassesTestFixtureToTestCase()
        {
            // Fixture setup
            var sut = new NaiveFirstClassTheoremAttribute(typeof(FakeTestFixture));
            const string methodName = "PassTestFixtureTest";
            var method = Reflector.Wrap(GetType().GetMethod(methodName));

            // Exercise system and Verify outcome
            Assert.DoesNotThrow(() => sut.CreateTestCommands(method).Single());
        }

        [Fact]
        public void CreateTestCommandsCreatesTestFixtureForEachTestCase()
        {
            int creatCount = 0;
            var sut = new DerivedNaiveFirstClassTheoremAttribute(() =>
            {
                creatCount++;
                return null;
            });
            const string methodName = "TestCasesTest";
            var method = Reflector.Wrap(GetType().GetMethod(methodName));

            sut.CreateTestCommands(method).ToArray();

            Assert.Equal(3, creatCount);
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

        private class DerivedNaiveFirstClassTheoremAttribute : NaiveFirstClassTheoremAttribute
        {
            public DerivedNaiveFirstClassTheoremAttribute(Func<ITestFixture> fixtureFactory)
                : base(fixtureFactory)
            {
            }

            public DerivedNaiveFirstClassTheoremAttribute(Func<MethodInfo, ITestFixture> fixtureFactory)
                : base(fixtureFactory)
            {
            }
        }
    }
}
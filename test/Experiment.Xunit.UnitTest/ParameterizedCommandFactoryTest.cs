namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Ploeh.Albedo;
    using global::Xunit;
    using global::Xunit.Sdk;

    public class ParameterizedCommandFactoryTest
    {
        [Fact]
        public void SutIsTestCommandFactory()
        {
            var sut = new ParameterizedCommandFactory();
            Assert.IsAssignableFrom<ITestCommandFactory>(sut);
        }

        [Fact]
        public void CreateWithNonParameterizedTestMethodReturnsEmpty()
        {
            var method = (MethodInfo)MethodBase.GetCurrentMethod();
            var sut = new ParameterizedCommandFactory();

            var actual = sut.Create(Reflector.Wrap(method), null);

            Assert.Empty(actual);
        }

        [Fact]
        public void CreateWithParameterizedTestMethodReturnsCorrectMethod()
        {
            var method = new Methods<ParameterizedCommandFactoryTest>().Select(x => x.ParameterizedTestMethod(null));
            var sut = new ParameterizedCommandFactory();
            var factory = Mocked.Of<ITestFixtureFactory>();

            var actual = sut.Create(Reflector.Wrap(method), factory).Single();

            var command = Assert.IsAssignableFrom<ParameterizedCommand>(actual);
            var testInfo = Assert.IsAssignableFrom<TestInfo>(command.TestCommandInfo);
            Assert.True(HasValues(
                testInfo,
                method,
                factory));
        }

        [Fact]
        public void CreateWithParameterizedTestMethodWithReturnValueReturnsCorrectMethod()
        {
            var method = new Methods<ParameterizedCommandFactoryTest>().Select(
                x => x.ParameterizedTestMethodWithReturnValue(null));
            var sut = new ParameterizedCommandFactory();
            var factory = Mocked.Of<ITestFixtureFactory>();

            var actual = sut.Create(Reflector.Wrap(method), factory).Single();

            var command = Assert.IsAssignableFrom<ParameterizedCommand>(actual);
            var testInfo = Assert.IsAssignableFrom<TestInfo>(command.TestCommandInfo);
            Assert.True(HasValues(
                testInfo,
                method,
                factory));
        }

        private static bool HasValues(
           TestInfo testInfo,
           MethodInfo testMethod,
           ITestFixtureFactory factory)
        {
            return testInfo.TestMethod == testMethod
                && testInfo.Arguments.Count() == 0
                && testInfo.TestFixtureFactory == factory;
        }

        private void ParameterizedTestMethod(object argument)
        {
        }

        private object ParameterizedTestMethodWithReturnValue(object argument)
        {
            return null;
        }
    }
}

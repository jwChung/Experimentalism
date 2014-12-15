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
            var method = Reflector.Wrap(new Methods<ParameterizedCommandFactoryTest>().Select(
                x => x.ParameterizedTestMethod(null)));
            var sut = new ParameterizedCommandFactory();
            var factory = Mocked.Of<IFixtureFactory>();

            var actual = sut.Create(method, factory).Single();

            var command = Assert.IsAssignableFrom<ParameterizedCommand>(actual);
            var context = Assert.IsAssignableFrom<ParameterizedCommandContext>(command.TestCommandContext);
            Assert.Equal(method, context.TestMethod);
            Assert.Equal(factory, context.FixtureFactory);
            Assert.Empty(context.ExplicitArguments);
        }

        [Fact]
        public void CreateWithParameterizedTestMethodWithReturnValueReturnsCorrectMethod()
        {
            var method = Reflector.Wrap(new Methods<ParameterizedCommandFactoryTest>().Select(
                x => x.ParameterizedTestMethodWithReturnValue(null)));
            var sut = new ParameterizedCommandFactory();
            var factory = Mocked.Of<IFixtureFactory>();

            var actual = sut.Create(method, factory).Single();

            var command = Assert.IsAssignableFrom<ParameterizedCommand>(actual);
            var context = Assert.IsAssignableFrom<ParameterizedCommandContext>(command.TestCommandContext);
            Assert.Equal(method, context.TestMethod);
            Assert.Equal(factory, context.FixtureFactory);
            Assert.Empty(context.ExplicitArguments);
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

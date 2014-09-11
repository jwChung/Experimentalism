namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Ploeh.Albedo;
    using global::Xunit;
    using global::Xunit.Sdk;

    public class FactCommandFactoryTest
    {
        [Fact]
        public void SutIsTestCommandFactory()
        {
            var sut = new FactCommandFactory();
            Assert.IsAssignableFrom<ITestCommandFactory>(sut);
        }

        [Fact]
        public void CreateWithNullContextThrows()
        {
            var sut = new FactCommandFactory();
            Assert.Throws<ArgumentNullException>(() => sut.Create(null).ToArray());
        }

        [Fact]
        public void CreateReturnsEmtpyCommandIfTestMethodHasReturnValue()
        {
            var sut = new FactCommandFactory();
            var method = new Methods<FactCommandFactoryTest>().Select(x => x.NonVoidMethod());
            var context = Mocked.Of<ITestMethodContext>(x => x.Actual == method);

            var actual = sut.Create(context);

            Assert.Empty(actual);
        }

        [Fact]
        public void CreateReturnsEmtpyCommandIfTestMethodIsParameterized()
        {
            var sut = new FactCommandFactory();
            var method = new Methods<FactCommandFactoryTest>().Select(x => x.ParameterizedMethod(null, 0));
            var context = Mocked.Of<ITestMethodContext>(x => x.Actual == method);

            var actual = sut.Create(context);

            Assert.Empty(actual);
        }

        [Fact]
        public void CreateReturnsCorrectCommandIfTestMethodIsValid()
        {
            var sut = new FactCommandFactory();
            var method = new Methods<FactCommandFactoryTest>().Select(
                x => x.CreateReturnsCorrectCommandIfTestMethodIsValid());
            var context = Mocked.Of<ITestMethodContext>(x => x.Actual == method);
            var expected = method.ReflectedType.FullName + "." + method.Name;

            var actual = sut.Create(context).Single();

            var command = Assert.IsAssignableFrom<FactCommand>(actual);
            Assert.Equal(expected, command.DisplayName);
        }

        private object NonVoidMethod()
        {
            return null;
        }

        private void ParameterizedMethod(string arg1, int arg2)
        {
        }
    }
}

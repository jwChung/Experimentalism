namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Ploeh.Albedo;
    using global::Xunit;
    using global::Xunit.Extensions;
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
        public void CreateWithNullTestMethodThrows()
        {
            var sut = new FactCommandFactory();
            Assert.Throws<ArgumentNullException>(() => sut.Create(null, null).ToArray());
        }

        [Fact]
        public void CreateReturnsEmptyCommandIfTestMethodIsParameterized()
        {
            var sut = new FactCommandFactory();
            var method = new Methods<FactCommandFactoryTest>().Select(x => x.ParameterizedMethod(null, 0));

            var actual = sut.Create(Reflector.Wrap(method), null);

            Assert.Empty(actual);
        }

        [Theory]
        [ValidTestMethodData]
        public void CreateReturnsCorrectCommandIfTestMethodIsValid(MethodInfo method)
        {
            var sut = new FactCommandFactory();
            var expected = method.ReflectedType.FullName + "." + method.Name;

            var actual = sut.Create(Reflector.Wrap(method), null).Single();

            var command = Assert.IsAssignableFrom<FactCommand>(actual);
            Assert.Equal(expected, command.DisplayName);
        }

        private object NonVoidMethod()
        {
            return null;
        }

        private void VoidMethod()
        {
        }

        private void ParameterizedMethod(string arg1, int arg2)
        {
        }

        private class ValidTestMethodDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(
                MethodInfo methodUnderTest, Type[] parameterTypes)
            {
                yield return new object[]
                {
                    new Methods<FactCommandFactoryTest>().Select(
                        x => x.VoidMethod())
                };
                yield return new object[]
                {
                    new Methods<FactCommandFactoryTest>().Select(
                        x => x.NonVoidMethod())
                };
            }
        }
    }
}
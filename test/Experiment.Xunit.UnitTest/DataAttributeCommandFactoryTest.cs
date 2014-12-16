namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using global::Xunit;
    using global::Xunit.Extensions;
    using global::Xunit.Sdk;

    public class DataAttributeCommandFactoryTest
    {
        [Fact]
        public void SutIsTestCommandFactory()
        {
            var sut = new DataAttributeCommandFactory();
            Assert.IsAssignableFrom<ITestCommandFactory>(sut);
        }

        [Fact]
        public void CreateWithNullTestMethodThrows()
        {
            var sut = new DataAttributeCommandFactory();
            Assert.Throws<ArgumentNullException>(() => sut.Create(null, null));
        }

        [Fact]
        public void CreateReturnsEmptyWhenTestMethodDoesNotHaveDataAttribute()
        {
            var testMethod = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            var sut = new DataAttributeCommandFactory();

            var actual = sut.Create(testMethod, null);

            Assert.Empty(actual);
        }

        [Fact]
        [InlineData("anonymous", 123)]
        [ClassData(typeof(ClassTestData))]
        public void CreateReturnsCorrectCommand()
        {
            var testMethod = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            var sut = new DataAttributeCommandFactory();
            var factory = Mocked.Of<ISpecimenBuilderFactory>();

            var actual = sut.Create(testMethod, factory).ToArray();

            Assert.Equal(3, actual.Length);
            foreach (var testCommand in actual)
            {
                var parameterizedCommand = Assert.IsAssignableFrom<ParameterizedCommand>(testCommand);
                var context = Assert.IsAssignableFrom<ParameterizedCommandContext>(parameterizedCommand.TestCommandContext);

                Assert.Equal(testMethod, context.TestMethod);
                Assert.Equal(factory, context.BuilderFactory);
                Assert.Equal(new object[] { "anonymous", 123 }, context.ExplicitArguments);
            }
        }

        private class ClassTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { "anonymous", 123 };
                yield return new object[] { "anonymous", 123 };
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }
    }
}
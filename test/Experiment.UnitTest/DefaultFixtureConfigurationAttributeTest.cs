namespace Jwc.Experiment
{
    using System;
    using System.Reflection;
    using Xunit;
    using Xunit.Extensions;

    public class DefaultFixtureConfigurationAttributeTest
    {
        [Fact]
        public void SutIsAssemblyFixtureConfigurationAttribute()
        {
            var sut = new DefaultFixtureConfigurationAttribute(typeof(DelegatingTestFixtureFactory));
            Assert.IsAssignableFrom<TestAssemblyConfigurationAttribute>(sut);
        }

        [Fact]
        public void InitializeWithNullFactoryTypeThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new DefaultFixtureConfigurationAttribute(null));
        }

        [Theory]
        [InlineData(typeof(int))]
        [InlineData(typeof(object))]
        [InlineData(typeof(ITestFixtureFactory))]
        [InlineData(typeof(AbstractTestFixtureFactory))]
        public void InitializeWithInvalidFactoryTypeThrows(Type factoryType)
        {
            Assert.Throws<ArgumentException>(
                () => new DefaultFixtureConfigurationAttribute(factoryType));
        }

        [Fact]
        public void InitializeWithNullFactoryThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new TssDefaultFixtureConfigurationAttribute((ITestFixtureFactory)null));
        }

        [Fact]
        public void FactoryTypeIsCorrectWhenInitializedWithFactoryType()
        {
            var expected = typeof(DelegatingTestFixtureFactory);
            var sut = new DefaultFixtureConfigurationAttribute(expected);

            var actual = sut.FactoryType;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FactoryTypeIsCorrectWhenInitializedWithFactory()
        {
            var factory = new DelegatingTestFixtureFactory();
            var sut = new TssDefaultFixtureConfigurationAttribute(factory);

            var actual = sut.FactoryType;

            Assert.Equal(factory.GetType(), actual);
        }

        [Fact]
        public void FactoryIsCorrectWhenInitializedWithFactoryType()
        {
            var expected = typeof(DelegatingTestFixtureFactory);
            var sut = new DefaultFixtureConfigurationAttribute(expected);

            var actual = sut.Factory;

            Assert.IsType<DelegatingTestFixtureFactory>(actual);
        }

        [Fact]
        public void FactoryIsCorrectWhenInitializedWithFactory()
        {
            var factory = new DelegatingTestFixtureFactory();
            var sut = new TssDefaultFixtureConfigurationAttribute(factory);

            var actual = sut.Factory;

            Assert.Equal(factory, actual);
        }

        [Fact]
        public void SetupSetsSuppliedFactoryAsCurrentOfDefaultFixtureFactory()
        {
            try
            {
                var factory = new DelegatingTestFixtureFactory();
                var sut = new TssDefaultFixtureConfigurationAttribute(factory);

                sut.CallSetup(null);

                Assert.Equal(factory, DefaultFixtureFactory.Current);
            }
            finally
            {
                DefaultFixtureFactory.SetCurrent(null);
            }
        }

        [Fact]
        public void SetupSetsInstanceOfSuppliedFactoryTypeAsCurrentOfDefaultFixtureFactory()
        {
            try
            {
                var factoryType = typeof(DelegatingTestFixtureFactory);
                var sut = new TssDefaultFixtureConfigurationAttribute(factoryType);

                sut.CallSetup(null);

                Assert.IsType<DelegatingTestFixtureFactory>(DefaultFixtureFactory.Current);
            }
            finally
            {
                DefaultFixtureFactory.SetCurrent(null);
            }
        }

        private class TssDefaultFixtureConfigurationAttribute : DefaultFixtureConfigurationAttribute
        {
            public TssDefaultFixtureConfigurationAttribute(Type factoryType) : base(factoryType)
            {
            }

            public TssDefaultFixtureConfigurationAttribute(ITestFixtureFactory factory) : base(factory)
            {
            }

            public void CallSetup(Assembly testAssembly)
            {
                this.Setup(testAssembly);
            }
        }

        private abstract class AbstractTestFixtureFactory : ITestFixtureFactory
        {
            public abstract ITestFixture Create(MethodInfo testMethod);
        }
    }
}
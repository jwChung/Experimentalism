using System;
using System.Reflection;
using Xunit;
using Xunit.Extensions;

namespace Jwc.Experiment
{
    public class TestFixtureConfigurationAttributeTest
    {
        [Fact]
        public void SutIsAssemblyFixtureConfigurationAttribute()
        {
            var sut = new TestFixtureConfigurationAttribute(typeof(DelegatingTestFixtureFactory));
            Assert.IsAssignableFrom<TestAssemblyConfigurationAttribute>(sut);
        }

        [Fact]
        public void InitializeWithNullFactoryTypeThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new TestFixtureConfigurationAttribute(null));
        }

        [Theory]
        [InlineData(typeof(int))]
        [InlineData(typeof(object))]
        [InlineData(typeof(ITestFixtureFactory))]
        [InlineData(typeof(AbstractTestFixtureFactory))]
        public void InitializeWithInvalidFactoryTypeThrows(Type factoryType)
        {
            Assert.Throws<ArgumentException>(
                () => new TestFixtureConfigurationAttribute(factoryType));
        }

        [Fact]
        public void InitializeWithNullFactoryThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new TssTestFixtureConfigurationAttribute((ITestFixtureFactory)null));
        }

        [Fact]
        public void FactoryTypeIsCorrectWhenInitializedWithFactoryType()
        {
            var expected = typeof(DelegatingTestFixtureFactory);
            var sut = new TestFixtureConfigurationAttribute(expected);

            var actual = sut.FactoryType;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FactoryTypeIsCorrectWhenInitializedWithFactory()
        {
            var factory = new DelegatingTestFixtureFactory();
            var sut = new TssTestFixtureConfigurationAttribute(factory);

            var actual = sut.FactoryType;

            Assert.Equal(factory.GetType(), actual);
        }

        [Fact]
        public void FactoryIsCorrectWhenInitializedWithFactoryType()
        {
            var expected = typeof(DelegatingTestFixtureFactory);
            var sut = new TestFixtureConfigurationAttribute(expected);

            var actual = sut.Factory;

            Assert.IsType<DelegatingTestFixtureFactory>(actual);
        }

        [Fact]
        public void FactoryIsCorrectWhenInitializedWithFactory()
        {
            var factory = new DelegatingTestFixtureFactory();
            var sut = new TssTestFixtureConfigurationAttribute(factory);

            var actual = sut.Factory;

            Assert.Equal(factory, actual);
        }

        [NewAppDomainFact]
        public void SetupSetsSuppliedFactoryAsCurrentOfDefaultFixtureFactory()
        {
            var factory = new DelegatingTestFixtureFactory();
            var sut = new TssTestFixtureConfigurationAttribute(factory);

            sut.CallSetup(null);

            Assert.Equal(factory, DefaultFixtureFactory.Current);
        }

        [NewAppDomainFact]
        public void SetupSetsInstanceOfSuppliedFactoryTypeAsCurrentOfDefaultFixtureFactory()
        {
            var factoryType = typeof(DelegatingTestFixtureFactory);
            var sut = new TssTestFixtureConfigurationAttribute(factoryType);

            sut.CallSetup(null);

            Assert.IsType<DelegatingTestFixtureFactory>(DefaultFixtureFactory.Current);
        }

        private class TssTestFixtureConfigurationAttribute : TestFixtureConfigurationAttribute
        {
            public TssTestFixtureConfigurationAttribute(Type factoryType) : base(factoryType)
            {
            }

            public TssTestFixtureConfigurationAttribute(ITestFixtureFactory factory) : base(factory)
            {
            }

            public void CallSetup(Assembly testAssembly)
            {
                base.Setup(testAssembly);
            }
        }

        private abstract class AbstractTestFixtureFactory : ITestFixtureFactory
        {
            public abstract ITestFixture Create(MethodInfo testMethod);
        }
    }
}
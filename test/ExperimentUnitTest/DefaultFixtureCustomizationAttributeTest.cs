using System;
using System.Reflection;
using Xunit;
using Xunit.Extensions;

namespace Jwc.Experiment
{
    public class DefaultFixtureCustomizationAttributeTest
    {
        [Fact]
        public void SutIsAssemblyCustomizationAttribute()
        {
            var sut = new DefaultFixtureCustomizationAttribute(typeof(DelegatingTestFixtureFactory));
            Assert.IsAssignableFrom<AssemblyCustomizationAttribute>(sut);
        }

        [Fact]
        public void InitializeWithNullFactoryTypeThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new DefaultFixtureCustomizationAttribute(null));
        }

        [Theory]
        [InlineData(typeof(int))]
        [InlineData(typeof(object))]
        [InlineData(typeof(ITestFixtureFactory))]
        [InlineData(typeof(AbstractTestFixtureFactory))]
        public void InitializeWithInvalidFactoryTypeThrows(Type factoryType)
        {
            Assert.Throws<ArgumentException>(
                () => new DefaultFixtureCustomizationAttribute(factoryType));
        }

        [Fact]
        public void InitializeWithNullFactoryThrows()
        {
            Assert.Throws<ArgumentNullException>(
                () => new TssDefaultFixtureCustomizationAttribute((ITestFixtureFactory)null));
        }

        [Fact]
        public void FactoryTypeIsCorrectWhenInitializedWithFactoryType()
        {
            var expected = typeof(DelegatingTestFixtureFactory);
            var sut = new DefaultFixtureCustomizationAttribute(expected);

            var actual = sut.FactoryType;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FactoryTypeIsCorrectWhenInitializedWithFactory()
        {
            var factory = new DelegatingTestFixtureFactory();
            var sut = new TssDefaultFixtureCustomizationAttribute(factory);

            var actual = sut.FactoryType;

            Assert.Equal(factory.GetType(), actual);
        }

        [Fact]
        public void FactoryIsCorrectWhenInitializedWithFactoryType()
        {
            var expected = typeof(DelegatingTestFixtureFactory);
            var sut = new DefaultFixtureCustomizationAttribute(expected);

            var actual = sut.Factory;

            Assert.IsType<DelegatingTestFixtureFactory>(actual);
        }

        [Fact]
        public void FactoryIsCorrectWhenInitializedWithFactory()
        {
            var factory = new DelegatingTestFixtureFactory();
            var sut = new TssDefaultFixtureCustomizationAttribute(factory);

            var actual = sut.Factory;

            Assert.Equal(factory, actual);
        }

        [NewAppDomainFact]
        public void SetupSetsSuppliedFactoryAsCurrentOfDefaultFixtureFactory()
        {
            var factory = new DelegatingTestFixtureFactory();
            var sut = new TssDefaultFixtureCustomizationAttribute(factory);

            sut.CallSetup(null);

            Assert.Equal(factory, DefaultFixtureFactory.Current);
        }

        [NewAppDomainFact]
        public void SetupSetsInstanceOfSuppliedFactoryTypeAsCurrentOfDefaultFixtureFactory()
        {
            var factoryType = typeof(DelegatingTestFixtureFactory);
            var sut = new TssDefaultFixtureCustomizationAttribute(factoryType);

            sut.CallSetup(null);

            Assert.IsType<DelegatingTestFixtureFactory>(DefaultFixtureFactory.Current);
        }

        private class TssDefaultFixtureCustomizationAttribute : DefaultFixtureCustomizationAttribute
        {
            public TssDefaultFixtureCustomizationAttribute(Type factoryType) : base(factoryType)
            {
            }

            public TssDefaultFixtureCustomizationAttribute(ITestFixtureFactory factory) : base(factory)
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
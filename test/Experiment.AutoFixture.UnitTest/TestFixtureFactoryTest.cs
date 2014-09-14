namespace Jwc.Experiment.AutoFixture
{
    using System;
    using System.Reflection;
    using Ploeh.AutoFixture;
    using global::Xunit;

    public partial class TestFixtureFactoryTest
    {
        [Fact]
        public void SutIsTestFixtureFactory()
        {
            var sut = new TestFixtureFactory();
            Assert.IsAssignableFrom<ITestFixtureFactory>(sut);
        }

        [Fact]
        public void CreateReturnsCorrectTestFixture()
        {
            var sut = new TestFixtureFactory();
            var actual = sut.Create((MethodInfo)MethodBase.GetCurrentMethod());
            Assert.IsAssignableFrom<TestFixture>(actual);
        }

        [Fact]
        public void CreateWithNullTestMethodThrows()
        {
            var sut = new TestFixtureFactory();
            Assert.Throws<ArgumentNullException>(() => sut.Create(null));
        }

        [Fact]
        public void CreateReturnsFixtureOmittingAutoProperties()
        {
            var sut = new TestFixtureFactory();

            var actual = sut.Create((MethodInfo)MethodBase.GetCurrentMethod());

            var testFixture = Assert.IsAssignableFrom<TestFixture>(actual);
            Assert.True(testFixture.Fixture.OmitAutoProperties);
        }

        [Fact]
        public void CreateCanReturnFixtureAllowingAutoProperties()
        {
            var sut = new TssTestFixtureFactory { OnCreateCustomization = m => new AutoPropertiesCustomizatoin() };

            var actual = sut.Create((MethodInfo)MethodBase.GetCurrentMethod());

            var testFixture = Assert.IsAssignableFrom<TestFixture>(actual);
            Assert.False(testFixture.Fixture.OmitAutoProperties);
        }

        [Fact]
        public void CreateReturnsFixtureBeingAbleToCreateInstanceOfAbstractType()
        {
            var sut = new TestFixtureFactory();
            var actual = sut.Create((MethodInfo)MethodBase.GetCurrentMethod());
            Assert.NotNull(actual.Create(typeof(IDisposable)));
        }

        [Fact]
        public void CreateCanReturnFixtureNotBeingAbleToCreateInstanceOfAbstractType()
        {
            var sut = new TssTestFixtureFactory { OnCreateCustomization = m => new EmptyCustomization() };
            var actual = sut.Create((MethodInfo)MethodBase.GetCurrentMethod());
            Assert.Throws<ObjectCreationException>(() => actual.Create(typeof(IDisposable)));
        }

        [Fact]
        public void CreateReturnsCorrectFixtureUsingEmptyCustomization()
        {
            var testMethod = (MethodInfo)MethodBase.GetCurrentMethod();
            var sut = new TssTestFixtureFactory
            {
                OnCreateCustomization = m =>
                {
                    Assert.Equal(testMethod, m);
                    return new EmptyCustomization();
                }
            };

            var actual = sut.Create((MethodInfo)MethodBase.GetCurrentMethod());

            var fixture = Assert.IsAssignableFrom<TestFixture>(actual).Fixture;
            Assert.False(fixture.OmitAutoProperties);
            Assert.Throws<ObjectCreationException>(() => fixture.Create<IDisposable>());
        }
    }

    public partial class TestFixtureFactoryTest
    {
        private class TssTestFixtureFactory : TestFixtureFactory
        {
            public TssTestFixtureFactory()
            {
                this.OnCreateCustomization = m => base.GetCustomization(m);
            }

            public Func<MethodInfo, ICustomization> OnCreateCustomization { get; set; }

            protected override ICustomization GetCustomization(MethodInfo testMethod)
            {
                return this.OnCreateCustomization(testMethod);
            }
        }

        private class EmptyCustomization : ICustomization
        {
            public void Customize(IFixture fixture)
            {
            }
        }

        private class AutoPropertiesCustomizatoin : ICustomization
        {
            public void Customize(IFixture fixture)
            {
                if (fixture == null)
                    throw new ArgumentNullException("fixture");

                fixture.OmitAutoProperties = false;
            }
        }
    }
}
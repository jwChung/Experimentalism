namespace Jwc.Experiment.AutoFixture
{
    using System;
    using Ploeh.AutoFixture;
    using global::Xunit;
    using global::Xunit.Extensions;

    public class TestFixtureTest
    {
        [Fact]
        public void SutIsTestFixture()
        {
            var sut = new TestFixture(new Fixture());
            Assert.IsAssignableFrom<ITestFixture>(sut);
        }
        
        [Fact]
        public void InitializeModestWithNullFixtureThrows()
        {
            Assert.Throws<ArgumentNullException>(() => new TestFixture(null));
        }

        [Fact]
        public void InitializeGreedyCtorWithAnyNullArgumentsThrows()
        {
            var fixture = Mocked.Of<IFixture>();
            var customization = Mocked.Of<ICustomization>();

            Assert.Throws<ArgumentNullException>(() => new TestFixture(null, customization));
            Assert.Throws<ArgumentNullException>(() => new TestFixture(fixture, null));
        }

        [Fact]
        public void InitializeModestCtorCorrectlyInitializes()
        {
            var fixture = Mocked.Of<IFixture>();

            var sut = new TestFixture(fixture);

            Assert.Equal(fixture, sut.Fixture);
            var customization = Assert.IsType<CompositeCustomization>(sut.Customization);
            Assert.Empty(customization.Customizations);
        }

        [Fact]
        public void InitializeGreedyCtorCorrectlyInitializes()
        {
            var fixture = Mocked.Of<IFixture>();
            var customization = Mocked.Of<ICustomization>();

            var sut = new TestFixture(fixture, customization);

            Assert.Equal(fixture, sut.Fixture);
            Assert.Equal(customization, sut.Customization);
        }

        [Fact]
        public void CreateReturnsCorrectSpecimenUsingModestCtor()
        {
            var request = typeof(object);
            var fixture = new Fixture();
            var expected = fixture.Freeze<object>();
            var sut = new TestFixture(fixture);

            var actual = sut.Create(request);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateReturnsCorrectSpecimenUsingGreedyCtor()
        {
            var fixture = new Fixture();
            var customization = new FreezingCustomization(typeof(object));
            var sut = new TestFixture(fixture, customization);
            var request = typeof(object);

            var actual = sut.Create(request);

            Assert.Equal(fixture.Create<object>(), actual);
        }

        [Theory]
        [InlineData(typeof(ITestFixture))]
        [InlineData(typeof(TestFixture))]
        public void CreateTestFixtureReturnsItself(Type testFixtureType)
        {
            var sut = new TestFixture(new Fixture());
            var actual = sut.Create(testFixtureType);
            Assert.Same(sut, actual);
        }
    }
}
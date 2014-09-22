namespace Jwc.Experiment.AutoFixture
{
    using System;
    using System.Linq;
    using Moq;
    using Moq.Protected;
    using Ploeh.AutoFixture;
    using global::Xunit;

    public class TestFixtureFactoryTest
    {
        [Fact]
        public void SutIsTestFixtureFactory()
        {
            var sut = new TestFixtureFactory();
            Assert.IsAssignableFrom<ITestFixtureFactory>(sut);
        }

        [Fact]
        public void CreateWithNullContextThrows()
        {
            var sut = new TestFixtureFactory();
            Assert.Throws<ArgumentNullException>(() => sut.Create(null));
        }

        [Fact]
        public void CreateReturnsCorrectlyCustomizedFixture()
        {
            var sut  = Mocked.Of<TestFixtureFactory>();
            var context = Mocked.Of<ITestMethodContext>();
            var customization = Mocked.Of<ICustomization>();
            sut.ToMock().Protected().Setup<ICustomization>(
                "GetCustomization",
                context)
                .Returns(customization);

            var actual = sut.Create(context);

            var fixture = Assert.IsAssignableFrom<Fixture>(
                Assert.IsAssignableFrom<TestFixture>(actual).Fixture);
            customization.ToMock().Verify(c => c.Customize(fixture));
        }

        [Fact]
        public void CreateReturnsFixtureCustomizedWithCorrectCustomization()
        {
            // Fixture setup
            var sut = new TssTestFixtureFactory();

            var context = Mocked.Of<ITestMethodContext>();

            // Exercise system
            sut.Create(context);

            // Verify outcome
            var customizations = Assert.IsAssignableFrom<CompositeCustomization>(
                sut.Customization).Customizations;

            Assert.Equal(2, customizations.Count());

            customizations.OfType<OmitAutoPropertiesCustomization>().Single();
            Assert.Equal(
                context,
                customizations.OfType<TestParametersCustomization>().Single().TestMethodContext);
        }

        private class TssTestFixtureFactory : TestFixtureFactory
        {
            public ICustomization Customization { get; set; }

            protected override ICustomization GetCustomization(ITestMethodContext context)
            {
                this.Customization = base.GetCustomization(context);
                return this.Customization;
            }
        }
    }
}
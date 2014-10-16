namespace Jwc.Experiment.AutoFixture
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Moq;
    using Moq.Protected;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoMoq;
    using global::Xunit;

    [Obsolete]
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
        public void CreateUsesCustomizationsInProperOrder()
        {
            // Fixture setup
            var sut = new TssTestFixtureFactory();

            var context = Mocked.Of<ITestMethodContext>();

            var expected = new[]
            {
                typeof(OmitAutoPropertiesCustomization),
                typeof(AutoMoqCustomization),
                typeof(TestParametersCustomization)
            };

            // Exercise system
            sut.Create(context);

            // Verify outcome
            var actual = Assert.IsAssignableFrom<CompositeCustomization>(
                sut.Customization).Customizations.Select(c => c.GetType());
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateReturnsFixtureCustomizedWithCorrectTestParametersCustomization()
        {
            var sut = new TssTestFixtureFactory();
            var parameters = new[] { Mocked.Of<ParameterInfo>(), Mocked.Of<ParameterInfo>() };
            var context = Mocked.Of<ITestMethodContext>(
                c => c.ActualMethod == Mocked.Of<MethodInfo>(
                    m => m.GetParameters() == parameters));

            sut.Create(context);

            var customization = Assert.IsAssignableFrom<CompositeCustomization>(sut.Customization)
                .Customizations.OfType<TestParametersCustomization>().Single();
            Assert.Equal(parameters, customization.Parameters);
        }

        private class TssTestFixtureFactory : TestFixtureFactory
        {
            public ICustomization Customization { get; set; }

            [Obsolete]
            protected override ICustomization GetCustomization(ITestMethodContext context)
            {
                this.Customization = base.GetCustomization(context);
                return this.Customization;
            }
        }
    }
}
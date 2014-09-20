﻿namespace Jwc.Experiment.AutoFixture
{
    using System;
    using System.Linq;
    using Ploeh.AutoFixture;
    using global::Xunit;

    public class AutoPropertiesAttributeTest
    {
        [Fact]
        public void SutIsCustomizeAttribute()
        {
            var sut = new AutoPropertiesAttribute();
            Assert.IsAssignableFrom<CustomizeAttribute>(sut);
        }

        [Fact]
        public void GetCustomizationReturnsCorrectCustomization()
        {
            var sut = new AutoPropertiesAttribute();
            var parameter = GetType().GetMethod("Test").GetParameters().Single();
            var fixture = new Fixture { OmitAutoProperties = true };

            var actual = sut.GetCustomization(parameter);

            actual.Customize(fixture);
            var specimen = fixture.Create<ConcreteType>();
            Assert.NotNull(specimen.Property4);
            Assert.NotNull(specimen.Property5);
        }

        [Fact]
        public void GetCustomizationWithNullParameterThrows()
        {
            var sut = new AutoPropertiesAttribute();
            Assert.Throws<ArgumentNullException>(() => sut.GetCustomization(null));
        }

        public void Test(ConcreteType arg)
        {
        }
    }
}
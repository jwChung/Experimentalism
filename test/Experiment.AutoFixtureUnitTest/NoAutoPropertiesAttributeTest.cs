// Original source code: https://github.com/AutoFixture/AutoFixture
// Copyright           : Copyright (c) 2013 Mark Seemann  
// License             : The MIT License

using System;
using System.Linq;
using Ploeh.AutoFixture;
using Xunit;

namespace Jwc.Experiment.AutoFixture
{
    public class NoAutoPropertiesAttributeTest
    {
        [Fact]
        public void SutIsAttribute()
        {
            // Fixture setup
            // Exercise system
            var sut = new NoAutoPropertiesAttribute();
            // Verify outcome
            Assert.IsAssignableFrom<CustomizeAttribute>(sut);
        }

        [Fact]
        public void GetCustomizationFromNullParameterThrows()
        {
            // Fixture setup
            var sut = new NoAutoPropertiesAttribute();
            // Exercise system and verify the outcome
            Assert.Throws<ArgumentNullException>(() => sut.GetCustomization(null));
        }

        [Fact]
        public void GetCustomizationReturnsTheCorrectResult()
        {
            // Fixture setup
            var sut = new NoAutoPropertiesAttribute();
            var parameter = typeof(TypeWithOverloadedMembers)
                .GetMethod("DoSomething", new[] { typeof(object) })
                .GetParameters()
                .Single();
            // Exercise system
            var result = sut.GetCustomization(parameter);
            // Verify the outcome
            Assert.IsAssignableFrom<NoAutoPropertiesCustomization>(result);
        }
    }
}
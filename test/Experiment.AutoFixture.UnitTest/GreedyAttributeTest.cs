﻿// Original source code: https://github.com/AutoFixture/AutoFixture
// Copyright           : Copyright (c) 2013 Mark Seemann  
// License             : The MIT License
namespace Jwc.Experiment.AutoFixture
{
    using System;
    using System.Linq;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;
    using global::Xunit;

    public class GreedyAttributeTest
    {
        [Fact]
        public void SutIsAttribute()
        {
            // Fixture setup
            // Exercise system
            var sut = new GreedyAttribute();

            // Verify outcome
            Assert.IsAssignableFrom<CustomizeAttribute>(sut);

            // Teardown
        }

        [Fact]
        public void GetCustomizationFromNullParamterThrows()
        {
            // Fixture setup
            var sut = new GreedyAttribute();

            // Exercise system and verify outcome
            Assert.Throws<ArgumentNullException>(
                () => sut.GetCustomization(null));

            // Teardown
        }

        [Fact]
        public void GetCustomizationReturnsCorrectResult()
        {
            // Fixture setup
            var sut = new GreedyAttribute();
            var parameter = typeof(TypeWithOverloadedMembers).GetMethod("DoSomething", new[] { typeof(object) })
                .GetParameters().Single();

            // Exercise system
            var result = sut.GetCustomization(parameter);

            // Verify outcome
            var invoker = Assert.IsAssignableFrom<ConstructorCustomization>(result);
            Assert.Equal(parameter.ParameterType, invoker.TargetType);
            Assert.IsAssignableFrom<GreedyConstructorQuery>(invoker.Query);

            // Teardown
        }
    }
}
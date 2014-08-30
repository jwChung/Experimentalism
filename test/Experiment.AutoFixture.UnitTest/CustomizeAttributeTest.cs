// Original source code: https://github.com/AutoFixture/AutoFixture
// Copyright           : Copyright (c) 2013 Mark Seemann  
// License             : The MIT License
namespace Jwc.Experiment.AutoFixture
{
    using System;
    using global::Xunit;

    public class CustomizeAttributeTest
    {
        [Fact]
        public void TestableSutIsSut()
        {
            // Fixture setup
            // Exercise system
            var sut = new DelegatingCustomizeAttribute();

            // Verify outcome
            Assert.IsAssignableFrom<CustomizeAttribute>(sut);

            // Teardown
        }

        [Fact]
        public void SutIsAttribute()
        {
            // Fixture setup
            // Exercise system
            var sut = new DelegatingCustomizeAttribute();

            // Verify outcome
            Assert.IsAssignableFrom<Attribute>(sut);

            // Teardown
        }
    }
}
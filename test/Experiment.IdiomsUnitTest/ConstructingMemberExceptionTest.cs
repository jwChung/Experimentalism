﻿using System;
using Xunit;

namespace Jwc.Experiment
{
    public class ConstructingMemberExceptionTest
    {
        [Fact]
        public void SutIsException()
        {
            var sut = new ConstructingMemberException();
            Assert.IsAssignableFrom<Exception>(sut);
        }
    }
}
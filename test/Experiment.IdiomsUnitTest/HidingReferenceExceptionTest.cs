using System;
using Xunit;

namespace Jwc.Experiment
{
    public class HidingReferenceExceptionTest
    {
        [Fact]
        public void SutIsException()
        {
            var sut = new HidingReferenceException();
            Assert.IsAssignableFrom<Exception>(sut);
        } 
    }
}
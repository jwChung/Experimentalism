using System;
using Xunit;

namespace Jwc.Experiment
{
    public class IndirectReferenceExceptionTest
    {
        [Fact]
        public void SutIsException()
        {
            var sut = new IndirectReferenceException();
            Assert.IsAssignableFrom<Exception>(sut);
        }
    }
}
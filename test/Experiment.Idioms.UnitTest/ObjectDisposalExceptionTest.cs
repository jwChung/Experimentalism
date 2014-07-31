using System;
using Xunit;

namespace Jwc.Experiment
{
    public class ObjectDisposalExceptionTest
    {
        [Fact]
        public void SutIsException()
        {
            var sut = new ObjectDisposalException();
            Assert.IsAssignableFrom<Exception>(sut);
        }
    }
}
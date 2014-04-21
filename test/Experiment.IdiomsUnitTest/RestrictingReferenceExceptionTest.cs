using System;
using Xunit;

namespace Jwc.Experiment
{
    public class RestrictingReferenceExceptionTest
    {
        [Fact]
        public void SutIsException()
        {
            var sut = new RestrictingReferenceException();
            Assert.IsAssignableFrom<Exception>(sut);
        }
    }
}
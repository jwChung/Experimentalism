using System;
using Xunit;

namespace Jwc.Experiment.Idioms
{
    public class RestrictiveReferenceExceptionTest
    {
        [Fact]
        public void SutIsException()
        {
            var sut = new RestrictiveReferenceException();
            Assert.IsAssignableFrom<Exception>(sut);
        }
    }
}
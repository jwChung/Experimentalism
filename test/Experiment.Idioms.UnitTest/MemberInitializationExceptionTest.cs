using System;
using Xunit;

namespace Jwc.Experiment
{
    public class MemberInitializationExceptionTest
    {
        [Fact]
        public void SutIsException()
        {
            var sut = new MemberInitializationException();
            Assert.IsAssignableFrom<Exception>(sut);
        }
    }
}
namespace Jwc.Experiment
{
    using System;
    using global::Xunit;

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
namespace Jwc.Experiment
{
    using System;
    using global::Xunit;

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
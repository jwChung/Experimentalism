namespace Jwc.Experiment
{
    using System;
    using global::Xunit;

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
namespace Jwc.Experiment
{
    using System;
    using global::Xunit;

    public class NotExposedReferenceExceptionTest
    {
        [Fact]
        public void SutIsException()
        {
            var sut = new NotExposedReferenceException();
            Assert.IsAssignableFrom<Exception>(sut);
        }
    }
}
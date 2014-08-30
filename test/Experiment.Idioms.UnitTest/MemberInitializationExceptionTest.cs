namespace Jwc.Experiment
{
    using System;
    using global::Xunit;

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
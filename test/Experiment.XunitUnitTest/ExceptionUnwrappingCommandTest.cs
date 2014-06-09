using Xunit;
using Xunit.Sdk;

namespace Jwc.Experiment.Xunit
{
    public class ExceptionUnwrappingCommandTest
    {
        [Fact]
        public void SutIsTestCommand()
        {
            var sut = new ExceptionUnwrappingCommand();
            Assert.IsAssignableFrom<ITestCommand>(sut);
        } 
    }
}
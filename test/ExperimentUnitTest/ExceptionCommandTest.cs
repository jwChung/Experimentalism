using System.Reflection;
using Xunit;
using Xunit.Sdk;

namespace Jwc.Experiment
{
    public class ExceptionCommandTest
    {
        [Fact]
        public void SutIsTestCommand()
        {
            var sut = new ExceptionCommand(Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod()));
            Assert.IsAssignableFrom<TestCommand>(sut);
        }
    }
}
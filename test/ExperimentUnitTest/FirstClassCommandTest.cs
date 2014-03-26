using System.Reflection;
using Xunit;
using Xunit.Sdk;

namespace Jwc.Experiment
{
    public class FirstClassCommandTest
    {
        [Fact]
        public void SutIsFactCommand()
        {
            var method = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            var sut = new FirstClassCommand(method);
            Assert.IsAssignableFrom<FactCommand>(sut);
        }
    }
}
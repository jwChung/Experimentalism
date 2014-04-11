using System.Reflection;
using Xunit;
using Xunit.Sdk;

namespace Jwc.Experiment.Idioms
{
    public class IdiomaticTestCommandTest
    {
        [Fact]
        public void SutIsTestCommand()
        {
            var dummyMethod = Reflector.Wrap((MethodInfo)MethodBase.GetCurrentMethod());
            var sut = new IdiomaticTestCommand(dummyMethod, null, null);
            Assert.IsAssignableFrom<TestCommand>(sut);
        } 
    }
}
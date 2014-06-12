using System;
using System.Reflection;

namespace Jwc.Experiment.Xunit
{
    public class TestAssemblyExceptionConfigurationAttribute : TestAssemblyConfigurationAttribute
    {
        protected override void Setup(Assembly testAssembly)
        {
            throw new InvalidOperationException();
        }
    }
}
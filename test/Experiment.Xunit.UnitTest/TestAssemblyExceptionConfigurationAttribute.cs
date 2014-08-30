namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Reflection;

    public class TestAssemblyExceptionConfigurationAttribute : TestAssemblyConfigurationAttribute
    {
        protected override void Setup(Assembly testAssembly)
        {
            throw new InvalidOperationException();
        }
    }
}
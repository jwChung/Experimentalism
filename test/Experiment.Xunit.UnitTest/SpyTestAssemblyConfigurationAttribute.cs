[assembly: Jwc.Experiment.Xunit.SpyTestAssemblyConfiguration]

namespace Jwc.Experiment.Xunit
{
    using System.Reflection;
    using Jwc.Experiment.Xunit;
    
    public class SpyTestAssemblyConfigurationAttribute : TestAssemblyConfigurationAttribute
    {
        public static int SetupCount { get; set; }

        public static int TeardownCount { get; set; }

        public static Assembly SetupAssembly { get; set; }

        public static Assembly TeardownAssembly { get; set; }

        protected override void Setup(Assembly testAssembly)
        {
            SetupAssembly = testAssembly;
            SetupCount++;
        }

        protected override void Teardown(Assembly testAssembly)
        {
            TeardownAssembly = testAssembly;
            TeardownCount++;
        }
    }
}
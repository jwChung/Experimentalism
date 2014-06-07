using System.Reflection;

namespace Jwc.Experiment.Xunit
{
    public class SpyAssemblyFixtureConfigurationAttribute : AssemblyFixtureConfigurationAttribute
    {
        public static int SetUpCount { get; set; }

        public static int TearDownCount { get; set; }

        public static Assembly SetUpAssembly { get; set; }

        public static Assembly TearDownAssembly { get; set; }

        protected override void SetUp(Assembly testAssembly)
        {
            SetUpAssembly = testAssembly;
            SetUpCount++;
        }

        protected override void TearDown(Assembly testAssembly)
        {
            TearDownAssembly = testAssembly;
            TearDownCount++;
        }
    }
}
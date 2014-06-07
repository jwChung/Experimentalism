using System.Reflection;

namespace Jwc.Experiment.Xunit
{
    public class SpyAssemblyFixtureConfigurationAttribute : AssemblyFixtureConfigurationAttribute
    {
        public static int SetUpCount { get; set; }

        public static int TearDownCount { get; set; }

        public static Assembly SetUpAssembly { get; set; }

        public static Assembly TearDownAssembly { get; set; }

        public override void SetUp(Assembly testAssembly)
        {
            SetUpAssembly = testAssembly;
            SetUpCount++;
        }

        public override void TearDown(Assembly testAssembly)
        {
            TearDownAssembly = testAssembly;
            TearDownCount++;
        }
    }
}
using System.Reflection;

namespace Jwc.Experiment.Xunit
{
    public class SpyOtherAssemblyFixtureConfigurationAttribute : AssemblyFixtureConfigurationAttribute
    {
        public static int SetupCount { get; set; }

        public static int TearDownCount { get; set; }

        protected override void SetUp(Assembly testAssembly)
        {
            SetupCount++;
        }

        protected override void TearDown(Assembly testAssembly)
        {
            TearDownCount++;
        }
    }
}
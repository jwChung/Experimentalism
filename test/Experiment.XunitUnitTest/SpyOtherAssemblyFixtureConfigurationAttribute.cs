using System.Reflection;

namespace Jwc.Experiment.Xunit
{
    public class SpyOtherAssemblyFixtureConfigurationAttribute : AssemblyFixtureConfigurationAttribute
    {
        public static int SetupCount { get; set; }

        public static int TearDownCount { get; set; }

        public override void SetUp(Assembly testAssembly)
        {
            SetupCount++;
        }

        public override void TearDown(Assembly testAssembly)
        {
            TearDownCount++;
        }
    }
}
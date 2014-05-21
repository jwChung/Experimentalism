using System;
using System.Reflection;

namespace Jwc.Experiment.Xunit
{
    public static class TestMethodExtensions
    {
        public static void Execute(this MethodInfo testMethod)
        {
            var appDomain = AppDomain.CreateDomain(
                testMethod.Name,
                AppDomain.CurrentDomain.Evidence,
                AppDomain.CurrentDomain.SetupInformation);

            try
            {
                var runner = (TestRunner)appDomain.CreateInstanceAndUnwrap(
                    Assembly.GetExecutingAssembly().FullName,
                    typeof(TestRunner).FullName);

                runner.Run(testMethod);
            }
            finally
            {
                AppDomain.Unload(appDomain);
            }
        }
    }
}
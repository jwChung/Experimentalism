using System;
using System.Reflection;

namespace Jwc.Experiment.Xunit
{
    public static class TestMethodExtensions
    {
        public static void RunOnOtherDomain(this MethodInfo testMethod)
        {
            var appDomain = AppDomain.CreateDomain(
                testMethod.Name,
                AppDomain.CurrentDomain.Evidence,
                AppDomain.CurrentDomain.SetupInformation);

            try
            {
                var invoker = (TestInvoker)appDomain.CreateInstanceAndUnwrap(
                    Assembly.GetExecutingAssembly().FullName,
                    typeof(TestInvoker).FullName);

                invoker.Invoke(testMethod);
            }
            finally
            {
                AppDomain.Unload(appDomain);
            }
        }
    }
}
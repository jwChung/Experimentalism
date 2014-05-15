using System;
using System.Reflection;

namespace Jwc.Experiment.Xunit
{
    public static class TestMethodExtensions
    {
        public static void Execute(this MethodInfo testMethod)
        {
            var setup = new AppDomainSetup { ApplicationBase = Environment.CurrentDirectory };
            var appDomain = AppDomain.CreateDomain(testMethod.Name, null, setup);

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

        private class TestRunner : MarshalByRefObject
        {
            public void Run(MethodInfo testMethod)
            {
                var target = Activator.CreateInstance(testMethod.ReflectedType);
                testMethod.Invoke(target, new object[0]);
            }
        }
    }
}
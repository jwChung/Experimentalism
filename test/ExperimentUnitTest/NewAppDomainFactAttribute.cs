using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;
using Xunit.Sdk;

namespace Jwc.Experiment
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class NewAppDomainFactAttribute : FactAttribute
    {
        protected override IEnumerable<ITestCommand> EnumerateTestCommands(IMethodInfo method)
        {
            yield return new StaticFactCommand(method);
        }

        private class StaticFactCommand : TestCommand
        {
            private readonly IMethodInfo _method;

            public StaticFactCommand(IMethodInfo method)
                : base(method, MethodUtility.GetDisplayName(method), MethodUtility.GetTimeoutParameter(method))
            {
                _method = method;
            }

            public override bool ShouldCreateInstance
            {
                get
                {
                    return false;
                }
            }

            public override MethodResult Execute(object testClass)
            {
                var appDomain = AppDomain.CreateDomain(
                    testMethod.Name,
                    AppDomain.CurrentDomain.Evidence,
                    AppDomain.CurrentDomain.SetupInformation);

                try
                {
                    var invoker = (NewAppDomainFactInvoker)appDomain.CreateInstanceAndUnwrap(
                        Assembly.GetExecutingAssembly().FullName,
                        typeof(NewAppDomainFactInvoker).FullName);

                    invoker.Invoke(_method.MethodInfo);
                }
                finally
                {
                    AppDomain.Unload(appDomain);
                }

                return new PassedResult(testMethod, DisplayName);
            }
        }
    }
}
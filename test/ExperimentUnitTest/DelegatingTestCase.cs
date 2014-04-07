using System;
using System.Reflection;
using Xunit.Sdk;

namespace Jwc.Experiment
{
    public class DelegatingTestCase : ITestCase
    {
        public Func<IMethodInfo, Func<MethodInfo, ITestFixture>, ITestCommand> OnConvertToTestCommand
        {
            get;
            set;
        }

        public ITestCommand ConvertToTestCommand(IMethodInfo method, Func<MethodInfo, ITestFixture> fixtureFactory)
        {
            return OnConvertToTestCommand(method, fixtureFactory);
        }
    }
}
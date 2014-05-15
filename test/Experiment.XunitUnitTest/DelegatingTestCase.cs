using System;
using Xunit.Sdk;

namespace Jwc.Experiment.Xunit
{
    public class DelegatingTestCase : ITestCase
    {
        public Func<IMethodInfo, ITestFixtureFactory, ITestCommand> OnConvertToTestCommand
        {
            get;
            set;
        }

        public ITestCommand ConvertToTestCommand(IMethodInfo method, ITestFixtureFactory testFixtureFactory)
        {
            return OnConvertToTestCommand(method, testFixtureFactory);
        }
    }
}
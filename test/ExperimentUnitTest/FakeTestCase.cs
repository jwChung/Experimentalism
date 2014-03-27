using System;
using Xunit.Sdk;

namespace Jwc.Experiment
{
    public class FakeTestCase : ITestCase
    {
        public Func<IMethodInfo, ITestFixtureFactory, ITestCommand> OnConvertToTestCommand
        {
            get;
            set;
        }

        public ITestCommand ConvertToTestCommand(IMethodInfo method, ITestFixtureFactory fixtureFactory)
        {
            return OnConvertToTestCommand(method, fixtureFactory);
        }
    }
}
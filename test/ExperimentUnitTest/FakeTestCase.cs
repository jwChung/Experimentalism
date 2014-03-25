using System;
using Xunit.Sdk;

namespace Jwc.Experiment
{
    public class FakeTestCase : ITestCase
    {
        public Func<IMethodInfo, ITestFixture, ITestCommand> OnConvertToTestCommand
        {
            get;
            set;
        }

        public ITestCommand ConvertToTestCommand(IMethodInfo method, ITestFixture testFixture)
        {
            return OnConvertToTestCommand(method, testFixture);
        }
    }
}
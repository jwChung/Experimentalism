using System;
using Xunit.Sdk;

namespace Jwc.Experiment
{
    public class FakeTestCase : ITestCase
    {
        public Func<IMethodInfo, ITestCommand> OnConvertToTestCommand
        {
            get;
            set;
        }

        public ITestCommand ConvertToTestCommand(IMethodInfo method)
        {
            return OnConvertToTestCommand(method);
        }
    }
}
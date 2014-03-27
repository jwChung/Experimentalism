using System;
using System.Reflection;
using Xunit.Sdk;

namespace Jwc.Experiment
{
    public class FakeTestCase : ITestCase
    {
        public Func<IMethodInfo, ITestFixture, ITestCommand> OnConvertToTestCommand2
        {
            get;
            set;
        }

        public Func<IMethodInfo, Func<MethodInfo, ITestFixture>, ITestCommand> OnConvertToTestCommand
        {
            get;
            set;
        }

        public ITestCommand ConvertToTestCommand(IMethodInfo method, ITestFixture testFixture)
        {
            return OnConvertToTestCommand2(method, testFixture);
        }

        public ITestCommand ConvertToTestCommand(IMethodInfo method, Func<MethodInfo, ITestFixture> fixtureFactory)
        {
            return OnConvertToTestCommand(method, fixtureFactory);
        }
    }
}
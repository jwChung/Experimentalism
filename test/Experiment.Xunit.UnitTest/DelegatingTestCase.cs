namespace Jwc.Experiment.Xunit
{
    using System;
    using global::Xunit.Sdk;

    public class DelegatingTestCase : ITestCase
    {
        public Func<IMethodInfo, ITestFixtureFactory, ITestCommand> OnConvertToTestCommand { get; set; }

        public ITestCommand ConvertToTestCommand(IMethodInfo method, ITestFixtureFactory testFixtureFactory)
        {
            return this.OnConvertToTestCommand(method, testFixtureFactory);
        }
    }
}
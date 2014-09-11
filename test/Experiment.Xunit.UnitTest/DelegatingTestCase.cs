namespace Jwc.Experiment.Xunit
{
    using System;
    using global::Xunit.Sdk;
    using ITestFixtureFactory2 = Experiment.ITestFixtureFactory;

    public class DelegatingTestCase : ITestCase
    {
        public Func<IMethodInfo, ITestFixtureFactory2, ITestCommand> OnConvertToTestCommand { get; set; }

        public ITestCommand ConvertToTestCommand(IMethodInfo method, ITestFixtureFactory2 testFixtureFactory)
        {
            return this.OnConvertToTestCommand(method, testFixtureFactory);
        }
    }
}
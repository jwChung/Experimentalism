namespace Jwc.Experiment
{
    using System;
    using System.Reflection;

    public class DelegatingTestFixtureFactory : ITestFixtureFactory
    {
        public Func<MethodInfo, ITestFixture> OnCreate { get; set; }

        public ITestFixture Create(MethodInfo testMethod)
        {
            return this.OnCreate(testMethod);
        }
    }
}
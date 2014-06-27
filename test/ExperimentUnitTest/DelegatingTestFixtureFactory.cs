using System;
using System.Reflection;

namespace Jwc.Experiment
{
    public class DelegatingTestFixtureFactory : ITestFixtureFactory
    {
        public Func<MethodInfo, ITestFixture> OnCreate { get; set; }

        public ITestFixture Create(MethodInfo testMethod)
        {
            return this.OnCreate(testMethod);
        }
    }
}
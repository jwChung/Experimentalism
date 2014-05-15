using System;
using System.Reflection;

namespace Jwc.Experiment.Xunit
{
    public class DelegatingTestFixtureFactory : ITestFixtureFactory
    {
        public Func<MethodInfo, ITestFixture> OnCreate
        {
            get;
            set;
        }

        public ITestFixture Create(MethodInfo testMethod)
        {
            return OnCreate(testMethod);
        }
    }
}
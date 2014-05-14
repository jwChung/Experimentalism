using System;
using System.Reflection;

namespace Jwc.Experiment
{
    public class DelegatingStaticTestFixtureFactory : ITestFixtureFactory
    {
        public DelegatingStaticTestFixtureFactory()
        {
            ConstructCount++;
        }

        public static int ConstructCount
        {
            get;
            set;
        }

        public static Func<MethodInfo, ITestFixture> OnCreate
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
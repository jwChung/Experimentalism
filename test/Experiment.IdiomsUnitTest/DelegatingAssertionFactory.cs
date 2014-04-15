using System;
using Ploeh.Albedo;

namespace Jwc.Experiment.Idioms
{
    public class DelegatingAssertionFactory : IAssertionFactory
    {
        public Func<ITestFixture, IReflectionVisitor<object>> OnCreate
        {
            get;
            set;
        }

        public IReflectionVisitor<object> Create(ITestFixture testFixture)
        {
            return OnCreate(testFixture);
        }
    }
}
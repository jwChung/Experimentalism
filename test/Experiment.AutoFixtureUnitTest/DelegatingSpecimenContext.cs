using System;
using Ploeh.AutoFixture.Kernel;

namespace Jwc.Experiment
{
    public class DelegatingSpecimenContext : ISpecimenContext
    {
        public Func<object, object> OnResolve
        {
            get;
            set;
        }

        public object Resolve(object request)
        {
            return OnResolve(request);
        }
    }
}
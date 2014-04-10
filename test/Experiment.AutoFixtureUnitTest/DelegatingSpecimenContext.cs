using System;
using Ploeh.AutoFixture.Kernel;

namespace NuGet.Jwc.Experiment
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
using System;
using Ploeh.AutoFixture.Kernel;

namespace Jwc.Experiment
{
    public class ClassImplementingMultiple : IDisposable, ISpecimenContext
    {
        public void Dispose()
        {
            throw new NotSupportedException();
        }

        public object Resolve(object request)
        {
            throw new NotSupportedException();
        }
    }
}
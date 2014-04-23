using System;
using Ploeh.AutoFixture.Kernel;

namespace Jwc.Experiment
{
    public class TypeImplementingMultiple : IDisposable, ISpecimenContext
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
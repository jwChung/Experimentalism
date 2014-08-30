namespace Jwc.Experiment
{
    using System;
    using Ploeh.AutoFixture.Kernel;

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
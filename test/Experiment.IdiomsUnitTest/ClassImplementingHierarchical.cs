using System;
using Ploeh.Albedo;

namespace Jwc.Experiment
{
    public class ClassImplementingHierarchical : IDisposable, IHierarchical
    {
        public void Dispose()
        {
            throw new NotSupportedException();
        }

        public object Resolve(object request)
        {
            throw new NotSupportedException();
        }

        public IReflectionVisitor<T> Accept<T>(IReflectionVisitor<T> visitor)
        {
            throw new NotSupportedException();
        }
    }
}
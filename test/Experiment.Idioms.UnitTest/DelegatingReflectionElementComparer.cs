using System;
using System.Collections.Generic;
using Ploeh.Albedo;

namespace Jwc.Experiment
{
    public class DelegatingReflectionElementComparer : IEqualityComparer<IReflectionElement>
    {
        public Func<IReflectionElement, IReflectionElement, bool> OnEquals { get; set; }

        public bool Equals(IReflectionElement x, IReflectionElement y)
        {
            return this.OnEquals(x, y);
        }

        public int GetHashCode(IReflectionElement obj)
        {
            return obj.GetHashCode();
        }
    }
}
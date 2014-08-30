namespace Jwc.Experiment
{
    using System;
    using System.Collections.Generic;
    using Ploeh.Albedo;

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
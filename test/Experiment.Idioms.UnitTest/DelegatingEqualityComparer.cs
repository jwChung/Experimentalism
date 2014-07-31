using System;
using System.Collections.Generic;

namespace Jwc.Experiment
{
    public class DelegatingEqualityComparer<T> : IEqualityComparer<T>
    {
        public Func<T, T, bool> OnEquals { get; set; }

        public Func<T, int> OnGetHashCode { get; set; }

        public bool Equals(T x, T y)
        {
            return this.OnEquals(x, y);
        }

        public int GetHashCode(T obj)
        {
            return this.OnGetHashCode(obj);
        }
    }
}
using System;
using System.Collections.Generic;

namespace Jwc.Experiment.Idioms
{
    public class DelegatingEqualityComparer<T> : IEqualityComparer<T>
    {
        public Func<T, T, bool> OnEquals
        {
            get;
            set;
        }

        public Func<T, int> OnGetHashCode
        {
            get;
            set;
        }

        public bool Equals(T x, T y)
        {
            return OnEquals(x, y);
        }

        public int GetHashCode(T obj)
        {
            return OnGetHashCode(obj);
        }
    }
}
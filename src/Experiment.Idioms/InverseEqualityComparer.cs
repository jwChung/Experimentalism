using System;
using System.Collections.Generic;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Decorates <see cref="IEqualityComparer{T}" /> to inverse the order of
    /// compared arguments
    /// </summary>
    /// <typeparam name="T">
    /// The type of the compared arguments.
    /// </typeparam>
    public class InverseEqualityComparer<T> : IEqualityComparer<T>
    {
        private readonly IEqualityComparer<T> _equalityComparer;

        /// <summary>
        /// Initializes a new instance of the <see cref="InverseEqualityComparer{T}"/> class.
        /// </summary>
        /// <param name="equalityComparer">
        /// The equality comparer to be evaluated reversely.
        /// </param>
        public InverseEqualityComparer(IEqualityComparer<T> equalityComparer)
        {
            if (equalityComparer == null)
            {
                throw new ArgumentNullException("equalityComparer");
            }

            _equalityComparer = equalityComparer;
        }

        /// <summary>
        /// Gets a indicating the equality comparer.
        /// </summary>
        public IEqualityComparer<T> EqualityComparer
        {
            get
            {
                return _equalityComparer;
            }
        }

        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <returns>
        /// true if the specified objects are equal; otherwise, false.
        /// </returns>
        /// <param name="x">
        /// The first object of type <paramref name="x" /> to compare.
        /// </param>
        /// <param name="y">
        /// The second object of type <paramref name="y" /> to compare.
        /// </param>
        public bool Equals(T x, T y)
        {
            return EqualityComparer.Equals(y, x);
        }

        /// <summary>
        /// Returns a hash code for the specified object.
        /// </summary>
        /// <returns>
        /// A hash code for the specified object.
        /// </returns>
        /// <param name="obj">
        /// The <see cref="object" /> for which a hash code is to be returned.
        /// </param>
        public int GetHashCode(T obj)
        {
            return EqualityComparer.GetHashCode(obj);
        }
    }
}
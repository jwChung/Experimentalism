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
        /// <summary>
        /// Initializes a new instance of the <see cref="InverseEqualityComparer{T}"/> class.
        /// </summary>
        /// <param name="equalityComparer">
        /// The equality comparer.
        /// </param>
        public InverseEqualityComparer(IEqualityComparer<T> equalityComparer)
        {
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
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }
    }
}
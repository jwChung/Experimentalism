using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Jwc.Experiment
{
    /// <summary>
    /// Represents logical or operator for <see cref="IEqualityComparer{T}" />
    /// </summary>
    /// <typeparam name="T">
    /// The compared type.
    /// </typeparam>
    public class OrEqualityComparer<T> : IEqualityComparer<T>
    {
        private readonly IEqualityComparer<T>[] _equalityComparers;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrEqualityComparer{T}" /> class.
        /// </summary>
        /// <param name="equalityComparers">
        /// The equality comparers.
        /// </param>
        public OrEqualityComparer(params IEqualityComparer<T>[] equalityComparers)
        {
            if (equalityComparers == null)
            {
                throw new ArgumentNullException("equalityComparers");
            }

            _equalityComparers = equalityComparers;
        }

        /// <summary>
        /// Gets a value indicating the equality comparers.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "The nested generic signature is desirable.")]
        public IEnumerable<IEqualityComparer<T>> EqualityComparers
        {
            get
            {
                return _equalityComparers;
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
            return EqualityComparers.Any(e => e.Equals(x, y));
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
            return 0;
        }
    }
}
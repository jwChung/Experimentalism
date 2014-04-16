using System.Collections.Generic;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represent <see cref="IEqualityComparer{T}" /> always returning a constant
    /// boolean.
    /// </summary>
    /// <typeparam name="T">The compared type.</typeparam>
    public class ConstantEqualityComparer<T> : IEqualityComparer<T>
    {
        private readonly bool _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstantEqualityComparer{T}"/> class.
        /// </summary>
        /// <param name="value">The constant value.</param>
        public ConstantEqualityComparer(bool value)
        {
            _value = value;
        }

        /// <summary>
        /// Gets a value indicating the constant.
        /// </summary>
        public bool Value
        {
            get
            {
                return _value;
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
            return Value;
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
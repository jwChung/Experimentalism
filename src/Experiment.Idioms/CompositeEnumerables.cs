using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents composite collection of test cases.
    /// </summary>
    /// <typeparam name="T">The type of objects to enumerate.</typeparam>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "The main responsibility of this class isn't to be a 'collection' (which, by the way, it isn't - it's just an Iterator).")]
    public class CompositeEnumerables<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T>[] _itemSet;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeEnumerables{T}"/> class.
        /// </summary>
        /// <param name="itemSet">The item set.</param>
        public CompositeEnumerables(params IEnumerable<T>[] itemSet)
        {
            if (itemSet == null)
            {
                throw new ArgumentNullException("itemSet");
            }

            _itemSet = itemSet;
        }

        /// <summary>
        /// Gets a value indicating the item set.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "The nested generic signature is desirable.")]
        public IEnumerable<IEnumerable<T>> ItemSet
        {
            get
            {
                return _itemSet;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="IEnumerator{T}" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            return ItemSet.SelectMany(items => items).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
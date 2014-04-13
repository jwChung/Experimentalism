using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents filter to exclude read-only properties.
    /// </summary>
    public class ExcludingReadOnlyProperties : IEnumerable<MemberInfo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExcludingReadOnlyProperties"/> class.
        /// </summary>
        /// <param name="targetMembers">
        /// The target members to be excluded.
        /// </param>
        public ExcludingReadOnlyProperties(IEnumerable<MemberInfo> targetMembers)
        {
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="IEnumerator{T}" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<MemberInfo> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
}
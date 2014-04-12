using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents filter to except certain members from given members.
    /// </summary>
    public class FilteringMembers : IEnumerable<MemberInfo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilteringMembers"/> class.
        /// </summary>
        /// <param name="targetMembers">The target members to be excepted.</param>
        /// <param name="exceptedMembers">The excepted members.</param>
        public FilteringMembers(
            IEnumerable<MemberInfo> targetMembers,
            params MemberInfo[] exceptedMembers)
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
            return GetEnumerator();
        }
    }
}
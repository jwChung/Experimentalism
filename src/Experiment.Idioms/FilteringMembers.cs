using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents filter to exclude certain members from given members.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "The main responsibility of this class isn't to be a 'collection' (which, by the way, it isn't - it's just an Iterator).")]
    public class FilteringMembers : IEnumerable<MemberInfo>
    {
        private readonly IEnumerable<MemberInfo> _targetMembers;
        private readonly Func<MemberInfo, bool> _condition;

        /// <summary>
        /// Initializes a new instance of the <see cref="FilteringMembers"/> class.
        /// </summary>
        /// <param name="targetMembers">The target members to be filtered.</param>
        /// <param name="condition">The condition to filter.</param>
        public FilteringMembers(
            IEnumerable<MemberInfo> targetMembers,
            Func<MemberInfo, bool> condition)
        {
            if (targetMembers == null)
            {
                throw new ArgumentNullException("targetMembers");
            }

            if (condition == null)
            {
                throw new ArgumentNullException("condition");
            }

            _targetMembers = targetMembers;
            _condition = condition;
        }

        /// <summary>
        /// Gets the target members.
        /// </summary>
        public IEnumerable<MemberInfo> TargetMembers
        {
            get
            {
                return _targetMembers;
            }
        }

        /// <summary>
        /// Gets the condition to filter the target members.
        /// </summary>
        /// <value>
        /// The condition.
        /// </value>
        public Func<MemberInfo, bool> Condition
        {
            get
            {
                return _condition;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="IEnumerator{T}" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<MemberInfo> GetEnumerator()
        {
            return TargetMembers.Where(m => !Condition(m)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
using System;
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
        private readonly IEnumerable<MemberInfo> _targetMembers;
        private readonly MemberInfo[] _exceptedMembers;

        /// <summary>
        /// Initializes a new instance of the <see cref="FilteringMembers"/> class.
        /// </summary>
        /// <param name="targetMembers">The target members to be excepted.</param>
        /// <param name="exceptedMembers">The excepted members.</param>
        public FilteringMembers(
            IEnumerable<MemberInfo> targetMembers,
            params MemberInfo[] exceptedMembers)
        {
            if (targetMembers == null)
            {
                throw new ArgumentNullException("targetMembers");
            }

            if (exceptedMembers == null)
            {
                throw new ArgumentNullException("exceptedMembers");
            }

            _targetMembers = targetMembers;
            _exceptedMembers = exceptedMembers;
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
        /// Gets the excepted members.
        /// </summary>
        public IEnumerable<MemberInfo> ExceptedMembers
        {
            get
            {
                return _exceptedMembers;
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
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
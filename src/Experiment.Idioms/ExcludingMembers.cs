﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents filter to exclude specified members.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "The main responsibility of this class isn't to be a 'collection' (which, by the way, it isn't - it's just an Iterator).")]
    public class ExcludingMembers : IEnumerable<MemberInfo>
    {
        private readonly IEnumerable<MemberInfo> _targetMembers;
        private readonly IEnumerable<MemberInfo> _excludedMembers;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExcludingMembers"/> class.
        /// </summary>
        /// <param name="targetMembers">
        /// The target members to be excluded.
        /// </param>
        /// <param name="excludedMembers">
        /// The excluded members.
        /// </param>
        public ExcludingMembers(
            IEnumerable<MemberInfo> targetMembers,
            IEnumerable<MemberInfo> excludedMembers)
        {
            if (targetMembers == null)
            {
                throw new ArgumentNullException("targetMembers");
            }

            if (excludedMembers == null)
            {
                throw new ArgumentNullException("excludedMembers");
            }

            _targetMembers = targetMembers;
            _excludedMembers = excludedMembers;
        }

        /// <summary>
        /// Gets the excluded members.
        /// </summary>
        public IEnumerable<MemberInfo> ExcludedMembers
        {
            get
            {
                return _excludedMembers;
            }
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
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="IEnumerator{T}" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<MemberInfo> GetEnumerator()
        {
            return TargetMembers.Except(ExcludedMembers).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
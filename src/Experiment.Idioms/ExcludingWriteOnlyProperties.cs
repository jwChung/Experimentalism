using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents filter to exclude write-only properties.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "The main responsibility of this class isn't to be a 'collection' (which, by the way, it isn't - it's just an Iterator).")]
    public class ExcludingWriteOnlyProperties : IEnumerable<MemberInfo>
    {
        private readonly IEnumerable<MemberInfo> _targetMembers;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExcludingWriteOnlyProperties"/> class.
        /// </summary>
        /// <param name="targetMembers">
        /// The target members to be excluded.
        /// </param>
        public ExcludingWriteOnlyProperties(IEnumerable<MemberInfo> targetMembers)
        {
            if (targetMembers == null)
            {
                throw new ArgumentNullException("targetMembers");
            }

            _targetMembers = targetMembers;
        }

        /// <summary>
        /// Gets a value indicating the target members.
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
            return TargetMembers.Where(
                m =>
                {
                    var property = m as PropertyInfo;
                    return property == null || property.GetGetMethod() != null;
                })
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
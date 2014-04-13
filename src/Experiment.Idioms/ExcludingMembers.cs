using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents filter to exclude specified members.
    /// </summary>
    public class ExcludingMembers : IEnumerable<MemberInfo>
    {
        private readonly IEnumerable<MemberInfo> _excludedMembers;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExcludingMembers"/> class.
        /// </summary>
        /// <param name="excludedMembers">
        /// The excluded members.
        /// </param>
        public ExcludingMembers(IEnumerable<MemberInfo> excludedMembers)
        {
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
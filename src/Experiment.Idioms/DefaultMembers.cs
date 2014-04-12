using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents default members of a certain <see cref="Type"/> which need
    /// to verify idiomatic assertions.
    /// </summary>
    public class DefaultMembers : IEnumerable<MemberInfo>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultMembers"/> class.
        /// </summary>
        /// <param name="type">The target type.</param>
        public DefaultMembers(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that
        /// can be used to iterate through the collection.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IEnumerator<MemberInfo> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
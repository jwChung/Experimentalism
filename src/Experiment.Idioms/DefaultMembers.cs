using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents default members of a certain <see cref="Type"/> which need
    /// to verify idiomatic assertions.
    /// </summary>
    public class DefaultMembers : IEnumerable<MemberInfo>
    {
        private readonly Type _type;

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

            _type = type;
        }

        /// <summary>
        /// Gets the target type.
        /// </summary>
        public Type Type
        {
            get
            {
                return _type;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that
        /// can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<MemberInfo> GetEnumerator()
        {
            const BindingFlags bindingFlags =
                BindingFlags.Public | BindingFlags.DeclaredOnly |
                BindingFlags.Instance | BindingFlags.Static;

            var accessors = Type.GetProperties(bindingFlags).SelectMany(p => p.GetAccessors());
            return Type.GetMembers(bindingFlags).Except(accessors).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
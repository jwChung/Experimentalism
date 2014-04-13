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
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "The main responsibility of this class isn't to be a 'collection' (which, by the way, it isn't - it's just an Iterator).")]
    public class TargetMembers : IEnumerable<MemberInfo>
    {
        private readonly Type _type;

        /// <summary>
        /// Initializes a new instance of the <see cref="TargetMembers"/> class.
        /// </summary>
        /// <param name="type">The target type.</param>
        public TargetMembers(Type type)
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification="This name is desirable to indicate the target type.")]
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
            var eventMethods = Type.GetEvents(bindingFlags).SelectMany(
                e => new[] { e.GetAddMethod(), e.GetRemoveMethod() });
            return Type.GetMembers(bindingFlags)
                .Except(accessors)
                .Except(eventMethods)
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
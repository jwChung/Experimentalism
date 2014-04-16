using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ploeh.Albedo.Refraction;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents default members of a certain <see cref="Type"/> which need
    /// to verify idiomatic assertions.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "The main responsibility of this class isn't to be a 'collection' (which, by the way, it isn't - it's just an Iterator).")]
    public class TypeMembers : IEnumerable<MemberInfo>
    {
        private readonly Accessibilities _accessibilities;
        private readonly Type _type;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeMembers"/> class.
        /// </summary>
        /// <param name="type">The target type.</param>
        public TypeMembers(Type type) : this(type, Accessibilities.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeMembers"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="accessibilities">The accessibilities.</param>
        public TypeMembers(Type type, Accessibilities accessibilities)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            _type = type;
            _accessibilities = accessibilities;
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
        /// Gets the accessibilities.
        /// </summary>
        public Accessibilities Accessibilities
        {
            get
            {
                return _accessibilities;
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
                BindingFlags.Public | BindingFlags.NonPublic |
                BindingFlags.Instance | BindingFlags.Static |
                BindingFlags.DeclaredOnly;

            var accessors = Type.GetProperties(bindingFlags).SelectMany(p => p.GetAccessors(true));
            var eventMethods = Type.GetEvents(bindingFlags).SelectMany(
                e => new[] { e.GetAddMethod(true), e.GetRemoveMethod(true) });

            return Type.GetMembers(bindingFlags)
                .Except(accessors)
                .Except(eventMethods)
                .Where(IsSatisfiedWithAccessibilities)
                .GetEnumerator();
        }

        private bool IsSatisfiedWithAccessibilities(MemberInfo member)
        {
            var accessibilities = member.ToReflectionElement()
                .Accept(new AccessibilityCollectingVisitor())
                .Value.Single();

            return (Accessibilities & accessibilities) != Accessibilities.None;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ploeh.Albedo.Refraction;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents members of a certain <see cref="Type"/> which will be
    /// verified by idiomatic assertions.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "The main responsibility of this class isn't to be a 'collection' (which, by the way, it isn't - it's just an Iterator).")]
    public class IdiomaticMembers : IEnumerable<MemberInfo>
    {
        private const BindingFlags _bindingFlags =
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly |
            BindingFlags.Static | BindingFlags.Instance;

        private static readonly MemberKindCollector _memberKindCollector = new MemberKindCollector();
        private readonly AccessibilityCollector _accessibilityCollector = new AccessibilityCollector();

        private readonly Type _type;
        private readonly MemberKinds _memberKinds;
        private readonly Accessibilities _accessibilities;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdiomaticMembers"/> class.
        /// </summary>
        /// <param name="type">A type to enumerate members.</param>
        public IdiomaticMembers(Type type)
            : this(type, MemberKinds.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdiomaticMembers"/> class.
        /// </summary>
        /// <param name="type">A type to enumerate members.</param>
        /// <param name="memberKinds">Member kinds to filter members.</param>
        public IdiomaticMembers(Type type, MemberKinds memberKinds)
            : this(type, memberKinds, Accessibilities.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdiomaticMembers" /> class.
        /// </summary>
        /// <param name="type">A type to enumerate members.</param>
        /// <param name="memberKinds">Member kinds to filter members.</param>
        /// <param name="accessibilities">The accessibilities to filter members.</param>
        public IdiomaticMembers(Type type, MemberKinds memberKinds, Accessibilities accessibilities)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            _type = type;
            _memberKinds = memberKinds;
            _accessibilities = accessibilities;
        }

        /// <summary>
        /// Gets a value indicating the type to enumerate members.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "This name is desirable to indicate the target type.")]
        public Type Type
        {
            get
            {
                return _type;
            }
        }

        /// <summary>
        /// Gets a value indicating the accessibilities.
        /// </summary>
        public Accessibilities Accessibilities
        {
            get
            {
                return _accessibilities;
            }
        }

        /// <summary>
        /// Gets a value indicating the member kinds to be enumerated.
        /// </summary>
        public MemberKinds MemberKinds
        {
            get
            {
                return _memberKinds;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can
        /// be used to iterate through the collection.
        /// </returns>
        public IEnumerator<MemberInfo> GetEnumerator()
        {
            return Type.GetMembers(_bindingFlags)
                .Except(GetAccessors())
                .Except(GetEventMethods())
                .Where(IsSpecifiedMemberKind)
                .Where(IsSpecifiedAccessibilites)
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private IEnumerable<MethodInfo> GetAccessors()
        {
            return Type.GetProperties(_bindingFlags).SelectMany(p => p.GetAccessors(true));
        }

        private IEnumerable<MethodInfo> GetEventMethods()
        {
            return Type.GetEvents(_bindingFlags).SelectMany(
                e => new[] { e.GetAddMethod(true), e.GetRemoveMethod(true) });
        }

        private bool IsSpecifiedMemberKind(MemberInfo member)
        {
            return (GetMemberKinds(member) & MemberKinds) != MemberKinds.None;
        }

        private bool IsSpecifiedAccessibilites(MemberInfo member)
        {
            return (GetAccessibilities(member) & Accessibilities) != Accessibilities.None;
        }

        private Accessibilities GetAccessibilities(MemberInfo member)
        {
            return member.ToReflectionElement().Accept(_accessibilityCollector).Value.Single();
        }

        private static MemberKinds GetMemberKinds(MemberInfo member)
        {
            return member.ToReflectionElement().Accept(_memberKindCollector).Value.Single();
        }
    }
}
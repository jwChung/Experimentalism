using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Ploeh.Albedo.Refraction;

namespace Jwc.Experiment
{
    /// <summary>
    /// Represents members of a certain <see cref="Type" /> which will be verified by idiomatic
    /// assertions.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "The main responsibility of this class isn't to be a 'collection' (which, by the way, it isn't - it's just an Iterator).")]
    public class IdiomaticMembers : IEnumerable<MemberInfo>
    {
        private const BindingFlags Bindings =
            BindingFlags.Public | BindingFlags.DeclaredOnly |
            BindingFlags.Static | BindingFlags.Instance;

        private static readonly MemberKindCollector MemberKindCollector = new MemberKindCollector();

        private readonly Type type;
        private readonly MemberKinds memberKinds;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdiomaticMembers" /> class.
        /// </summary>
        /// <param name="type">
        /// A type to enumerate members.
        /// </param>
        public IdiomaticMembers(Type type) : this(type, MemberKinds.Default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdiomaticMembers" /> class.
        /// </summary>
        /// <param name="type">
        /// A type to enumerate members.
        /// </param>
        /// <param name="memberKinds">
        /// Member kinds to filter members.
        /// </param>
        public IdiomaticMembers(
            Type type,
            MemberKinds memberKinds)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            this.type = type;
            this.memberKinds = memberKinds;
        }

        /// <summary>
        /// Gets a value indicating the type to enumerate members.
        /// </summary>
        [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "This name is desirable to indicate the target type.")]
        public Type Type
        {
            get
            {
                return this.type;
            }
        }

        /// <summary>
        /// Gets a value indicating the member kinds to be enumerated.
        /// </summary>
        public MemberKinds MemberKinds
        {
            get
            {
                return this.memberKinds;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to
        /// iterate through the collection.
        /// </returns>
        public IEnumerator<MemberInfo> GetEnumerator()
        {
            return Type.GetMembers(Bindings)
                .Except(this.GetAccessors())
                .Except(this.GetEventMethods())
                .Where(m => !(m is Type))
                .Where(this.IsSpecifiedByMemberKind)
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private static MemberKinds GetMemberKinds(MemberInfo member)
        {
            return member.ToReflectionElement().Accept(MemberKindCollector).Value.Single();
        }

        private IEnumerable<MethodInfo> GetAccessors()
        {
            return Type.GetProperties(Bindings).SelectMany(p => p.GetAccessors(true));
        }

        private IEnumerable<MethodInfo> GetEventMethods()
        {
            return Type.GetEvents(Bindings).SelectMany(
                e => new[] { e.GetAddMethod(true), e.GetRemoveMethod(true) });
        }

        private bool IsSpecifiedByMemberKind(MemberInfo member)
        {
            return (GetMemberKinds(member) & MemberKinds) != MemberKinds.None;
        }
    }
}
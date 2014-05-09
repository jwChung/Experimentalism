using System;
using System.Collections.Generic;
using System.Reflection;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Defines idiomatic extension methods.
    /// </summary>
    public static class IdiomaticExtensions
    {
        /// <summary>
        /// Converts a type to idiomatic members.
        /// </summary>
        /// <param name="type">
        /// A type to enumerate the idiomatic members.
        /// </param>
        /// <returns>
        /// The idiomatic members.
        /// </returns>
        public static IEnumerable<MemberInfo> ToIdiomaticMembers(this Type type)
        {
            return ToIdiomaticMembers(type, MemberKinds.All);
        }

        /// <summary>
        /// Converts a type to idiomatic members corresponding to member kinds.
        /// </summary>
        /// <param name="type">
        /// A type to enumerate the idiomatic members.
        /// </param>
        /// <param name="memberKinds">
        /// The member kinds.
        /// </param>
        /// <returns>
        /// The idiomatic members.
        /// </returns>
        public static IEnumerable<MemberInfo> ToIdiomaticMembers(this Type type, MemberKinds memberKinds)
        {
            return new IdiomaticMembers(type, memberKinds);
        }

        /// <summary>
        /// Converts a type to idiomatic instance members.
        /// </summary>
        /// <param name="type">
        /// A type to enumerate the idiomatic instance members.
        /// </param>
        /// <returns>
        /// The idiomatic instance members.
        /// </returns>
        public static IEnumerable<MemberInfo> ToIdiomaticInstanceMembers(this Type type)
        {
            return ToIdiomaticInstanceMembers(type, MemberKinds.All);
        }

        /// <summary>
        /// Converts a type to idiomatic instance members corresponding to
        /// member kinds.
        /// </summary>
        /// <param name="type">
        /// A type to enumerate the idiomatic instance members.
        /// </param>
        /// <param name="memberKinds">
        /// The instance member kinds.
        /// </param>
        /// <returns>
        /// The idiomatic instance members.
        /// </returns>
        public static IEnumerable<MemberInfo> ToIdiomaticInstanceMembers(this Type type, MemberKinds memberKinds)
        {
            return new IdiomaticMembers(
                type,
                memberKinds,
                bindingFlags: IdiomaticMembers.DefaultBindingFlags & ~BindingFlags.Static);
        }

        /// <summary>
        /// Converts a type to idiomatic static members.
        /// </summary>
        /// <param name="type">
        /// A type to enumerate the idiomatic static members.
        /// </param>
        /// <returns>
        /// The idiomatic static members.
        /// </returns>
        public static IEnumerable<MemberInfo> ToIdiomaticStaticMembers(this Type type)
        {
            return ToIdiomaticStaticMembers(type, MemberKinds.All);
        }

        /// <summary>
        /// Converts a type to idiomatic static members corresponding to member
        /// kinds.
        /// </summary>
        /// <param name="type">
        /// A type to enumerate the idiomatic static members.
        /// </param>
        /// <param name="memberKinds">
        /// The static member kinds.
        /// </param>
        /// <returns>
        /// The idiomatic static members.
        /// </returns>
        public static IEnumerable<MemberInfo> ToIdiomaticStaticMembers(this Type type, MemberKinds memberKinds)
        {
            return new IdiomaticMembers(
                type,
                memberKinds,
                bindingFlags: IdiomaticMembers.DefaultBindingFlags & ~BindingFlags.Instance);
        }
    }
}
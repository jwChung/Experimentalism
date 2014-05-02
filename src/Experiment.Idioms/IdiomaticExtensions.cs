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
        /// Converts a type to idiomatic members corresponding to member kinds.
        /// </summary>
        /// <param name="type">
        /// A type to enumerate the idiomatic members.
        /// </param>
        /// <returns>
        /// The idiomatic members.
        /// </returns>
        public static IEnumerable<MemberInfo> ToMembers(this Type type)
        {
            return ToMembers(type, MemberKinds.All);
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
        public static IEnumerable<MemberInfo> ToMembers(this Type type, MemberKinds memberKinds)
        {
            return new TypeMembers(type, memberKinds);
        }

        /// <summary>
        /// Converts a type to idiomatic instance members corresponding to member
        /// kinds.
        /// </summary>
        /// <param name="type">
        /// A type to enumerate the idiomatic instance members.
        /// </param>
        /// <returns>
        /// The idiomatic instance members.
        /// </returns>
        public static IEnumerable<MemberInfo> ToInstanceMembers(this Type type)
        {
            return ToInstanceMembers(type, MemberKinds.All);
        }
        
        /// <summary>
        /// Converts a type to idiomatic instance members corresponding to member
        /// kinds.
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
        public static IEnumerable<MemberInfo> ToInstanceMembers(this Type type, MemberKinds memberKinds)
        {
            return new TypeMembers(
                type,
                memberKinds,
                bindingFlags: TypeMembers.DefaultBindingFlags & ~BindingFlags.Static);
        }

        /// <summary>
        /// Converts a type to idiomatic static members corresponding to member
        /// kinds.
        /// </summary>
        /// <param name="type">
        /// A type to enumerate the idiomatic static members.
        /// </param>
        /// <returns>
        /// The idiomatic static members.
        /// </returns>
        public static IEnumerable<MemberInfo> ToStaticMembers(this Type type)
        {
            return ToStaticMembers(type, MemberKinds.All);
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
        public static IEnumerable<MemberInfo> ToStaticMembers(this Type type, MemberKinds memberKinds)
        {
            return new TypeMembers(
                type,
                memberKinds,
                bindingFlags: TypeMembers.DefaultBindingFlags & ~BindingFlags.Instance);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Jwc.Experiment
{
    /// <summary>
    ///     Defines idiomatic extension methods.
    /// </summary>
    public static class IdiomaticExtensions
    {
        /// <summary>
        ///     Gets idiomatic members of a type.
        /// </summary>
        /// <param name="type">
        ///     A type to enumerate the idiomatic members.
        /// </param>
        /// <returns>
        ///     The idiomatic members.
        /// </returns>
        public static IEnumerable<MemberInfo> GetIdiomaticMembers(this Type type)
        {
            return new IdiomaticMembers(type);
        }

        /// <summary>
        ///     Gets idiomatic members of a type corresponding to member kinds.
        /// </summary>
        /// <param name="type">
        ///     A type to enumerate the idiomatic members.
        /// </param>
        /// <param name="memberKinds">
        ///     The member kinds.
        /// </param>
        /// <returns>
        ///     The idiomatic members.
        /// </returns>
        public static IEnumerable<MemberInfo> GetIdiomaticMembers(this Type type, MemberKinds memberKinds)
        {
            return new IdiomaticMembers(type, memberKinds);
        }
    }
}
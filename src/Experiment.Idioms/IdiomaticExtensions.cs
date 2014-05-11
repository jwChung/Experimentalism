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
            return new IdiomaticMembers(type);
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
    }
}
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Defines idiomatic extension methods.
    /// </summary>
    [CLSCompliant(false)] 
    public static class IdiomaticExtensions
    {
        /// <summary>
        /// Gets idiomatic members corresponding to member kinds.
        /// </summary>
        /// <param name="type">A type to enumerate the idiomatic members.</param>
        /// <returns>The idiomatic members.</returns>
        public static IEnumerable<MemberInfo> GetIdiomaticMembers(
            this Type type)
        {
            return GetIdiomaticMembers(type, MemberKinds.All);
        }

        /// <summary>
        /// Gets idiomatic members corresponding to member kinds.
        /// </summary>
        /// <param name="type">A type to enumerate the idiomatic members.</param>
        /// <param name="memberKinds">The member kinds.</param>
        /// <returns>The idiomatic members.</returns>
        public static IEnumerable<MemberInfo> GetIdiomaticMembers(
            this Type type,
            MemberKinds memberKinds)
        {
            return new TypeMembers(type, memberKinds);
        }

        /// <summary>
        /// Gets idiomatic instance members corresponding to member kinds.
        /// </summary>
        /// <param name="type">A type to enumerate the idiomatic instance members.</param>
        /// <returns>The idiomatic instance members.</returns>
        public static IEnumerable<MemberInfo> GetIdiomaticInstanceMembers(this Type type)
        {
            return GetIdiomaticInstanceMembers(type, MemberKinds.All);
        }
        
        /// <summary>
        /// Gets idiomatic instance members corresponding to member kinds.
        /// </summary>
        /// <param name="type">A type to enumerate the idiomatic instance members.</param>
        /// <param name="memberKinds">The instance member kinds.</param>
        /// <returns>The idiomatic instance members.</returns>
        public static IEnumerable<MemberInfo> GetIdiomaticInstanceMembers(
            this Type type,
            MemberKinds memberKinds)
        {
            return new TypeMembers(
                type,
                memberKinds,
                bindingFlags: TypeMembers.DefaultBindingFlags & ~BindingFlags.Static);
        }

        /// <summary>
        /// Gets idiomatic static members corresponding to member kinds.
        /// </summary>
        /// <param name="type">A type to enumerate the idiomatic static members.</param>
        /// <returns>The idiomatic static members.</returns>
        public static IEnumerable<MemberInfo> GetIdiomaticStaticMembers(
            this Type type)
        {
            return GetIdiomaticStaticMembers(type, MemberKinds.All);
        }

        /// <summary>
        /// Gets idiomatic static members corresponding to member kinds.
        /// </summary>
        /// <param name="type">A type to enumerate the idiomatic static members.</param>
        /// <param name="memberKinds">The static member kinds.</param>
        /// <returns>The idiomatic static members.</returns>
        public static IEnumerable<MemberInfo> GetIdiomaticStaticMembers(
            this Type type,
            MemberKinds memberKinds)
        {
            return new TypeMembers(
                type,
                memberKinds,
                bindingFlags: TypeMembers.DefaultBindingFlags & ~BindingFlags.Instance);
        }
    }
}
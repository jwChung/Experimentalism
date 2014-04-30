using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Jwc.Experiment
{
    /// <summary>
    /// Defines idiomatic extension methods.
    /// </summary>
    public static class IdiomaticExtensions
    {
        /// <summary>
        /// Gets idiomatic members.
        /// </summary>
        /// <param name="type">A type to enumerate the idiomatic members.</param>
        /// <returns>The idiomatic members.</returns>
        public static IEnumerable<MemberInfo> GetIdiomaticMembers(this Type type)
        {
            return new IdiomaticMembers(type);
        }

        /// <summary>
        /// Gets idiomatic members corresponding to member kinds.
        /// </summary>
        /// <param name="type">A type to enumerate the idiomatic members.</param>
        /// <param name="memberKinds">The member kinds.</param>
        /// <returns>The idiomatic members.</returns>
        public static IEnumerable<MemberInfo> GetIdiomaticMembers(this Type type, MemberKinds memberKinds)
        {
            return new IdiomaticMembers(type, memberKinds);
        }

        /// <summary>
        /// Verifies members with an idiomatic member assertion.
        /// </summary>
        /// <param name="members">The members.</param>
        /// <param name="assertion">The assertion.</param>
        public static void Verify(this IEnumerable<MemberInfo> members, IIdiomaticMemberAssertion assertion)
        {
            if (members == null)
                throw new ArgumentNullException("members");

            if (assertion == null)
                throw new ArgumentNullException("assertion");

            foreach (var member in members)
            {
                assertion.Verify(member);
            }
        }

        /// <summary>
        /// Converts members to idiomatic test cases with an idiomatic member
        /// assertion.
        /// </summary>
        /// <param name="members">The members.</param>
        /// <param name="assertion">The assertion.</param>
        /// <returns>The test cases.</returns>
        public static IEnumerable<ITestCase> ToTestCases(
            this IEnumerable<MemberInfo> members, IIdiomaticMemberAssertion assertion)
        {
            if (members == null)
                throw new ArgumentNullException("members");

            if (assertion == null)
                throw new ArgumentNullException("assertion");

            return members.Select(m => new TestCase(() => assertion.Verify(m)));
        }
    }
}
namespace Jwc.Experiment
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Ploeh.Albedo.Refraction;

    /// <summary>
    /// Defines <see cref="MemberInfo" /> extension methods.
    /// </summary>
    public static class MemberInfoExtensions
    {
        /// <summary>
        /// Gets the display name of a member.
        /// </summary>
        /// <param name="member">
        /// The member.
        /// </param>
        /// <returns>
        /// the display name.
        /// </returns>
        public static string GetDisplayName(this MemberInfo member)
        {
            if (member == null)
                throw new ArgumentNullException("member");

            return member.ToReflectionElement().Accept(new DisplayNameCollector()).Value.Single();
        }
    }
}
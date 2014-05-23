using System.Reflection;

namespace Jwc.Experiment.Idioms.Assertions
{
    /// <summary>
    ///     Represents an encapsulation of an idiomatic unit test assertion based on
    ///     <see cref="MemberInfo" />.
    /// </summary>
    public interface IIdiomaticMemberAssertion
    {
        /// <summary>
        ///     Verifies that the idiomatic assertion can be verified for the specified member.
        /// </summary>
        /// <param name="member">
        ///     The member.
        /// </param>
        void Verify(MemberInfo member);
    }
}
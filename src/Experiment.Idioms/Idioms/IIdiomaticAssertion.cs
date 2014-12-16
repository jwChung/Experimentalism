namespace Jwc.Experiment.Idioms
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Represents an encapsulation of an idiomatic unit test assertion.
    /// </summary>
    public interface IIdiomaticAssertion
    {
        /// <summary>
        /// Verifies that the idiomatic assertion can be verified for the specified assembly.
        /// </summary>
        /// <param name="assembly">
        /// The assembly.
        /// </param>
        void Verify(Assembly assembly);
        
        /// <summary>
        /// Verifies that the idiomatic assertion can be verified for the specified type.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        void Verify(Type type);

        /// <summary>
        /// Verifies that the idiomatic assertion can be verified for the specified member.
        /// </summary>
        /// <param name="member">
        /// The member.
        /// </param>
        void Verify(MemberInfo member);
    }
}
namespace Jwc.Experiment.Idioms
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Composes an arbitrary number of <see cref="IIdiomaticAssertion"/> instances.
    /// </summary>
    public class CompositeIdiomaticAssertion : IIdiomaticAssertion
    {
        /// <summary>
        /// Verifies that the idiomatic assertion can be verified for the specified assembly.
        /// </summary>
        /// <param name="assembly">
        /// The assembly.
        /// </param>
        public void Verify(Assembly assembly)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Verifies that the idiomatic assertion can be verified for the specified type.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        public void Verify(Type type)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Verifies that the idiomatic assertion can be verified for the specified member.
        /// </summary>
        /// <param name="member">
        /// The member.
        /// </param>
        public void Verify(MemberInfo member)
        {
            throw new NotImplementedException();
        }
    }
}
namespace Jwc.Experiment.Idioms
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Represents base class implementing <see cref="IIdiomaticAssemblyAssertion" />,
    /// <see cref="IIdiomaticTypeAssertion" /> and <see cref="IIdiomaticMemberAssertion" />.
    /// </summary>
    public abstract class IdiomaticAssertion
        : IdiomaticMemberAssertion, IIdiomaticAssemblyAssertion, IIdiomaticTypeAssertion
    {
        /// <summary>
        /// Verifies that the idiomatic assertion can be verified for the specified assembly.
        /// </summary>
        /// <param name="assembly">
        /// The assembly.
        /// </param>
        public virtual void Verify(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");

            foreach (var type in assembly.GetExportedTypes())
                this.Verify(type);
        }

        /// <summary>
        /// Verifies that the idiomatic assertion can be verified for the specified type.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        public virtual void Verify(Type type)
        {
            foreach (var member in type.GetIdiomaticMembers())
                this.Verify(member);
        }
    }
}
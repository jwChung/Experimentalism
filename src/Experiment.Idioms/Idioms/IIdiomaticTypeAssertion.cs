using System;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents an encapsulation of an idiomatic unit test assertion based on
    /// <see cref="Type" />.
    /// </summary>
    public interface IIdiomaticTypeAssertion
    {
        /// <summary>
        /// Verifies that the idiomatic assertion can be verified for the specified type.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        void Verify(Type type);
    }
}
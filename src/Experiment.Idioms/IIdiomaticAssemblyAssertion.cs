using System.Reflection;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents an encapsulation of an idiomatic unit test assertion based on
    /// <see cref="Assembly"/>.
    /// </summary>
    public interface IIdiomaticAssemblyAssertion
    {
        /// <summary>
        /// Verifies that the idiomatic assertion can be verified for the
        /// specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        void Verify(Assembly assembly);
    }
}
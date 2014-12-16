namespace Jwc.Experiment.Idioms
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Composes an arbitrary number of <see cref="IIdiomaticAssertion"/> instances.
    /// </summary>
    public class CompositeIdiomaticAssertion : IIdiomaticAssertion
    {
        private readonly IEnumerable<IIdiomaticAssertion> assertions;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeIdiomaticAssertion"/> class.
        /// </summary>
        /// <param name="assertions">
        /// The assertions.
        /// </param>
        public CompositeIdiomaticAssertion(IEnumerable<IIdiomaticAssertion> assertions)
        {
            if (assertions == null)
                throw new ArgumentNullException("assertions");

            this.assertions = assertions;
        }

        /// <summary>
        /// Gets the idiomatic assertions.
        /// </summary>
        public IEnumerable<IIdiomaticAssertion> Assertions
        {
            get { return this.assertions; }
        }

        /// <summary>
        /// Verifies that the idiomatic assertion can be verified for the specified assembly.
        /// </summary>
        /// <param name="assembly">
        /// The assembly.
        /// </param>
        public void Verify(Assembly assembly)
        {
            foreach (var assertion in this.assertions)
                assertion.Verify(assembly);
        }

        /// <summary>
        /// Verifies that the idiomatic assertion can be verified for the specified type.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        public void Verify(Type type)
        {
            foreach (var assertion in this.assertions)
                assertion.Verify(type);
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
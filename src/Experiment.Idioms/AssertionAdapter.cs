using System;
using Ploeh.Albedo;
using Ploeh.AutoFixture.Idioms;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents an assertion to adapt <see cref="IIdiomaticAssertion"/>
    /// to <see cref="IReflectionVisitor{T}"/>.
    /// </summary>
    public class AssertionAdapter : ReflectionVisitor<object>
    {
        private readonly IIdiomaticAssertion _assertion;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssertionAdapter"/> class.
        /// </summary>
        /// <param name="assertion">The assertion.</param>
        public AssertionAdapter(IIdiomaticAssertion assertion)
        {
            _assertion = assertion;
            if (assertion == null)
            {
                throw new ArgumentNullException("assertion");
            }
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public override object Value
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        /// <summary>
        /// Gets the assertion.
        /// </summary>
        /// <value>
        /// The assertion.
        /// </value>
        public IIdiomaticAssertion Assertion
        {
            get
            {
                return _assertion;
            }
        }

        /// <summary>
        /// Visits the specified assembly element.
        /// </summary>
        /// <param name="assemblyElement">The assembly element.</param>
        /// <returns>The result visitor.</returns>
        public override IReflectionVisitor<object> Visit(AssemblyElement assemblyElement)
        {
            if (assemblyElement == null)
            {
                throw new ArgumentNullException("assemblyElement");
            }

            _assertion.Verify(assemblyElement.Assembly);
            return this;
        }
    }
}
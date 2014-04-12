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
        /// <summary>
        /// Initializes a new instance of the <see cref="AssertionAdapter"/> class.
        /// </summary>
        /// <param name="assertion">The assertion.</param>
        public AssertionAdapter(IIdiomaticAssertion assertion)
        {
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
    }
}
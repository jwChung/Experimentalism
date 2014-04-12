using System;
using Ploeh.Albedo;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Idioms;
using Ploeh.AutoFixture.Kernel;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents a factory to create <see cref="GuardClauseAssertion"/>
    /// </summary>
    public class GuardClauseAssertionFactory : IAssertionFactory
    {
        /// <summary>
        /// Creates an assertion.
        /// </summary>
        /// <param name="testFixture">The test fixture.</param>
        /// <returns>
        /// The created assertion.
        /// </returns>
        public IReflectionVisitor<object> Create(ITestFixture testFixture)
        {
            if (testFixture == null)
            {
                throw new ArgumentNullException("testFixture");
            }

            var fixture = (ISpecimenBuilder)testFixture.Create(typeof(Fixture));
            return new AssertionAdapter(new GuardClauseAssertion(fixture));
        }
    }
}
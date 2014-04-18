using Ploeh.Albedo;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents a factory to create <see cref="ConstructingMemberAssertion"/>
    /// </summary>
    public class ConstructingMemberAssertionFactory : IAssertionFactory
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
            return new ConstructingMemberAssertion(
                new OrEqualityComparer<IReflectionElement>(
                    new ParameterToPropertyComparer(testFixture),
                    new ParameterToFieldComparer(testFixture)),
                new OrEqualityComparer<IReflectionElement>(
                    new PropertyToParameterComparer(testFixture),
                    new FieldToParameterComparer(testFixture)));
        }
    }
}
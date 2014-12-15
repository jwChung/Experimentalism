namespace Jwc.Experiment.Xunit
{
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;

    /// <summary>
    /// Represents a factory to create an instance of <see cref="ITestFixture" />.
    /// </summary>
    public interface IFixtureFactory
    {
        /// <summary>
        /// Creates a test fixture.
        /// </summary>
        /// <param name="context">
        /// Information about a test method.
        /// </param>
        /// <returns>
        /// The test fixture.
        /// </returns>
        ITestFixture Create(ITestMethodContext context);

        /// <summary>
        /// Creates a fixture.
        /// </summary>
        /// <param name="context">
        /// Information about a test method.
        /// </param>
        /// <returns>
        /// The specimen builder.
        /// </returns>
        ISpecimenBuilder NewCreate(ITestMethodContext context);
    }
}
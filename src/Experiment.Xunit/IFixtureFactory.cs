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
        /// Creates a fixture.
        /// </summary>
        /// <param name="context">
        /// Information about a test method.
        /// </param>
        /// <returns>
        /// The specimen builder.
        /// </returns>
        ISpecimenBuilder Create(ITestMethodContext context);
    }
}
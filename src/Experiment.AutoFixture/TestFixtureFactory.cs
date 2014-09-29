namespace Jwc.Experiment.AutoFixture
{
    using System;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoMoq;

    /// <summary>
    /// Represents a factory to create test fixtures.
    /// </summary>
    public class TestFixtureFactory : ITestFixtureFactory
    {
        /// <summary>
        /// Creates a test fixture.
        /// </summary>
        /// <param name="context">
        /// The test method context to provide information for creating the test fixture.
        /// </param>
        /// <returns>
        /// The test fixture.
        /// </returns>
        public ITestFixture Create(ITestMethodContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            return new TestFixture(new Fixture().Customize(this.GetCustomization(context)));
        }

        /// <summary>
        /// Gets a customization.
        /// </summary>
        /// <param name="context">
        /// The test method context to provide information for getting the customization.
        /// </param>
        /// <returns>
        /// The customization.
        /// </returns>
        protected virtual ICustomization GetCustomization(ITestMethodContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            return new CompositeCustomization(
                new OmitAutoPropertiesCustomization(),
                new AutoMoqCustomization(),
                new TestParametersCustomization(context.ActualMethod.GetParameters()));
        }
    }
}
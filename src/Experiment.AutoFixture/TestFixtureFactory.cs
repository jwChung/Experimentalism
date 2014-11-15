namespace Jwc.Experiment.AutoFixture
{
    using System;
    using System.ComponentModel;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoMoq;

    /// <summary>
    /// Represents a factory to create test fixtures.
    /// </summary>
    [Obsolete("Do not use this class but instead, explicity create TestFixture.")]
    [EditorBrowsable(EditorBrowsableState.Never)]
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
        [Obsolete("Do not use this method but instead, explicitly create TestFixture.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public ITestFixture Create(ITestMethodContext context)
        {
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
        [Obsolete("Do not use this method but instead, explicitly create TestFixture.")]
        [EditorBrowsable(EditorBrowsableState.Never)]
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
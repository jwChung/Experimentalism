namespace Jwc.Experiment.AutoFixture
{
    using System;
    using Ploeh.AutoFixture;

    /// <summary>
    /// Represents fixture customization to set false to <see cref="IFixture.OmitAutoProperties"/>.
    /// </summary>
    public class OmitAutoPropertiesCustomization : ICustomization
    {
        /// <summary>
        /// Customizes the specified fixture.
        /// </summary>
        /// <param name="fixture">
        /// The fixture to customize.
        /// </param>
        public void Customize(IFixture fixture)
        {
            if (fixture == null)
                throw new ArgumentNullException("fixture");

            fixture.OmitAutoProperties = true;
        }
    }
}
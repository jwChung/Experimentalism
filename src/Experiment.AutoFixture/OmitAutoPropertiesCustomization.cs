namespace Jwc.Experiment.AutoFixture
{
    using System;
    using Ploeh.AutoFixture;

    public class OmitAutoPropertiesCustomization : ICustomization
    {
        public void Customize(IFixture fixture)
        {
            if (fixture == null)
                throw new ArgumentNullException("fixture");

            fixture.OmitAutoProperties = true;
        }
    }
}
namespace Jwc.Experiment.AutoFixture
{
    using System;
    using Ploeh.AutoFixture;

    public class TestFixtureFactory : ITestFixtureFactory
    {
        public ITestFixture Create(ITestMethodContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            return new TestFixture(new Fixture().Customize(this.GetCustomization(context)));
        }

        protected virtual ICustomization GetCustomization(ITestMethodContext context)
        {
            return new CompositeCustomization(
                new OmitAutoPropertiesCustomization(),
                new TestParametersCustomization(context));
        }
    }
}
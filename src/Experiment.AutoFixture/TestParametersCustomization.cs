namespace Jwc.Experiment.AutoFixture
{
    using System;
    using System.Linq;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Xunit;

    public class TestParametersCustomization : ICustomization
    {
        private readonly ITestMethodContext testMethodContext;

        public TestParametersCustomization(ITestMethodContext testMethodContext)
        {
            if (testMethodContext == null)
                throw new ArgumentNullException("testMethodContext");

            this.testMethodContext = testMethodContext;
        }

        public ITestMethodContext TestMethodContext
        {
            get { return this.testMethodContext; }
        }

        public void Customize(IFixture fixture)
        {
            if (fixture == null)
                throw new ArgumentNullException("fixture");

            var customizations = this.testMethodContext.ActualMethod.GetParameters()
                .SelectMany(p =>
                    p.GetCustomAttributes(typeof(CustomizeAttribute), false)
                    .Cast<CustomizeAttribute>()
                    .Select(c => c.GetCustomization(p)))
                .ToArray();

            fixture.Customize(new CompositeCustomization(customizations));
        }
    }
}
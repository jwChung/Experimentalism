namespace Jwc.Experiment.AutoFixture
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Xunit;

    /// <summary>
    /// Represents a customization to support <see cref="CustomizeAttribute"/>(s).
    /// </summary>
    public class TestParametersCustomization : ICustomization
    {
        private readonly IEnumerable<ParameterInfo> parameters;
        private readonly ITestMethodContext testMethodContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestParametersCustomization"/> class.
        /// </summary>
        /// <param name="testMethodContext">
        /// The test method context.
        /// </param>
        public TestParametersCustomization(ITestMethodContext testMethodContext)
        {
            if (testMethodContext == null)
                throw new ArgumentNullException("testMethodContext");

            this.testMethodContext = testMethodContext;
        }

        public TestParametersCustomization(IEnumerable<ParameterInfo> parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException("parameters");

            this.parameters = parameters;
        }

        /// <summary>
        /// Gets the test method context.
        /// </summary>
        public ITestMethodContext TestMethodContext
        {
            get { return this.testMethodContext; }
        }

        public IEnumerable<ParameterInfo> Parameters
        {
            get { return this.parameters; }
        }

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

            var customizations = this.parameters
                .SelectMany(
                    p => p.GetCustomAttributes(typeof(CustomizeAttribute), false)
                        .Cast<CustomizeAttribute>()
                        .Select(c => c.GetCustomization(p)))
                .ToArray();

            fixture.Customize(new CompositeCustomization(customizations));
        }
    }
}
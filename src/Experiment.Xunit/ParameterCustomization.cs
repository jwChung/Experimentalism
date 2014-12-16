namespace Jwc.Experiment.Xunit
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
    public class ParameterCustomization : ICustomization
    {
        private readonly IEnumerable<ParameterInfo> parameters;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterCustomization"/> class.
        /// </summary>
        /// <param name="parameters">
        /// Test parameters to get <see cref="CustomizeAttribute"/> to customize fixture.
        /// </param>
        public ParameterCustomization(IEnumerable<ParameterInfo> parameters)
        {
            if (parameters == null)
                throw new ArgumentNullException("parameters");

            this.parameters = parameters;
        }

        /// <summary>
        /// Gets the test parameters.
        /// </summary>
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
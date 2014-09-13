namespace Jwc.Experiment.AutoFixture
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoMoq;

    /// <summary>
    /// Represent the default factory for <see cref="ITestFixtureFactory" />.
    /// </summary>
    public class TestFixtureFactory : ITestFixtureFactory
    {
        /// <summary>
        /// Creates a test fixture.
        /// </summary>
        /// <param name="testMethod">
        /// The test method in which the test fixture will be used.
        /// </param>
        /// <returns>
        /// The test fixture.
        /// </returns>
        public ITestFixture Create(MethodInfo testMethod)
        {
            if (testMethod == null)
                throw new ArgumentNullException("testMethod");

#pragma warning disable 618
            var fixture = this.CreateFixture(testMethod).Customize(
                new ParameterAttributeCutomization(testMethod.GetParameters()));
#pragma warning restore 618
            return new TestFixture(fixture);
        }

        /// <summary>
        /// Creates a fixture with a test method.
        /// </summary>
        /// <param name="testMethod">
        /// The test method.
        /// </param>
        /// <returns>
        /// The new fixture.
        /// </returns>
        [Obsolete("Use the GetCustomiztion method instead. This method will be removed on the next major release.")]
        protected virtual IFixture CreateFixture(MethodInfo testMethod)
        {
            return new Fixture().Customize(this.GetCustomization(testMethod));
        }

        /// <summary>
        /// Creates a customization to customize test fixture.
        /// </summary>
        /// <param name="testMethod">
        /// The test method.
        /// </param>
        /// <returns>
        /// The new customization.
        /// </returns>
        protected virtual ICustomization GetCustomization(MethodInfo testMethod)
        {
            return new CompositeCustomization(
                new AutoMoqCustomization(),
                new OmitAutoPropertiesCustomizatoin());
        }

        private class ParameterAttributeCutomization : ICustomization
        {
            private readonly IEnumerable<ParameterInfo> parameters;

            public ParameterAttributeCutomization(IEnumerable<ParameterInfo> parameters)
            {
                this.parameters = parameters;
            }

            public void Customize(IFixture fixture)
            {
                this.parameters.SelectMany(GetCustomizations)
                    .Aggregate(fixture, (f, c) => f.Customize(c));
            }

            private static IEnumerable<ICustomization> GetCustomizations(ParameterInfo parameter)
            {
                return parameter.GetCustomAttributes(typeof(CustomizeAttribute), false)
                    .Cast<CustomizeAttribute>()
                    .Select(ca => ca.GetCustomization(parameter));
            }
        }

        private class OmitAutoPropertiesCustomizatoin : ICustomization
        {
            public void Customize(IFixture fixture)
            {
                if (fixture == null)
                    throw new ArgumentNullException("fixture");

                fixture.OmitAutoProperties = true;
            }
        }
    }
}
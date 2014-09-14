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

            var fixture = new Fixture()
                .Customize(this.GetCustomization(testMethod));

            return new TestFixture(fixture);
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace Jwc.Experiment.AutoFixture
{
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

            var cutomization = new ParameterAttributeCutomization(testMethod.GetParameters());
            return new TestFixture(this.CreateFixture(testMethod).Customize(cutomization));
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
        protected virtual IFixture CreateFixture(MethodInfo testMethod)
        {
            return new Fixture { OmitAutoProperties = true }.Customize(new AutoMoqCustomization());
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
    }
}
using System;
using System.Reflection;
using Jwc.Experiment;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using Ploeh.AutoFixture.Xunit;

namespace Jwc.Experiment
{
    /// <summary>
    /// Represents a fixture factory to create an instance of
    /// <see cref="Fixture"/>.
    /// </summary>
    public class AutoFixtureFactory : ITestFixtureFactory
    {
        /// <summary>
        /// Creates an instance of <see cref="Fixture" />.
        /// </summary>
        /// <param name="testMethod">The test method</param>
        /// <returns>
        /// The created fixture.
        /// </returns>
        public ITestFixture Create(MethodInfo testMethod)
        {
            if (testMethod == null)
            {
                throw new ArgumentNullException("testMethod");
            }

            var fixture = CreateFixture();
            foreach (var parameter in testMethod.GetParameters())
            {
                Customize(fixture, parameter);
            }

            return new TestFixtureAdapter(new SpecimenContext(fixture));
        }

        private static IFixture CreateFixture()
        {
            return new Fixture();
        }

        private static void Customize(IFixture fixture, ParameterInfo parameter)
        {
            foreach (CustomizeAttribute customAttribute
                in parameter.GetCustomAttributes(typeof(CustomizeAttribute), false))
            {
                var customization = customAttribute.GetCustomization(parameter);
                fixture.Customize(customization);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
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
        private static readonly ITestFixtureFactory _instance = new AutoFixtureFactory();

        private AutoFixtureFactory()
        {
        }

        /// <summary>
        /// Gets the singleton instance of <see cref="AutoFixtureFactory"/>.
        /// </summary>
        public static ITestFixtureFactory Instance
        {
            get
            {
                return _instance;
            }
        }

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

            var fixture = testMethod.GetParameters()
                .SelectMany(SelectCustomizations)
                .Aggregate(CreateFixture(), (f, c) => f.Customize(c));

            return new AutoFixtureAdapter(new SpecimenContext(fixture));
        }

        private static IEnumerable<ICustomization> SelectCustomizations(ParameterInfo parameter)
        {
            return parameter.GetCustomAttributes(typeof(CustomizeAttribute), false)
                .Cast<CustomizeAttribute>()
                .Select(a => a.GetCustomization(parameter));
        }

        private static IFixture CreateFixture()
        {
            return new Fixture();
        }
    }
}
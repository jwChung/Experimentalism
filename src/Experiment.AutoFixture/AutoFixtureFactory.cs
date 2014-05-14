using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Jwc.Experiment;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit;

[assembly: TestFixtureFactory(typeof(NuGet.Jwc.Experiment.AutoFixtureFactory))]

namespace NuGet.Jwc.Experiment
{
    /// <summary>
    /// Represent the default factory for <see cref="ITestFixtureFactory"/>.
    /// </summary>
    public class AutoFixtureFactory : ITestFixtureFactory
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
            var cutomization = new ParameterAttributeCutomization(testMethod.GetParameters());
            var fixture = new Fixture().Customize(cutomization);
            return new AutoFixture(fixture);
        }

        private class ParameterAttributeCutomization : ICustomization
        {
            private readonly IEnumerable<ParameterInfo> _parameters;

            public ParameterAttributeCutomization(IEnumerable<ParameterInfo> parameters)
            {
                _parameters = parameters;
            }

            public void Customize(IFixture fixture)
            {
                _parameters.SelectMany(SelectCustomizations)
                    .Aggregate(fixture, (f, c) => f.Customize(c));
            }

            private static IEnumerable<ICustomization> SelectCustomizations(ParameterInfo parameter)
            {
                return parameter.GetCustomAttributes(typeof(CustomizeAttribute), false)
                    .Cast<CustomizeAttribute>()
                    .Select(ca => ca.GetCustomization(parameter));
            }
        }
    }
}
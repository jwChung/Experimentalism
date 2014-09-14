namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using global::Xunit.Extensions;
    using global::Xunit.Sdk;

    /// <summary>
    /// Represents a factory to create test commands whose data is from <see cref="DataAttribute"/>.
    /// </summary>
    public class DataAttributeCommandFactory : ITestCommandFactory
    {
        /// <summary>
        /// Creates test commands.
        /// </summary>
        /// <param name="testMethod">
        /// Information about a test method.
        /// </param>
        /// <param name="fixtureFactory">
        /// A factory of test fixture.
        /// </param>
        /// <returns>
        /// The new test commands.
        /// </returns>
        public IEnumerable<ITestCommand> Create(IMethodInfo testMethod, ITestFixtureFactory fixtureFactory)
        {
            if (testMethod == null)
                throw new ArgumentNullException("testMethod");

            var attributes = testMethod.MethodInfo
                .GetCustomAttributes(typeof(DataAttribute), false).Cast<DataAttribute>();

            if (!attributes.Any())
                return Enumerable.Empty<ITestCommand>();

            var parameterTypes = testMethod.MethodInfo.GetParameters()
                .Select(p => p.ParameterType).ToArray();

            return from attribute in attributes
                   from data in attribute.GetData(testMethod.MethodInfo, parameterTypes)
                   select new ParameterizedCommand(
                       new TestCommandContext(testMethod, fixtureFactory, data));
        }
    }
}
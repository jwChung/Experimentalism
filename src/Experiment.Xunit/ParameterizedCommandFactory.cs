namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;
    using global::Xunit.Sdk;

    /// <summary>
    /// Represents a factory to create parameterized commands.
    /// </summary>
    public class ParameterizedCommandFactory : ITestCommandFactory
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
            if (testMethod.MethodInfo.GetParameters().Length == 0)
                yield break;

            yield return new ParameterizedCommand(
                new TestCommandContext(testMethod, fixtureFactory, new object[0]));
        }
    }
}

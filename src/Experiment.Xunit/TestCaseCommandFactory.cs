namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using global::Xunit.Sdk;

    /// <summary>
    /// Represents a factory to create test-case commands.
    /// </summary>
    public class TestCaseCommandFactory : ITestCommandFactory
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

            var method = testMethod.MethodInfo;
            if (!typeof(IEnumerable<ITestCase2>).IsAssignableFrom(method.ReturnType)
                || method.GetParameters().Length != 0)
                yield break;

            var reflectedType = method.ReflectedType;
            var testObject = !reflectedType.IsAbstract || !reflectedType.IsSealed
                ? Activator.CreateInstance(reflectedType)
                : null;
            var testCases = (IEnumerable<ITestCase2>)method.Invoke(testObject, null);
            var commands = testCases.Select(t => new ParameterizedCommand(
                new TestInfo(method, t.TestMethod, testObject, fixtureFactory, t.Arguments)));

            foreach (var command in commands)
                yield return command;
        }
    }
}

namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using global::Xunit.Sdk;

    /// <summary>
    /// Represents a factory to create test-case commands.
    /// </summary>
    public class TestCaseCommandFactory2 : ITestCommandFactory
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

            if (IsValidSignature(testMethod.MethodInfo))
                return Enumerable.Empty<ITestCommand>();

            if (testMethod.IsStatic)
                return CreateStaticTestCommands(testMethod, fixtureFactory);

            return CreateTestCommands(testMethod, fixtureFactory);
        }

        private static bool IsValidSignature(MethodInfo method)
        {
            return !typeof(IEnumerable<ITestCase2>).IsAssignableFrom(method.ReturnType)
                || method.GetParameters().Length != 0;
        }

        private static IEnumerable<ITestCommand> CreateStaticTestCommands(
            IMethodInfo testMethod, ITestFixtureFactory fixtureFactory)
        {
            var testCases = (IEnumerable<ITestCase2>)testMethod.MethodInfo.Invoke(null, new object[0]);
            return testCases.Select(
                c => new ParameterizedCommand(
                    new TestCommandContext(
                        testMethod,
                        Reflector.Wrap(c.TestMethod),
                        fixtureFactory,
                        c.Arguments)));
        }

        private static IEnumerable<ITestCommand> CreateTestCommands(
            IMethodInfo testMethod, ITestFixtureFactory fixtureFactory)
        {
            var testObject = testMethod.CreateInstance();
            var testCases = (IEnumerable<ITestCase2>)testMethod.MethodInfo.Invoke(testObject, new object[0]);

            return testCases.Select(
                c => new ParameterizedCommand(
                    new TestCommandContext(
                        testMethod,
                        Reflector.Wrap(c.TestMethod),
                        testObject,
                        fixtureFactory,
                        c.Arguments)));
        }
    }
}
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

            if (!IsValidSignature(testMethod.MethodInfo))
                return Enumerable.Empty<ITestCommand>();

            return CreateTestCases(testMethod).Select(
                c => c.Target != null
                    ? CreateCommand(testMethod, fixtureFactory, c)
                    : CreateStaticCommand(testMethod, fixtureFactory, c));
        }

        private static IEnumerable<ITestCase> CreateTestCases(IMethodInfo testMethod)
        {
            return (IEnumerable<ITestCase>)testMethod.MethodInfo.Invoke(
                CreateTestClass(testMethod), new object[0]);
        }

        private static object CreateTestClass(IMethodInfo testMethod)
        {
            return testMethod.MethodInfo.ReflectedType.IsAbstract
                ? null
                : testMethod.CreateInstance();
        }

        private static bool IsValidSignature(MethodInfo method)
        {
            return typeof(IEnumerable<ITestCase>).IsAssignableFrom(method.ReturnType)
                && method.GetParameters().Length == 0;
        }

        private static ParameterizedCommand CreateCommand(
            IMethodInfo testMethod, ITestFixtureFactory fixtureFactory, ITestCase testCase)
        {
            return new ParameterizedCommand(
                new TestCaseCommandContext(
                    testMethod,
                    Reflector.Wrap(testCase.TestMethod),
                    testCase.Target,
                    fixtureFactory,
                    testCase.Arguments));
        }

        private static ParameterizedCommand CreateStaticCommand(
            IMethodInfo testMethod, ITestFixtureFactory fixtureFactory, ITestCase testCase)
        {
            return new ParameterizedCommand(
                new StaticTestCaseCommandContext(
                    testMethod,
                    Reflector.Wrap(testCase.TestMethod),
                    fixtureFactory,
                    testCase.Arguments));
        }
    }
}
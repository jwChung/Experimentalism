namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections;
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
        public IEnumerable<ITestCommand> Create(
            IMethodInfo testMethod, IFixtureFactory fixtureFactory)
        {
            if (testMethod == null)
                throw new ArgumentNullException("testMethod");
            
            if (!IsValidSignature(testMethod.MethodInfo))
                return Enumerable.Empty<ITestCommand>();

            return new TestCommandContextCollection(testMethod, fixtureFactory)
                .Select(c => new ParameterizedCommand(c));
        }

        private static bool IsValidSignature(MethodInfo method)
        {
            return typeof(IEnumerable<ITestCase>).IsAssignableFrom(method.ReturnType);
        }

        private class TestCommandContextCollection : IEnumerable<ITestCommandContext>
        {
            private readonly IMethodInfo testMethod;
            private readonly MethodInfo methodInfo;
            private readonly IFixtureFactory fixtureFactory;

            public TestCommandContextCollection(
                IMethodInfo testMethod, IFixtureFactory fixtureFactory)
            {
                this.testMethod = testMethod;
                this.methodInfo = testMethod.MethodInfo;
                this.fixtureFactory = fixtureFactory;
            }

            public IEnumerator<ITestCommandContext> GetEnumerator()
            {
                return this.CreateTestCases().Select(this.TestCommandContext).GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                throw new NotImplementedException();
            }

            private IEnumerable<ITestCase> CreateTestCases()
            {
                object testObject = this.CreateTestClass();
                return (IEnumerable<ITestCase>)this.methodInfo.Invoke(
                    testObject, this.GetArguments(testObject));
            }

            private ITestCommandContext TestCommandContext(ITestCase testCase)
            {
                return testCase.Target != null
                    ? (ITestCommandContext)new TestCaseCommandContext(
                        this.testMethod,
                        Reflector.Wrap(testCase.TestMethod),
                        testCase.Target,
                        this.fixtureFactory,
                        testCase.Arguments)
                    : new StaticTestCaseCommandContext(
                        this.testMethod,
                        Reflector.Wrap(testCase.TestMethod),
                        this.fixtureFactory,
                        testCase.Arguments);
            }

            private object CreateTestClass()
            {
                return this.methodInfo.ReflectedType.IsAbstract
                    ? null
                    : this.testMethod.CreateInstance();
            }

            private object[] GetArguments(object testObject)
            {
                if (!this.methodInfo.GetParameters().Any())
                    return new object[0];

                var fixture = this.fixtureFactory.NewCreate(new TestMethodContext(
                    this.methodInfo,
                    this.methodInfo,
                    testObject,
                    testObject));

                return this.methodInfo.GetParameters()
                    .Select(p => fixture.CreateAnonymous(p)).ToArray();
            }
        }
    }
}
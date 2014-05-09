using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Sdk;

namespace Jwc.Experiment
{
    /// <summary>
    /// A test attribute used to adorn methods that creates first-class
    /// executable test cases.
    /// </summary>
    public abstract class FirstClassTheoremBaseAttribute : FactAttribute
    {
        /// <summary>
        /// Enumerates the test commands represented by this test method.
        /// Derived classes should override this method to return instances of
        /// <see cref="ITestCommand" />, one per execution of a test method.
        /// </summary>
        /// <param name="method">The test method</param>
        /// <returns>
        /// The test commands which will execute the test runs for the given method
        /// </returns>
        protected override IEnumerable<ITestCommand> EnumerateTestCommands(IMethodInfo method)
        {
            if (method == null)
                throw new ArgumentNullException("method");

            var enumerator = GetTestCommands(method).GetEnumerator();
            
            Func<IMethodInfo, ITestCommand> exceptionCommandFunc;

            while (TryMoveNext(enumerator, out exceptionCommandFunc))
                yield return enumerator.Current;

            if (exceptionCommandFunc != null)
                yield return exceptionCommandFunc.Invoke(method);
        }

        /// <summary>
        /// Creates an instance of <see cref="ITestFixture" />.
        /// </summary>
        /// <param name="testMethod">
        /// The test method
        /// </param>
        /// <returns>
        /// The created fixture.
        /// </returns>
        protected abstract ITestFixture CreateTestFixture(MethodInfo testMethod);

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This is suppressed to catch unhandled exception thrown when creating test commands.")]
        private IEnumerable<ITestCommand> GetTestCommands(IMethodInfo method)
        {
            try
            {
                return CreateTestCases(method).Select(tc => ConvertToTestCommand(method, tc));
            }
            catch (Exception exception)
            {
                return new ITestCommand[] { new ExceptionCommand(method, exception) };
            }
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This is suppressed to catch unhandled exception thrown when creating test commands.")]
        private static bool TryMoveNext(
            IEnumerator<ITestCommand> enumerator,
            out Func<IMethodInfo, ITestCommand> exceptionCommandFunc)
        {
            try
            {
                var moveNext = enumerator.MoveNext();
                exceptionCommandFunc = null;
                return moveNext;
            }
            catch (Exception exception)
            {
                exceptionCommandFunc = m => new ExceptionCommand(m, exception);
                return false;
            }
        }

        private static IEnumerable<ITestCase> CreateTestCases(IMethodInfo method)
        {
            var methodInfo = method.MethodInfo;
            if (!IsMethodParameterless(methodInfo))
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "The supplied method '{0}' is not parameterless.",
                        methodInfo),
                    "method");
            }

            if (!IsReturnTypeValid(methodInfo.ReturnType))
            {
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "The supplied method '{0}' does not return IEnumerable<ITestCase>.",
                        methodInfo),
                    "method");
            }

            var testCases = methodInfo.Invoke(CreateReflectedObject(methodInfo), null);
            return (IEnumerable<ITestCase>)testCases;
        }

        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This is suppressed to catch unhandled exception thrown by ConvertToTestCommand.")]
        private ITestCommand ConvertToTestCommand(IMethodInfo method, ITestCase testCase)
        {
            try
            {
                return testCase.ConvertToTestCommand(method, new TestFixtureFactory(CreateTestFixture));
            }
            catch (Exception exception)
            {
                return new ExceptionCommand(method, exception);
            }
        }

        private static bool IsMethodParameterless(MethodInfo methodInfo)
        {
            return !methodInfo.GetParameters().Any();
        }

        private static bool IsReturnTypeValid(Type returnType)
        {
            return typeof(IEnumerable<ITestCase>).IsAssignableFrom(returnType);
        }

        private static object CreateReflectedObject(MethodInfo methodInfo)
        {
            return methodInfo.IsStatic
                ? null
                : Activator.CreateInstance(methodInfo.ReflectedType);
        }

        private class TestFixtureFactory : ITestFixtureFactory
        {
            private readonly Func<MethodInfo, ITestFixture> _testFixtureFactory;

            public TestFixtureFactory(Func<MethodInfo, ITestFixture> testFixtureFactory)
            {
                _testFixtureFactory = testFixtureFactory;
            }

            public ITestFixture Create(MethodInfo testMethod)
            {
                return _testFixtureFactory(testMethod);
            }
        }
    }
}
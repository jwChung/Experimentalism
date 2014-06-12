using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Xunit;
using Xunit.Sdk;

namespace Jwc.Experiment.Xunit
{
    /// <summary>
    ///     A test attribute used to adorn methods that creates first-class executable test
    ///     cases.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    [SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes", Justification = "This attribue can be inherited by custom attribute.")]
    public class FirstClassTestAttribute : FactAttribute
    {
        /// <summary>
        ///     Enumerates the test commands represented by this test method. Derived classes should
        ///     override this method to return instances of <see cref="ITestCommand" />, one per
        ///     execution of a test method.
        /// </summary>
        /// <param name="method">
        ///     The test method.
        /// </param>
        /// <returns>
        ///     The test commands which will execute the test runs for the given method.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This is suppressed to catch unhandled exception thrown by test assembly configuration.")]
        protected override IEnumerable<ITestCommand> EnumerateTestCommands(IMethodInfo method)
        {
            if (method == null)
                throw new ArgumentNullException("method");

            try
            {
                TestAssemblyConfigurationAttribute.Configure(
                    method.MethodInfo.ReflectedType.Assembly);
            }
            catch (Exception exception)
            {
                return new[] { new ExceptionCommand(method, exception) };
            }

            return new TestCommandEnumerable(method, GetTestCommands(method));
        }

        /// <summary>
        ///     Creates an instance of <see cref="ITestFixture" />.
        /// </summary>
        /// <param name="testMethod">
        ///     The test method.
        /// </param>
        /// <returns>
        ///     The created fixture.
        /// </returns>
        protected virtual ITestFixture CreateTestFixture(MethodInfo testMethod)
        {
            return DefaultFixtureFactory.Current.Create(testMethod);
        }

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
                return testCase.ConvertToTestCommand(method, new FuncFixtureFactory(CreateTestFixture));
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

        private class FuncFixtureFactory : ITestFixtureFactory
        {
            private readonly Func<MethodInfo, ITestFixture> _func;

            public FuncFixtureFactory(Func<MethodInfo, ITestFixture> func)
            {
                _func = func;
            }

            public ITestFixture Create(MethodInfo testMethod)
            {
                return _func(testMethod);
            }
        }
    }
}
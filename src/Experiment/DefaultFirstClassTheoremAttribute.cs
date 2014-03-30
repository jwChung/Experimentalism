using System;
using System.Collections.Generic;
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
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes", Justification = "Parameterized test에 auto data를 제공하기 위해, Subclass에서 ITestFixture factory를 제공할 수 있음.")]
    [AttributeUsage(AttributeTargets.Method)]
    public class DefaultFirstClassTheoremAttribute : FactAttribute
    {
        private readonly ITestFixtureFactory _fixtureFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultFirstClassTheoremAttribute"/> class.
        /// </summary>
        public DefaultFirstClassTheoremAttribute()
        {
            _fixtureFactory = new TypeFixtureFactory(typeof(NotSupportedFixture));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultFirstClassTheoremAttribute"/> class.
        /// </summary>
        /// <param name="fixtureType">Type of the fixture.</param>
        public DefaultFirstClassTheoremAttribute(Type fixtureType)
        {
            if (fixtureType == null)
            {
                throw new ArgumentNullException("fixtureType");
            }

            _fixtureFactory = new TypeFixtureFactory(fixtureType);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultFirstClassTheoremAttribute"/> class.
        /// </summary>
        /// <param name="fixtureFactory">The fixture factory.</param>
        protected DefaultFirstClassTheoremAttribute(ITestFixtureFactory fixtureFactory)
        {
            if (fixtureFactory == null)
            {
                throw new ArgumentNullException("fixtureFactory");
            }

            _fixtureFactory = fixtureFactory;
        }

        /// <summary>
        /// Gets a value indicating the fixture factory passed from a constructor.
        /// </summary>
        public ITestFixtureFactory FixtureFactory
        {
            get
            {
                return _fixtureFactory;
            }
        }

        /// <summary>
        /// Gets a value indicating the fixture type passed from a constructor.
        /// </summary>
        public Type FixtureType
        {
            get
            {
                var dummyMethod = typeof(object).GetMethod("ToString");
                return FixtureFactory.Create(dummyMethod).GetType();
            }
        }

        /// <summary>
        /// Enumerates the test commands represented by this test method.
        /// Derived classes should override this method to return instances of
        /// <see cref="ITestCommand" />, one per execution of a test method.
        /// </summary>
        /// <param name="method">The test method</param>
        /// <returns>
        /// The test commands which will execute the test runs for the given method
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "This is suppressed to catch unhandled exception thrown from creating test commands.")]
        protected override IEnumerable<ITestCommand> EnumerateTestCommands(IMethodInfo method)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }

            try
            {
                return CreateTestCases(method).Select(
                        tc => tc.ConvertToTestCommand(method, FixtureFactory))
                    .ToArray();
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
                        "The supplied method '{0}' does not be parameterless.",
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
    }
}
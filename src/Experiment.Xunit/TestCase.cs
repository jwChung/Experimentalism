using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit.Sdk;

namespace Jwc.Experiment.Xunit
{
    /// <summary>
    ///     Represents a weakly-typed test case that can be turned into an xUnit.net
    ///     ITestCommand when returned from a test method adorned with the
    ///     <see cref="FirstClassTestAttribute" />.
    /// </summary>
    public class TestCase : ITestCase
    {
        private readonly Delegate _delegate;

        /// <summary>
        ///     Initializes a new instance of the <see cref="TestCase" /> class.
        /// </summary>
        /// <param name="delegate">
        ///     The test delegate.
        /// </param>
        public TestCase(Action @delegate) : this((Delegate)@delegate)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TestCase" /> class.
        /// </summary>
        /// <param name="delegate">
        ///     The test delegate.
        /// </param>
        public TestCase(Func<object> @delegate) : this((Delegate)@delegate)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="TestCase" /> class.
        /// </summary>
        /// <param name="delegate">
        ///     The test delegate.
        /// </param>
        public TestCase(Delegate @delegate)
        {
            if (@delegate == null)
                throw new ArgumentNullException("delegate");

            if (@delegate.GetInvocationList().Length != 1)
            {
                throw new ArgumentException(
                    "Composite delegates are not supported, set only one operation.",
                    "delegate");
            }

            _delegate = @delegate;
        }

        /// <summary>
        ///     Gets the test delegate.
        /// </summary>
        public Delegate Delegate
        {
            get
            {
                return _delegate;
            }
        }

        /// <summary>
        ///     Converts the instance to an xUnit.net ITestCommand instance.
        /// </summary>
        /// <param name="method">
        ///     The method adorned by a <see cref="FirstClassTestAttribute" />.
        /// </param>
        /// <param name="testFixtureFactory">
        ///     A test fixture factory to provide auto data.
        /// </param>
        /// <returns>
        ///     An xUnit.net ITestCommand that represents the executable test case.
        /// </returns>
        public ITestCommand ConvertToTestCommand(IMethodInfo method, ITestFixtureFactory testFixtureFactory)
        {
            if (method == null)
                throw new ArgumentNullException("method");

            if (testFixtureFactory == null)
                throw new ArgumentNullException("testFixtureFactory");

            var parameters = Delegate.Method.GetParameters();

            if (!parameters.Any())
                return CreateNonParamterizedCommand(method);

            return CreateParameterizedCommand(method, testFixtureFactory, parameters);
        }

        private FirstClassCommand CreateNonParamterizedCommand(IMethodInfo method)
        {
            return new FirstClassCommand(method, string.Empty, Delegate, new object[0]);
        }

        private ITestCommand CreateParameterizedCommand(
            IMethodInfo method,
            ITestFixtureFactory testFixtureFactory,
            IEnumerable<ParameterInfo> parameters)
        {
            var fixture = testFixtureFactory.Create(Delegate.Method);
            var arguments = parameters.Select(p => fixture.Create(p.ParameterType)).ToArray();
            return new FirstClassCommand(method, GetTestParameterName(arguments), Delegate, arguments);
        }

        private string GetTestParameterName(IList<object> arguments)
        {
            return string.Join(", ", GetArgumentValues(arguments));
        }

        private IEnumerable<string> GetArgumentValues(IList<object> arguments)
        {
            return Delegate.Method.GetParameters()
                .Select(pi => GetArgumentValue(pi.ParameterType.Name, arguments[pi.Position]));
        }

        private static string GetArgumentValue(string typeName, object argument)
        {
            return typeName + ": " + (argument ?? "(null)");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit.Sdk;

namespace Jwc.Experiment.Xunit
{
    /// <summary>
    /// Represents a weakly-typed test case that can be turned into an xUnit.net ITestCommand when
    /// returned from a test method adorned with the <see cref="FirstClassTestAttribute" />.
    /// </summary>
    public partial class TestCase : ITestCase
    {
        private readonly string displayParameterName;
        private readonly Delegate @delegate;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase" /> class.
        /// </summary>
        /// <param name="delegate">
        /// The test delegate.
        /// </param>
        public TestCase(Delegate @delegate)
        {
            if (@delegate == null)
                throw new ArgumentNullException("delegate");
            
            this.@delegate = @delegate;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase" /> class.
        /// </summary>
        /// <param name="delegate">
        /// The test delegate.
        /// </param>
        /// <param name="displayParameterName">
        /// A string to show parameters of a test method in test result.
        /// </param>
        public TestCase(Delegate @delegate, string displayParameterName)
            : this(@delegate)
        {
            if (displayParameterName == null)
                throw new ArgumentNullException("displayParameterName");

            this.displayParameterName = displayParameterName;
        }
        
        /// <summary>
        /// Gets a value indicating the string to show parameters of a test method in test result.
        /// </summary>
        public string DisplayParameterName
        {
            get { return this.displayParameterName; }
        }

        /// <summary>
        /// Gets the test delegate.
        /// </summary>
        public Delegate Delegate
        {
            get { return @delegate; }
        }

        /// <summary>
        /// Creates a new instance of <see cref="TestCase"/>.
        /// </summary>
        /// <param name="action">
        /// An action.
        /// </param>
        /// <returns>
        /// The new instance.
        /// </returns>
        public static TestCase New(Action action)
        {
            return new TestCase(action);
        }

        /// <summary>
        /// Creates a new instance of <see cref="TestCase" />.
        /// </summary>
        /// <param name="action">
        /// An action.
        /// </param>
        /// <param name="displayParameterName">
        /// A name of the parameter.
        /// </param>
        /// <returns>
        /// The new instance.
        /// </returns>
        public static TestCase New(Action action, string displayParameterName)
        {
            return new TestCase(action, displayParameterName);
        }

        /// <summary>
        /// Creates a new instance of <see cref="TestCase" />.
        /// </summary>
        /// <typeparam name="TArg">A type of the first argument.</typeparam>
        /// <param name="action">
        /// An action.
        /// </param>
        /// <returns>
        /// The new instance.
        /// </returns>
        public static TestCase New<TArg>(Action<TArg> action)
        {
            return new TestCase(action);
        }

        /// <summary>
        /// Creates a new instance of <see cref="TestCase" />.
        /// </summary>
        /// <typeparam name="TArg">A type of the first argument.</typeparam>
        /// <param name="action">
        /// An action.
        /// </param>
        /// <param name="displayParameterName">
        /// A name of the parameter.
        /// </param>
        /// <returns>
        /// The new instance.
        /// </returns>
        public static TestCase New<TArg>(Action<TArg> action, string displayParameterName)
        {
            return new TestCase(action, displayParameterName);
        }

        /// <summary>
        /// Converts the instance to an xUnit.net ITestCommand instance.
        /// </summary>
        /// <param name="method">
        /// The method adorned by a <see cref="FirstClassTestAttribute" />.
        /// </param>
        /// <param name="testFixtureFactory">
        /// A test fixture factory to provide auto data.
        /// </param>
        /// <returns>
        /// An xUnit.net ITestCommand that represents the executable test case.
        /// </returns>
        public ITestCommand ConvertToTestCommand(IMethodInfo method, ITestFixtureFactory testFixtureFactory)
        {
            if (method == null)
                throw new ArgumentNullException("method");

            if (testFixtureFactory == null)
                throw new ArgumentNullException("testFixtureFactory");

            var fixture = new Lazy<ITestFixture>(() => testFixtureFactory.Create(this.Delegate.Method));
            return this.CreateTestCommand(method, fixture);
        }

        private static string GetArgumentValue(string typeName, object argument)
        {
            return typeName + ": " + (argument ?? "(null)");
        }

        private ITestCommand CreateTestCommand(IMethodInfo method, Lazy<ITestFixture> fixture)
        {
            var arguments = this.Delegate.Method.GetParameters()
                .Select(p => fixture.Value.Create(p.ParameterType)).ToArray();

            return new TargetInvocationExceptionUnwrappingCommand(
                new FirstClassCommand(
                    method,
                    this.GetDisplayParameterName(arguments),
                    this.GetAction(arguments)));
        }

        private string GetDisplayParameterName(IList<object> arguments)
        {
            return this.DisplayParameterName ?? string.Join(", ", this.GetArgumentValues(arguments));
        }

        private Action GetAction(object[] arguments)
        {
            var action = this.Delegate as Action;
            if (action != null)
                return action;

            var func = this.Delegate as Func<object>;
            if (func != null)
                return () => func();

            return () => this.Delegate.GetType().GetMethod("Invoke").Invoke(this.Delegate, arguments);
        }

        private IEnumerable<string> GetArgumentValues(IList<object> arguments)
        {
            return this.Delegate.Method.GetParameters()
                .Select(pi => GetArgumentValue(pi.ParameterType.Name, arguments[pi.Position]));
        }
    }
}
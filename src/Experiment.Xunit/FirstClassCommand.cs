using System;
using Xunit.Sdk;

namespace Jwc.Experiment.Xunit
{
    /// <summary>
    /// Represents a test command for first class tests.
    /// </summary>
    public class FirstClassCommand : TestCommand
    {
        private readonly IMethodInfo _method;
        private readonly string _displayParameterName;
        private readonly Action _action;

        /// <summary>
        /// Initializes a new instance of the <see cref="FirstClassCommand" /> class.
        /// </summary>
        /// <param name="method">
        /// The test method which this instance is associated. This will likely be the method
        /// adorned with an <see cref="FirstClassTestAttribute" />.
        /// </param>
        /// <param name="displayParameterName">
        /// A string to show parameters of a test method in test result.
        /// </param>
        /// <param name="action">
        /// The test action to be invoked when the test is executed.
        /// </param>
        public FirstClassCommand(IMethodInfo method, string displayParameterName, Action action) : base(
            EnsureIsNotNull(method),
            MethodUtility.GetDisplayName(method),
            MethodUtility.GetTimeoutParameter(method))
        {
            if (displayParameterName == null)
                throw new ArgumentNullException("displayParameterName");

            if (action == null)
                throw new ArgumentNullException("action");

            _method = method;
            _displayParameterName = displayParameterName;
            _action = action;
            DisplayName += "(" + displayParameterName + ")";
        }

        /// <summary>
        /// Gets the test method.
        /// </summary>
        public IMethodInfo Method
        {
            get { return _method; }
        }

        /// <summary>
        /// Gets a value indicating the string to show parameters of a test method in test result.
        /// </summary>
        public string DisplayParameterName
        {
            get { return _displayParameterName; }
        }

        /// <summary>
        /// Gets the test action.
        /// </summary>
        public Action Action
        {
            get { return _action; }
        }

        /// <summary>
        /// Gets a value indicating whether a test-case instance is created.
        /// </summary>
        /// <value>
        /// <c>true</c> if a test-case instance is created; otherwise, <c>false</c>.
        /// </value>
        public override bool ShouldCreateInstance
        {
            get { return false; }
        }

        /// <summary>
        /// Execute the test action.
        /// </summary>
        /// <param name="testClass">
        /// The test class object.
        /// </param>
        /// <returns>
        /// The result of the execution.
        /// </returns>
        public override MethodResult Execute(object testClass)
        {
            Action();
            return new PassedResult(Method, DisplayName);
        }

        private static IMethodInfo EnsureIsNotNull(IMethodInfo method)
        {
            if (method == null)
                throw new ArgumentNullException("method");

            return method;
        }
    }
}
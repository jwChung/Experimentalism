namespace Jwc.Experiment.Xunit
{
    using System;
    using global::Xunit.Sdk;

    /// <summary>
    /// Represents a test command for first class tests.
    /// </summary>
    public class FirstClassCommand : TestCommand
    {
        private readonly IMethodInfo method;
        private readonly string displayParameterName;
        private readonly Action action;

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
            FirstClassCommand.EnsureIsNotNull(method),
            MethodUtility.GetDisplayName(method),
            MethodUtility.GetTimeoutParameter(method))
        {
            if (displayParameterName == null)
                throw new ArgumentNullException("displayParameterName");

            if (action == null)
                throw new ArgumentNullException("action");

            this.method = method;
            this.displayParameterName = displayParameterName;
            this.action = action;
            this.DisplayName += "(" + displayParameterName + ")";
        }

        /// <summary>
        /// Gets the test method.
        /// </summary>
        public IMethodInfo Method
        {
            get { return this.method; }
        }

        /// <summary>
        /// Gets a value indicating the string to show parameters of a test method in test result.
        /// </summary>
        public string DisplayParameterName
        {
            get { return this.displayParameterName; }
        }

        /// <summary>
        /// Gets the test action.
        /// </summary>
        public Action Action
        {
            get { return this.action; }
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
            this.Action();
            return new PassedResult(this.Method, this.DisplayName);
        }

        private static IMethodInfo EnsureIsNotNull(IMethodInfo method)
        {
            if (method == null)
                throw new ArgumentNullException("method");

            return method;
        }
    }
}
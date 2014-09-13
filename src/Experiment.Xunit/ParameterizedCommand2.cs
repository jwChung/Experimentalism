namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Linq;
    using global::Xunit.Extensions;
    using global::Xunit.Sdk;

    /// <summary>
    /// Represents a parameterized command.
    /// </summary>
    public class ParameterizedCommand2 : TestCommand
    {
        private readonly ITestCommandContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterizedCommand2"/> class.
        /// </summary>
        /// <param name="context">
        /// Information of this test command.
        /// </param>
        public ParameterizedCommand2(ITestCommandContext context)
            : base(GuardNull(context).ActualMethod, null, MethodUtility.GetTimeoutParameter(context.ActualMethod))
        {
            this.context = context;
        }

        /// <summary>
        /// Gets the Information of this test command.
        /// </summary>
        public ITestCommandContext TestCommandContext
        {
            get { return this.context; }
        }

        /// <summary>
        /// Executes this test command with a test object.
        /// </summary>
        /// <param name="testClass">
        /// The type of the test object.
        /// </param>
        /// <returns>
        /// The result of this test command.
        /// </returns>
        public override MethodResult Execute(object testClass)
        {
            var arguments = this.GetArguments(testClass);
            this.SetDisplayName(arguments);
            this.InvokeTestMethod(testClass, arguments);

            return new PassedResult(this.context.ActualMethod, this.DisplayName);
        }

        private static ITestCommandContext GuardNull(ITestCommandContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            return context;
        }

        private void InvokeTestMethod(object testClass, object[] arguments)
        {
            this.context.ActualMethod.Invoke(testClass, arguments);
        }

        private object[] GetArguments(object testClass)
        {
            var methodContext = this.context.GetMethodContext(testClass);
            return this.context.GetArguments(methodContext).ToArray();
        }

        private void SetDisplayName(object[] arguments)
        {
            this.DisplayName = new TheoryCommand(this.context.TestMethod, arguments).DisplayName;
        }
    }
}
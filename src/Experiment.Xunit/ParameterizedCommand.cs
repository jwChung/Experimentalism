namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Linq;
    using global::Xunit.Extensions;
    using global::Xunit.Sdk;

    /// <summary>
    /// Represents a parameterized command.
    /// </summary>
    public class ParameterizedCommand : TestCommand
    {
        private readonly ITestCommandContext context;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterizedCommand"/> class.
        /// </summary>
        /// <param name="context">
        /// Information of this test command.
        /// </param>
        public ParameterizedCommand(ITestCommandContext context)
            : base(GuardNull(context).TestMethod, null, MethodUtility.GetTimeoutParameter(context.TestMethod))
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
            var methodContext = this.context.GetMethodContext(testClass);
            var actualMethod = Reflector.Wrap(methodContext.ActualMethod);
            var arguments = this.context.GetArguments(methodContext).ToArray();

            this.DisplayName = new TheoryCommand(this.context.TestMethod, arguments).DisplayName;

            actualMethod.Invoke(methodContext.ActualObject, arguments);

            return new PassedResult(actualMethod, this.DisplayName);
        }

        private static ITestCommandContext GuardNull(ITestCommandContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            return context;
        }
    }
}
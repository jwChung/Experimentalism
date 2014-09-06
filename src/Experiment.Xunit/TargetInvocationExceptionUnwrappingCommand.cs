namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Reflection;
    using System.Xml;
    using global::Xunit.Sdk;

    /// <summary>
    /// Represents <see cref="ITestCommand" /> unwrapping <see cref="TargetInvocationException" />.
    /// </summary>
    public class TargetInvocationExceptionUnwrappingCommand : ITestCommand
    {
        private readonly ITestCommand testCommand;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="TargetInvocationExceptionUnwrappingCommand" /> class.
        /// </summary>
        /// <param name="testCommand">
        /// The test command to be unwrapped.
        /// </param>
        public TargetInvocationExceptionUnwrappingCommand(ITestCommand testCommand)
        {
            if (testCommand == null)
                throw new ArgumentNullException("testCommand");

            this.testCommand = testCommand;
        }

        /// <summary>
        /// Gets a value indicating the test command to be unwrapped.
        /// </summary>
        public ITestCommand TestCommand
        {
            get
            {
                return this.testCommand;
            }
        }

        /// <summary>
        /// Gets the display name of the test method.
        /// </summary>
        public string DisplayName
        {
            get
            {
                return this.TestCommand.DisplayName;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the test runner infrastructure should create a new
        /// instance of the test class before running the test.
        /// </summary>
        public bool ShouldCreateInstance
        {
            get
            {
                return this.TestCommand.ShouldCreateInstance;
            }
        }

        /// <summary>
        /// Gets a value indicating amount of time before automatically failing.
        /// </summary>
        /// <returns>
        /// The timeout value, in milliseconds; if zero, the test will not have a timeout.
        /// </returns>
        public int Timeout
        {
            get
            {
                return this.TestCommand.Timeout;
            }
        }

        /// <summary>
        /// Executes the test method.
        /// </summary>
        /// <param name="testClass">
        /// The instance of the test class
        /// </param>
        /// <returns>
        /// Returns information about the test run
        /// </returns>
        public MethodResult Execute(object testClass)
        {
            try
            {
                return this.TestCommand.Execute(testClass);
            }
            catch (TargetInvocationException exception)
            {
                if (exception.InnerException == null)
                    throw;

                throw TargetInvocationExceptionUnwrappingCommand.Unwrap(exception);
            }
        }

        /// <summary>
        /// To the start XML.
        /// </summary>
        /// <returns>
        /// The XML node.
        /// </returns>
        public XmlNode ToStartXml()
        {
            return this.TestCommand.ToStartXml();
        }

        private static Exception Unwrap(TargetInvocationException exception)
        {
            ((Action)Delegate.CreateDelegate(
                typeof(Action),
                exception.InnerException,
                "InternalPreserveStackTrace"))
                .Invoke();

            var e = exception.InnerException as TargetInvocationException;
            if (e != null)
                return TargetInvocationExceptionUnwrappingCommand.Unwrap(e);

            return exception.InnerException;
        }
    }
}
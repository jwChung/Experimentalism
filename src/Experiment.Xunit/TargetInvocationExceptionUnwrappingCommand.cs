using System;
using System.Reflection;
using System.Xml;
using Xunit.Sdk;

namespace Jwc.Experiment.Xunit
{
    /// <summary>
    /// Represents <see cref="ITestCommand" /> unwrapping <see cref="TargetInvocationException" />.
    /// </summary>
    public class TargetInvocationExceptionUnwrappingCommand : ITestCommand
    {
        private readonly ITestCommand _testCommand;

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

            _testCommand = testCommand;
        }

        /// <summary>
        /// Gets a value inicating the test command to be unwrapped.
        /// </summary>
        public ITestCommand TestCommand
        {
            get
            {
                return _testCommand;
            }
        }

        /// <summary>
        /// Gets the display name of the test method.
        /// </summary>
        public string DisplayName
        {
            get
            {
                return TestCommand.DisplayName;
            }
        }

        /// <summary>
        /// Determines if the test runner infrastructure should create a new instance of the test
        /// class before running the test.
        /// </summary>
        public bool ShouldCreateInstance
        {
            get
            {
                return TestCommand.ShouldCreateInstance;
            }
        }

        /// <summary>
        /// Determines if the test should be limited to running a specific amount of time before
        /// automatically failing.
        /// </summary>
        /// <returns>
        /// The timeout value, in milliseconds; if zero, the test will not have a timeout.
        /// </returns>
        public int Timeout
        {
            get
            {
                return TestCommand.Timeout;
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
                return TestCommand.Execute(testClass);
            }
            catch (TargetInvocationException exception)
            {
                if (exception.InnerException != null)
                    throw exception.InnerException;

                throw;
            }
        }

        /// <summary>
        /// To the start XML.
        /// </summary>
        /// <returns />
        public XmlNode ToStartXml()
        {
            return TestCommand.ToStartXml();
        }
    }
}
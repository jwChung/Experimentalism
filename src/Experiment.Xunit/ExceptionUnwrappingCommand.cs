using System;
using System.Reflection;
using System.Xml;
using Xunit.Sdk;

namespace Jwc.Experiment.Xunit
{
    /// <summary>
    ///     Represents <see cref="ITestCommand" /> unwrapping
    ///     <see cref="TargetInvocationException" />.
    /// </summary>
    public class ExceptionUnwrappingCommand : ITestCommand
    {
        /// <summary>
        ///     Gets the display name of the test method.
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        ///     Determines if the test runner infrastructure should create a new instance of the
        ///     test class before running the test.
        /// </summary>
        public bool ShouldCreateInstance { get; private set; }

        /// <summary>
        ///     Determines if the test should be limited to running a specific amount of time before
        ///     automatically failing.
        /// </summary>
        /// <returns>
        ///     The timeout value, in milliseconds; if zero, the test will not have a timeout.
        /// </returns>
        public int Timeout { get; private set; }

        /// <summary>
        ///     Executes the test method.
        /// </summary>
        /// <param name="testClass">
        ///     The instance of the test class
        /// </param>
        /// <returns>
        ///     Returns information about the test run
        /// </returns>
        public MethodResult Execute(object testClass)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     To the start XML.
        /// </summary>
        /// <returns />
        public XmlNode ToStartXml()
        {
            throw new NotImplementedException();
        }
    }
}
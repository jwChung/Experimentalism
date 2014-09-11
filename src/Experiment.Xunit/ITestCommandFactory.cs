namespace Jwc.Experiment.Xunit
{
    using System.Collections.Generic;
    using global::Xunit.Sdk;

    /// <summary>
    /// Represents a test command factory.
    /// </summary>
    public interface ITestCommandFactory
    {
        /// <summary>
        /// Creates test commands.
        /// </summary>
        /// <param name="testMethod">
        /// Information about a test method.
        /// </param>
        /// <returns>
        /// The new test commands.
        /// </returns>
        IEnumerable<ITestCommand> Create(ITestMethodInfo testMethod);
    }
}

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
        /// <param name="context">
        /// The information about a test method.
        /// </param>
        /// <returns>
        /// The new test commands.
        /// </returns>
        IEnumerable<ITestCommand> Create(ITestMethodContext context);
    }
}

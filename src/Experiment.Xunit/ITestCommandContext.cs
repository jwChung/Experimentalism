namespace Jwc.Experiment.Xunit
{
    using System.Collections.Generic;
    using global::Xunit.Sdk;

    /// <summary>
    /// Defines information about a test command.
    /// </summary>
    public interface ITestCommandContext
    {
        /// <summary>
        /// Gets the test method.
        /// </summary>
        IMethodInfo TestMethod { get; }

        /// <summary>
        /// Gets information of the test method.
        /// </summary>
        /// <param name="testObject">
        /// The test object.
        /// </param>
        /// <returns>
        /// The information of the test method.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "object", Justification = "This rule is suppressed because the 'Object' term is appropriate to represent an object of a test class.")]
        ITestMethodContext GetMethodContext(object testObject);

        /// <summary>
        /// Gets test arguments.
        /// </summary>
        /// <param name="context">
        /// Information of the test method.
        /// </param>
        /// <returns>
        /// The test arguments.
        /// </returns>
        IEnumerable<object> GetArguments(ITestMethodContext context);
    }
}

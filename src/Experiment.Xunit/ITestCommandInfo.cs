namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;
    using global::Xunit.Sdk;

    /// <summary>
    /// Defines information about a test command.
    /// </summary>
    public interface ITestCommandInfo
    {
        /// <summary>
        /// Gets the test method.
        /// </summary>
        IMethodInfo TestMethod { get; }

        /// <summary>
        /// Gets the actual method.
        /// </summary>
        IMethodInfo ActualMethod { get; }

        /// <summary>
        /// Gets test arguments.
        /// </summary>
        /// <param name="actualObject">
        /// A actual object.
        /// </param>
        /// <returns>
        /// The test arguments.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "object", Justification = "This rule is suppressed because the 'Object' term is appropriate to represent an object of a test class.")]
        IEnumerable<object> GetArguments(object actualObject);
    }
}
namespace Jwc.Experiment.Xunit
{
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Represents a test-case.
    /// </summary>
    public interface ITestCase
    {
        /// <summary>
        /// Gets the test object.
        /// </summary>
        object Target { get; }

        /// <summary>
        /// Gets the test method to be actually executed.
        /// </summary>
        MethodInfo TestMethod { get; }

        /// <summary>
        /// Gets the arguments specified with explicit values.
        /// </summary>
        IEnumerable<object> Arguments { get; }
    }
}

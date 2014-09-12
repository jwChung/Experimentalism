namespace Jwc.Experiment.Xunit
{
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Represents a test-case.
    /// </summary>
    public interface ITestCase2
    {
        /// <summary>
        /// Gets the arguments specified with explicit values.
        /// </summary>
        IEnumerable<object> Arguments { get; }

        /// <summary>
        /// Gets the test method to be actually executed.
        /// </summary>
        MethodInfo TestMethod { get; }

        /// <summary>
        /// Gets the test object declaring a actual test method.
        /// </summary>
        object Target { get; } // TODO: delete
    }
}

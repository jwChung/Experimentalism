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
        /// Gets the explicit arguments.
        /// </summary>
        IEnumerable<object> ExplicitArguments { get; }

        /// <summary>
        /// Gets the test method to be actually executed.
        /// </summary>
        MethodInfo ActualMethod { get; }

        /// <summary>
        /// Gets the test object declaring a actual test method.
        /// </summary>
        object ActualObject { get; }
    }
}

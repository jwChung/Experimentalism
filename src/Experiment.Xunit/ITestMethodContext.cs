namespace Jwc.Experiment.Xunit
{
    using System.Reflection;

    /// <summary>
    /// Defines information about a test method.
    /// </summary>
    public interface ITestMethodContext
    {
        /// <summary>
        /// Gets the test method adorned with a test attribute.
        /// </summary>
        MethodInfo TestMethod { get; }

        /// <summary>
        /// Gets the test method to be actually executed.
        /// </summary>
        MethodInfo ActualMethod { get; }

        /// <summary>
        /// Gets the test object declaring a adorned test method.
        /// </summary>
        object TestObject { get; }

        /// <summary>
        /// Gets the test object declaring a actual test method.
        /// </summary>
        object ActualObject { get; }
    }
}

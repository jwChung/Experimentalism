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
        MethodInfo Adorned { get; }

        /// <summary>
        /// Gets the test method to be actually executed.
        /// </summary>
        MethodInfo Actual { get; }

        /// <summary>
        /// Gets the test object declaring a adorned test method.
        /// </summary>
        object AdornedObject { get; }

        /// <summary>
        /// Gets the test object declaring a actual test method.
        /// </summary>
        object ActualObject { get; }
    }
}

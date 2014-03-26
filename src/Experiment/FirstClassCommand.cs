using Xunit.Sdk;

namespace Jwc.Experiment
{
    /// <summary>
    /// Represents a test command for first class tests.
    /// </summary>
    public class FirstClassCommand : FactCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FirstClassCommand"/> class.
        /// </summary>
        /// <param name="method">The test method.</param>
        public FirstClassCommand(IMethodInfo method) : base(method)
        {
        }
    }
}
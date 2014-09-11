namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using global::Xunit.Sdk;

    /// <summary>
    /// Represents a factory to create <see cref="FactCommand"/>.
    /// </summary>
    public class FactCommandFactory : ITestCommandFactory
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
        public IEnumerable<ITestCommand> Create(ITestMethodContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            if (context.Actual.ReturnType != typeof(void) || context.Actual.GetParameters().Any())
                yield break;

            yield return new FactCommand(Reflector.Wrap(context.Actual));
        }
    }
}

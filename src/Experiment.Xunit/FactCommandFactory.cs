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
        public IEnumerable<ITestCommand> Create(ITestMethodInfo context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            if (context.ActualMethod.ReturnType != typeof(void) || context.ActualMethod.GetParameters().Any())
                yield break;

            yield return new FactCommand(Reflector.Wrap(context.ActualMethod));
        }
    }
}

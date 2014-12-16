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
        /// <param name="testMethod">
        /// Information about a test method.
        /// </param>
        /// <param name="builderFactory">
        /// A factory of test fixture.
        /// </param>
        /// <returns>
        /// The new test commands.
        /// </returns>
        public IEnumerable<ITestCommand> Create(IMethodInfo testMethod, ISpecimenBuilderFactory builderFactory)
        {
            if (testMethod == null)
                throw new ArgumentNullException("testMethod");

            if (testMethod.MethodInfo.GetParameters().Any())
                yield break;

            yield return new FactCommand(testMethod);
        }
    }
}

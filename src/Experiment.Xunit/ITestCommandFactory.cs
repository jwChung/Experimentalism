﻿namespace Jwc.Experiment.Xunit
{
    using System.Collections.Generic;
    using global::Xunit.Sdk;

    /// <summary>
    /// Represents a test command factory.
    /// </summary>
    public interface ITestCommandFactory
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
        IEnumerable<ITestCommand> Create(IMethodInfo testMethod, ISpecimenBuilderFactory builderFactory);
    }
}

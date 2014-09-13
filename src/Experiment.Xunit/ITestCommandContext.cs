﻿namespace Jwc.Experiment.Xunit
{
    using System.Collections.Generic;
    using global::Xunit.Sdk;

    /// <summary>
    /// Defines information about a test command.
    /// </summary>
    public interface ITestCommandContext
    {
        /// <summary>
        /// Gets the test method.
        /// </summary>
        IMethodInfo TestMethod { get; }

        /// <summary>
        /// Gets the actual method.
        /// </summary>
        IMethodInfo ActualMethod { get; }

        /// <summary>
        /// Gets information of the test method.
        /// </summary>
        /// <param name="actualObject">
        /// The actual object.
        /// </param>
        /// <returns>
        /// The information of the test method.
        /// </returns>
        ITestMethodContext GetMethodContext(object actualObject);

        /// <summary>
        /// Gets test arguments.
        /// </summary>
        /// <param name="context">
        /// Information of the test method.
        /// </param>
        /// <returns>
        /// The test arguments.
        /// </returns>
        IEnumerable<object> GetArguments(ITestMethodContext context);
    }
}

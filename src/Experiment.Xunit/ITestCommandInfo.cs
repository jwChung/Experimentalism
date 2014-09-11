﻿namespace Jwc.Experiment.Xunit
{
    using System;
    using global::Xunit.Sdk;

    /// <summary>
    /// Defines information about a test command.
    /// </summary>
    public interface ITestCommandInfo
    {
        /// <summary>
        /// Gets the test method.
        /// </summary>
        IMethodInfo TestMethod { get; }

        /// <summary>
        /// Gets test arguments.
        /// </summary>
        /// <param name="testObject">
        /// A test object.
        /// </param>
        /// <returns>
        /// The test arguments.
        /// </returns>
        object[] GetArguments(object testObject);
    }
}
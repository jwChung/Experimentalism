using System;
using System.Reflection;

namespace Jwc.Experiment
{
    /// <summary>
    ///     Represents a test fixture created by <see cref="Func{TResult}" />
    /// </summary>
    public class FuncTestFixtureFactory : ITestFixtureFactory
    {
        /// <summary>
        ///     Creates a test fixture.
        /// </summary>
        /// <param name="testMethod">
        ///     The test method in which the test fixture will be used.
        /// </param>
        /// <returns>
        ///     The test fixture.
        /// </returns>
        public ITestFixture Create(MethodInfo testMethod)
        {
            throw new NotImplementedException();
        }
    }
}
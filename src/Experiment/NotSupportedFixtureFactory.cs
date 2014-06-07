using System;
using System.Reflection;

namespace Jwc.Experiment
{
    /// <summary>
    ///     Represents test fixture factory which throws
    ///     <see cref="NotSupportedException" /> when the <code>Create</code>
    ///     method is called.
    /// </summary>
    public class NotSupportedFixtureFactory : ITestFixtureFactory
    {
        /// <summary>
        ///     Throws <see cref="NotSupportedException" />.
        /// </summary>
        /// <param name="testMethod">
        ///     The test method in which the test fixture will be used.
        /// </param>
        public ITestFixture Create(MethodInfo testMethod)
        {
            throw new NotSupportedException(
                "To create auto data, call 'TestFixtureFactory.SetCurrent' to set 'ITestFixtureFactory', or override " +
                "'CreateTestFixture' of 'TestAttribute' or 'FirstClassTestAttribute' to create an instance of " +
                "'ITestFixture'.");
        }
    }
}
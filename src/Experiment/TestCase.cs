using System;
using Xunit.Sdk;

namespace Jwc.Experiment
{
    /// <summary>
    /// Represents a weakly-typed test case that can be turned into an
    /// xUnit.net ITestCommand when returned from a test method adorned with
    /// the <see cref="DefaultFirstClassTheoremAttribute" />.
    /// </summary>
    public class TestCase : ITestCase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase"/> class.
        /// </summary>
        /// <param name="action">The test action.</param>
        public TestCase(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase"/> class.
        /// </summary>
        /// <param name="func">The test function.</param>
        public TestCase(Func<object> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }
        }

        /// <summary>
        /// Converts the instance to an xUnit.net ITestCommand instance.
        /// </summary>
        /// <param name="method">
        /// The method adorned by a <see cref="DefaultFirstClassTheoremAttribute" />.
        /// </param>
        /// <param name="fixtureFactory">
        /// A test fixture factory to provide auto data.
        /// </param>
        /// <returns>
        /// An xUnit.net ITestCommand that represents the executable test case.
        /// </returns>
        public ITestCommand ConvertToTestCommand(IMethodInfo method, ITestFixtureFactory fixtureFactory)
        {
            throw new System.NotImplementedException();
        }
    }
}
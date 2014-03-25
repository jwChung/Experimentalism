using System;
using Xunit.Sdk;

namespace Jwc.Experiment
{
    /// <summary>
    /// Represents a weakly-typed test case that can be turned into an
    /// xUnit.net ITestCommand when returned from a test method adorned with
    /// the <see cref="NaiveFirstClassTheoremAttribute" />.
    /// </summary>
    public class TestCase : ITestCase
    {
        private TestCase()
        {
        }

        /// <summary>
        /// Creates a new instance of <see cref="ITestCase"/>.
        /// </summary>
        /// <param name="delegate">
        /// The delegate to be invoked when the test is executed.
        /// </param>
        /// <returns>The created instance.</returns>
        public static ITestCase New(Action @delegate)
        {
            return new TestCase();
        }

        /// <summary>
        /// Converts the instance to an xUnit.net ITestCommand instance.
        /// </summary>
        /// <param name="method">The method adorned by a <see cref="NaiveFirstClassTheoremAttribute" />.</param>
        /// <param name="testFixture">A test fixture to provide auto data.</param>
        /// <returns>
        /// An xUnit.net ITestCommand that represents the executable test case.
        /// </returns>
        public ITestCommand ConvertToTestCommand(IMethodInfo method, ITestFixture testFixture)
        {
            throw new NotImplementedException();
        }
    }
}
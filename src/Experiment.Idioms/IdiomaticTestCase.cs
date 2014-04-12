using System;
using System.Reflection;
using Xunit.Sdk;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents a idiomatic test case that can be turned into an
    /// xUnit.net ITestCommand when returned from a test method adorned with
    /// the <see cref="BaseFirstClassTheoremAttribute" />.
    /// </summary>
    public class IdiomaticTestCase : ITestCase
    {
        /// <summary>
        /// Converts the instance to an xUnit.net ITestCommand instance.
        /// </summary>
        /// <param name="method">
        /// The method adorned by a <see cref="BaseFirstClassTheoremAttribute" />.
        /// </param>
        /// <param name="fixtureFactory">
        /// A test fixture factory to provide auto data.
        /// </param>
        /// <returns>
        /// An xUnit.net ITestCommand that represents the executable test case.
        /// </returns>
        public ITestCommand ConvertToTestCommand(
            IMethodInfo method, Func<MethodInfo, ITestFixture> fixtureFactory)
        {
            throw new NotImplementedException();
        }
    }
}
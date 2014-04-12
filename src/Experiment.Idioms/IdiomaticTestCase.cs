using System;
using System.Reflection;
using Ploeh.Albedo;
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
        /// Initializes a new instance of the <see cref="IdiomaticTestCase"/> class.
        /// </summary>
        /// <param name="element">The reflection element to be verified with the assertion.</param>
        /// <param name="assetionFactory">The assetion factory.</param>
        public IdiomaticTestCase(
            IReflectionElement element,
            Func<ITestFixture, IReflectionVisitor<object>> assetionFactory)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            
            if (assetionFactory == null)
            {
                throw new ArgumentNullException("assetionFactory");
            }
        }

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
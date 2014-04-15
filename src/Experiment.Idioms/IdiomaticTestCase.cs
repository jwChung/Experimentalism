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
        private readonly IReflectionElement _element;
        private readonly IAssertionFactory _assertionFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="IdiomaticTestCase"/> class.
        /// </summary>
        /// <param name="element">The reflection element to be verified with the assertion.</param>
        /// <param name="assertionFactory">The assetion factory.</param>
        public IdiomaticTestCase(
            IReflectionElement element,
            IAssertionFactory assertionFactory)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }

            if (assertionFactory == null)
            {
                throw new ArgumentNullException("assertionFactory");
            }

            _element = element;
            _assertionFactory = assertionFactory;
        }

        /// <summary>
        /// Gets the reflection element.
        /// </summary>
        public IReflectionElement ReflectionElement
        {
            get
            {
                return _element;
            }
        }

        /// <summary>
        /// Gets the assertion factory.
        /// </summary>
        public IAssertionFactory AssertionFactory
        {
            get
            {
                return _assertionFactory;
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
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }

            if (fixtureFactory == null)
            {
                throw new ArgumentNullException("fixtureFactory");
            }

            return new IdiomaticTestCommand(
                method,
                ReflectionElement,
                AssertionFactory.Create(fixtureFactory(method.MethodInfo)));
        }
    }
}
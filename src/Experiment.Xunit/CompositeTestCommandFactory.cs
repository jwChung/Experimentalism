namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using global::Xunit.Sdk;

    /// <summary>
    /// Represents composite factory to create test commands.
    /// </summary>
    public class CompositeTestCommandFactory : ITestCommandFactory
    {
        private readonly ITestCommandFactory[] factories;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeTestCommandFactory"/> class.
        /// </summary>
        /// <param name="factories">
        /// Factories to create test commands.
        /// </param>
        public CompositeTestCommandFactory(params ITestCommandFactory[] factories)
        {
            if (factories == null)
                throw new ArgumentNullException("factories");

            this.factories = factories;
        }

        /// <summary>
        /// Gets the factories to create test commands.
        /// </summary>
        public IEnumerable<ITestCommandFactory> TestCommandFactories
        {
            get { return this.factories; }
        }

        /// <summary>
        /// Creates test commands.
        /// </summary>
        /// <param name="testMethod">
        /// Information about a test method.
        /// </param>
        /// <param name="fixtureFactory">
        /// A factory of test fixture.
        /// </param>
        /// <returns>
        /// The new test commands.
        /// </returns>
        public IEnumerable<ITestCommand> Create(IMethodInfo testMethod, ITestFixtureFactory fixtureFactory)
        {
            return this.factories
                .Select(f => f.Create(testMethod, fixtureFactory))
                .First(c => c.Any());
        }
    }
}

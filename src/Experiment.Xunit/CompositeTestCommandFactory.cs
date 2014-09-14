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
            foreach (var factory in this.factories)
            {
                int count = 0;
                var testCommands = factory.Create(testMethod, fixtureFactory);
                foreach (var testCommand in testCommands)
                {
                    count++;
                    yield return testCommand;
                }

                if (count != 0)
                    yield break;
            }
        }
    }
}

namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;
    using global::Xunit;
    using global::Xunit.Sdk;

    /// <summary>
    /// Represents base attribute to indicate that a given method is a test method.
    /// </summary>
    public abstract class TestBaseAttribute : FactAttribute, ITestFixtureFactory
    {
        private readonly ITestCommandFactory factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestBaseAttribute"/> class.
        /// </summary>
        protected TestBaseAttribute()
            : this(new CompositeTestCommandFactory(
                new TestCaseCommandFactory2(),
                new ParameterizedCommandFactory(),
                new FactCommandFactory()))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestBaseAttribute"/> class.
        /// </summary>
        /// <param name="factory">
        /// A factory to create test commands.
        /// </param>
        protected TestBaseAttribute(ITestCommandFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");

            this.factory = factory;
        }

        /// <summary>
        /// Gets the factory to create test commands.
        /// </summary>
        public ITestCommandFactory TestCommandFactory
        {
            get { return this.factory; }
        }

        ITestFixture ITestFixtureFactory.Create(ITestMethodContext context)
        {
            return this.Create(context);
        }

        /// <summary>
        /// Enumerates the test commands represented by this test method. Derived classes should
        /// override this method to return instances of <see cref="T:Xunit.Sdk.ITestCommand" />,
        /// one per execution of a test method.
        /// </summary>
        /// <param name="method">
        /// The test method.
        /// </param>
        /// <returns>
        /// The test commands which will execute the test runs for the given method.
        /// </returns>
        protected override IEnumerable<ITestCommand> EnumerateTestCommands(IMethodInfo method)
        {
            return new TestCommandCollection(method, this.factory.Create(method, this));
        }

        /// <summary>
        /// Creates a test fixture.
        /// </summary>
        /// <param name="context">
        /// The test information about a test method.
        /// </param>
        /// <returns>
        /// The test fixture.
        /// </returns>
        protected abstract ITestFixture Create(ITestMethodContext context);
    }
}

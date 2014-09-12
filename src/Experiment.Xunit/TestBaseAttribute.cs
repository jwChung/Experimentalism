namespace Jwc.Experiment.Xunit
{
    using System;
    using global::Xunit;

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
                new TestCaseCommandFactory(),
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

        ITestFixture ITestFixtureFactory.Create(ITestMethodInfo context)
        {
            return this.Create(context);
        }

        /// <summary>
        /// Creates a test fixture.
        /// </summary>
        /// <param name="testMethod">
        /// The test information about a test method.
        /// </param>
        /// <returns>
        /// The test fixture.
        /// </returns>
        protected abstract ITestFixture Create(ITestMethodInfo testMethod);
    }
}

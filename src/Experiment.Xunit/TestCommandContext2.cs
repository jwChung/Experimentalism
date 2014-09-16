namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;
    using global::Xunit.Sdk;

    public abstract class TestCommandContext2 : ITestCommandContext
    {
        private readonly ITestFixtureFactory factory;
        private readonly IEnumerable<object> arguments;

        public TestCommandContext2(ITestFixtureFactory factory, IEnumerable<object> arguments)
        {
            this.factory = factory;
            this.arguments = arguments;
        }

        /// <summary>
        /// Gets the factory to create test fixture.
        /// </summary>
        public ITestFixtureFactory TestFixtureFactory
        {
            get { return this.factory; }
        }

        /// <summary>
        /// Gets the explicit arguments of the test.
        /// </summary>
        public IEnumerable<object> ExplicitArguments
        {
            get { return this.arguments; }
        }

        public abstract IMethodInfo TestMethod { get; }

        public abstract ITestMethodContext GetMethodContext(object testObject);

        public abstract ITestMethodContext GetStaticMethodContext();

        public IEnumerable<object> GetArguments(ITestMethodContext context)
        {
            throw new NotImplementedException();
        }
    }
}
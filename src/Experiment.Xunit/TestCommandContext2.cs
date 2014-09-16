namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using global::Xunit.Sdk;

    /// <summary>
    /// Represents a base class for test command context.
    /// </summary>
    public abstract class TestCommandContext2 : ITestCommandContext
    {
        private readonly ITestFixtureFactory factory;
        private readonly IEnumerable<object> arguments;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCommandContext2"/> class.
        /// </summary>
        /// <param name="factory">
        /// A factory to create test fixture.
        /// </param>
        /// <param name="arguments">
        /// Explicit arguments of the test method.
        /// </param>
        protected TestCommandContext2(ITestFixtureFactory factory, IEnumerable<object> arguments)
        {
            if (factory == null)
                throw new ArgumentNullException("factory");

            if (arguments == null)
                throw new ArgumentNullException("arguments");

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

        /// <summary>
        /// Gets the test method.
        /// </summary>
        public abstract IMethodInfo TestMethod { get; }

        /// <summary>
        /// Gets information of the test method.
        /// </summary>
        /// <param name="testObject">
        /// The test object.
        /// </param>
        /// <returns>
        /// The information of the test method.
        /// </returns>
        public abstract ITestMethodContext GetMethodContext(object testObject);

        /// <summary>
        /// Gets information of the static test method.
        /// </summary>
        /// <returns>
        /// The information of the static test method.
        /// </returns>
        public abstract ITestMethodContext GetStaticMethodContext();

        /// <summary>
        /// Gets test arguments.
        /// </summary>
        /// <param name="context">
        /// Information of the test method.
        /// </param>
        /// <returns>
        /// The test arguments.
        /// </returns>
        public IEnumerable<object> GetArguments(ITestMethodContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            var parameters = context.ActualMethod.GetParameters();
            var explicitArguments = this.arguments.ToArray();

            if (explicitArguments.Length > parameters.Length)
                throw new InvalidOperationException(string.Format(
                    CultureInfo.CurrentCulture,
                    "Expected {0} parameters, got {1} parameters",
                    parameters.Length,
                    explicitArguments.Length));

            if (explicitArguments.Length == parameters.Length)
                return explicitArguments;

            var fixture = this.factory.Create(context);
            var autoArguments = parameters.Skip(explicitArguments.Length)
                .Select(p => fixture.Create(p.ParameterType));

            return explicitArguments.Concat(autoArguments);
        }
    }
}
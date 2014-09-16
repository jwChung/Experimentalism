namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
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
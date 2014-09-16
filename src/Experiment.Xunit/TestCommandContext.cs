namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using global::Xunit.Sdk;

    /// <summary>
    /// Represents information of a test command.
    /// </summary>
    public class TestCommandContext : ITestCommandContext
    {
        private IMethodInfo testMethod;
        private IMethodInfo actualMethod;
        private object actualObject;
        private ITestFixtureFactory factory;
        private IEnumerable<object> arguments;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCommandContext"/> class.
        /// </summary>
        /// <param name="testMethod">
        /// A test method.
        /// </param>
        /// <param name="factory">
        /// A factory to create test fixture.
        /// </param>
        /// <param name="arguments">
        /// Explicit arguments of the test method.
        /// </param>
        public TestCommandContext(IMethodInfo testMethod, ITestFixtureFactory factory, IEnumerable<object> arguments)
        {
            if (testMethod == null)
                throw new ArgumentNullException("testMethod");

            if (factory == null)
                throw new ArgumentNullException("factory");

            if (arguments == null)
                throw new ArgumentNullException("arguments");

            this.testMethod = testMethod;
            this.factory = factory;
            this.arguments = arguments;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCommandContext"/> class.
        /// </summary>
        /// <param name="testMethod">
        /// A test method.
        /// </param>
        /// <param name="actualMethod">
        /// A actual method.
        /// </param>
        /// <param name="factory">
        /// A factory to create test fixture.
        /// </param>
        /// <param name="arguments">
        /// Explicit arguments of the actual method.
        /// </param>
        public TestCommandContext(
            IMethodInfo testMethod,
            IMethodInfo actualMethod,
            ITestFixtureFactory factory,
            IEnumerable<object> arguments)
            : this(testMethod, factory, arguments)
        {
            if (actualMethod == null)
                throw new ArgumentNullException("actualMethod");

            this.actualMethod = actualMethod;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCommandContext"/> class.
        /// </summary>
        /// <param name="testMethod">
        /// A test method.
        /// </param>
        /// <param name="actualMethod">
        /// A actual method.
        /// </param>
        /// <param name="actualObject">
        /// The test object.
        /// </param>
        /// <param name="factory">
        /// A factory to create test fixture.
        /// </param>
        /// <param name="arguments">
        /// Explicit arguments of the actual method.
        /// </param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "object", Justification = "This rule is suppressed because the 'Object' term is appropriate to represent an object of a test class.")]
        public TestCommandContext(
            IMethodInfo testMethod,
            IMethodInfo actualMethod,
            object actualObject,
            ITestFixtureFactory factory,
            IEnumerable<object> arguments)
            : this(testMethod, actualMethod, factory, arguments)
        {
            if (actualObject == null)
                throw new ArgumentNullException("actualObject");

            this.actualObject = actualObject;
        }

        /// <summary>
        /// Gets the test method.
        /// </summary>
        public IMethodInfo TestMethod
        {
            get { return this.testMethod; }
        }

        /// <summary>
        /// Gets the actual method.
        /// </summary>
        public IMethodInfo ActualMethod
        {
            get { return this.actualMethod; }
        }

        /// <summary>
        /// Gets the actual object.
        /// </summary>
        public object ActualObject
        {
            get { return this.actualObject; }
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
        /// Gets information of the test method.
        /// </summary>
        /// <param name="testObject">
        /// The test object.
        /// </param>
        /// <returns>
        /// The information of the test method.
        /// </returns>
        public ITestMethodContext GetMethodContext(object testObject)
        {
            if (this.actualMethod == null)
                return new TestMethodContext(
                    this.testMethod.MethodInfo, this.testMethod.MethodInfo, testObject, testObject);

            return new TestMethodContext(
                    this.testMethod.MethodInfo, this.actualMethod.MethodInfo, testObject, this.actualObject);
        }

        /// <summary>
        /// Gets information of the static test method.
        /// </summary>
        /// <returns>
        /// The information of the static test method.
        /// </returns>
        public ITestMethodContext GetStaticMethodContext()
        {
            throw new NotSupportedException();
        }

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

        private class TestMethodContext : ITestMethodContext
        {
            private readonly MethodInfo testMethod;
            private readonly MethodInfo actualMethod;
            private readonly object testObject;
            private readonly object actualObject;

            public TestMethodContext(
                MethodInfo testMethod,
                MethodInfo actualMethod,
                object testObject,
                object actualObject)
            {
                this.testMethod = testMethod;
                this.actualMethod = actualMethod;
                this.testObject = testObject;
                this.actualObject = actualObject;
            }

            public MethodInfo TestMethod
            {
                get { return this.testMethod; }
            }

            public MethodInfo ActualMethod
            {
                get { return this.actualMethod; }
            }

            public object TestObject
            {
                get { return this.testObject; }
            }

            public object ActualObject
            {
                get { return this.actualObject; }
            }
        }
    }
}
namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using global::Xunit.Sdk;

    /// <summary>
    /// Represents information of a test.
    /// </summary>
    public class TestInfo : ITestMethodInfo, ITestCommandInfo
    {
        private readonly MethodInfo testMethod;
        private readonly MethodInfo actualMethod;
        private readonly object testObject;
        private readonly object actualObject;
        private readonly ITestFixtureFactory factory;
        private readonly object[] arguments;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestInfo"/> class.
        /// </summary>
        /// <param name="testMethod">
        /// The test method.
        /// </param>
        /// <param name="factory">
        /// The factory of test fixture.
        /// </param>
        /// <param name="arguments">
        /// The arguments.
        /// </param>
        public TestInfo(
            MethodInfo testMethod,
            ITestFixtureFactory factory,
            IEnumerable<object> arguments)
        {
            if (testMethod == null)
                throw new ArgumentNullException("testMethod");

            if (factory == null)
                throw new ArgumentNullException("factory");

            if (arguments == null)
                throw new ArgumentNullException("arguments");

            this.testMethod = testMethod;
            this.actualMethod = testMethod;
            this.factory = factory;
            this.arguments = arguments.ToArray();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestInfo"/> class.
        /// </summary>
        /// <param name="testMethod">
        /// The test method.
        /// </param>
        /// <param name="actualMethod">
        /// The actual method.
        /// </param>
        /// <param name="actualClass">
        /// The actual object.
        /// </param>
        /// <param name="factory">
        /// The factory of test fixture.
        /// </param>
        /// <param name="arguments">
        /// The arguments.
        /// </param>
        public TestInfo(
            MethodInfo testMethod,
            MethodInfo actualMethod,
            object actualClass,
            ITestFixtureFactory factory,
            IEnumerable<object> arguments)
        {
            if (testMethod == null)
                throw new ArgumentNullException("testMethod");

            if (actualMethod == null)
                throw new ArgumentNullException("actualMethod");

            if (actualClass == null)
                throw new ArgumentNullException("actualClass");

            if (factory == null)
                throw new ArgumentNullException("factory");

            if (arguments == null)
                throw new ArgumentNullException("arguments");

            this.testMethod = testMethod;
            this.actualMethod = actualMethod;
            this.actualObject = actualClass;
            this.factory = factory;
            this.arguments = arguments.ToArray();
        }

        private TestInfo(
            object testObject,
            TestInfo other)
        {
            this.testMethod = other.testMethod;
            this.actualMethod = other.actualMethod;
            this.testObject = testObject;
            this.actualObject = other.actualObject ?? testObject;
        }

        /// <summary>
        /// Gets the test method adorned with a test attribute.
        /// </summary>
        public MethodInfo TestMethod
        {
            get { return this.testMethod; }
        }

        /// <summary>
        /// Gets the test method to be actually executed.
        /// </summary>
        public MethodInfo ActualMethod
        {
            get { return this.actualMethod; }
        }

        object ITestMethodInfo.TestObject
        {
            get { return this.testObject; }
        }

        /// <summary>
        /// Gets the test object declaring a actual test method.
        /// </summary>
        public object ActualObject
        {
            get { return this.actualObject; }
        }

        /// <summary>
        /// Gets the factory of test fixture.
        /// </summary>
        public ITestFixtureFactory TestFixtureFactory
        {
            get { return this.factory; }
        }

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        public IEnumerable<object> Arguments
        {
            get { return this.arguments; }
        }

        IMethodInfo ITestCommandInfo.TestMethod
        {
            get { return Reflector.Wrap(this.actualMethod); }
        }

        IEnumerable<object> ITestCommandInfo.GetArguments(object testClass)
        {
            var parameters = this.actualMethod.GetParameters();
            if (parameters.Length < this.arguments.Length)
                throw new InvalidOperationException(string.Format(
                    CultureInfo.CurrentCulture,
                    "Expected {0} parameters, got {1} parameters",
                    parameters.Length,
                    this.arguments.Length));

            if (this.actualMethod.GetParameters().Length == this.arguments.Length)
                return this.arguments;

            return this.GetArguments(testClass);
        }

        private IEnumerable<object> GetArguments(object testClass)
        {
            return this.arguments.Concat(this.GetAutoData(testClass));
        }

        private IEnumerable<object> GetAutoData(object testClass)
        {
            var fixture = this.factory.Create(new TestInfo(testClass, this));
            return this.actualMethod.GetParameters()
                .Skip(this.arguments.Length)
                .Select(p => fixture.Create(p.ParameterType));
        }
    }
}

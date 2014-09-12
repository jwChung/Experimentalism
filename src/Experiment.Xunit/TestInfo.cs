namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using global::Xunit.Sdk;

    /// <summary>
    /// Represents information of a test.
    /// </summary>
    public class TestInfo : ITestMethodInfo, ITestCommandInfo
    {
        private readonly MethodInfo testMethod;
        private readonly MethodInfo actualMethod;
        private readonly object actualObject;
        private readonly ITestFixtureFactory factory;
        private readonly IEnumerable<object> arguments;

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
            this.arguments = arguments;
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
        /// <param name="actualObject">
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
            object actualObject,
            ITestFixtureFactory factory,
            IEnumerable<object> arguments)
        {
            if (testMethod == null)
                throw new ArgumentNullException("testMethod");

            if (actualMethod == null)
                throw new ArgumentNullException("actualMethod");

            if (actualObject == null)
                throw new ArgumentNullException("actualObject");

            if (factory == null)
                throw new ArgumentNullException("factory");

            if (arguments == null)
                throw new ArgumentNullException("arguments");

            this.testMethod = testMethod;
            this.actualMethod = actualMethod;
            this.actualObject = actualObject;
            this.factory = factory;
            this.arguments = arguments;
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

        /// <summary>
        /// Gets the test object declaring a adorned test method.
        /// </summary>
        public object TestObject
        {
            get { return null; }
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

        object[] ITestCommandInfo.GetArguments(object testObject)
        {
            throw new NotImplementedException();
        }
    }
}

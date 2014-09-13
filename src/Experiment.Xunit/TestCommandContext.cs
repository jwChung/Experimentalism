namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;
    using global::Xunit.Sdk;

    /// <summary>
    /// Represents information of a test command.
    /// </summary>
    public class TestCommandContext : ITestCommandContext
    {
        private IMethodInfo testMethod;
        private IMethodInfo actualMethod;
        private object testObject;
        private ITestFixtureFactory factory;
        private object[] arguments;

        public TestCommandContext(IMethodInfo testMethod, ITestFixtureFactory factory, object[] arguments)
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

        public TestCommandContext(
            IMethodInfo testMethod,
            IMethodInfo actualMethod,
            object testObject,
            ITestFixtureFactory factory,
            object[] arguments)
        {
            if (testMethod == null)
                throw new ArgumentNullException("testMethod");

            if (actualMethod == null)
                throw new ArgumentNullException("actualMethod");

            if (testObject == null)
                throw new ArgumentNullException("testObject");

            if (factory == null)
                throw new ArgumentNullException("factory");

            if (arguments == null)
                throw new ArgumentNullException("arguments");

            this.testMethod = testMethod;
            this.actualMethod = actualMethod;
            this.testObject = testObject;
            this.factory = factory;
            this.arguments = arguments;
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

        public object TestObject
        {
            get { return this.testObject; }
        }

        public ITestFixtureFactory TestFixtureFactory
        {
            get { return this.factory; }
        }

        public IEnumerable<object> ExplicitArguments
        {
            get { return this.arguments; }
        }

        /// <summary>
        /// Gets information of the test method.
        /// </summary>
        /// <param name="actualObject">
        /// The actual object.
        /// </param>
        /// <returns>
        /// The information of the test method.
        /// </returns>
        public ITestMethodContext GetMethodContext(object actualObject)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
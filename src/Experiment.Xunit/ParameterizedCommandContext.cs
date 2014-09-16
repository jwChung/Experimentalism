namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;
    using global::Xunit.Sdk;

    /// <summary>
    /// Represents context of a parameterized command.
    /// </summary>
    public class ParameterizedCommandContext : TestCommandContext2
    {
        private readonly IMethodInfo testMethod;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterizedCommandContext"/> class.
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
        public ParameterizedCommandContext(
            IMethodInfo testMethod, ITestFixtureFactory factory, IEnumerable<object> arguments)
            : base(factory, arguments)
        {
            if (testMethod == null)
                throw new ArgumentNullException("testMethod");

            this.testMethod = testMethod;
        }

        /// <summary>
        /// Gets the test method.
        /// </summary>
        public override IMethodInfo TestMethod
        {
            get { return this.testMethod; }
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
        public override ITestMethodContext GetMethodContext(object testObject)
        {
            if (testObject == null)
                throw new ArgumentNullException("testObject");

            return new TestMethodContext(
                this.testMethod.MethodInfo, this.testMethod.MethodInfo, testObject, testObject);
        }

        /// <summary>
        /// Gets information of the static test method.
        /// </summary>
        /// <returns>
        /// The information of the static test method.
        /// </returns>
        public override ITestMethodContext GetStaticMethodContext()
        {
            return new TestMethodContext(
               this.testMethod.MethodInfo, this.testMethod.MethodInfo, null, null);
        }
    }
}
namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;
    using global::Xunit.Sdk;

    /// <summary>
    /// Represents context of a static test-case command.
    /// </summary>
    public class StaticTestCaseCommandContext : TestCommandContext
    {
        private readonly IMethodInfo testMethod;
        private readonly IMethodInfo actualMethod;

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticTestCaseCommandContext"/> class.
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
        public StaticTestCaseCommandContext(
            IMethodInfo testMethod,
            IMethodInfo actualMethod,
            ISpecimenBuilderFactory factory,
            IEnumerable<object> arguments)
            : base(factory, arguments)
        {
            if (testMethod == null)
                throw new ArgumentNullException("testMethod");

            if (actualMethod == null)
                throw new ArgumentNullException("actualMethod");

            this.testMethod = testMethod;
            this.actualMethod = actualMethod;
        }

        /// <summary>
        /// Gets the test method.
        /// </summary>
        public override IMethodInfo TestMethod
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
                this.testMethod.MethodInfo, this.actualMethod.MethodInfo, testObject, null);
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
                this.testMethod.MethodInfo, this.actualMethod.MethodInfo, null, null);
        }
    }
}
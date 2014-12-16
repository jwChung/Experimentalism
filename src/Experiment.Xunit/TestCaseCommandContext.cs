namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;
    using global::Xunit.Sdk;

    /// <summary>
    /// Represents context of a test-case command.
    /// </summary>
    public class TestCaseCommandContext : TestCommandContext
    {
        private readonly IMethodInfo testMethod;
        private readonly IMethodInfo actualMethod;
        private readonly object actualObject;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseCommandContext"/> class.
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "object", Justification = "This rule is suppressed because the 'Object' term is appropriate to represent an object of a actual class.")]
        public TestCaseCommandContext(
            IMethodInfo testMethod,
            IMethodInfo actualMethod,
            object actualObject,
            ISpecimenBuilderFactory factory,
            IEnumerable<object> arguments)
            : base(factory, arguments)
        {
            if (testMethod == null)
                throw new ArgumentNullException("testMethod");

            if (actualMethod == null)
                throw new ArgumentNullException("actualMethod");

            if (actualObject == null)
                throw new ArgumentNullException("actualObject");

            this.testMethod = testMethod;
            this.actualMethod = actualMethod;
            this.actualObject = actualObject;
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
        /// Gets the actual object.
        /// </summary>
        public object ActualObject
        {
            get { return this.actualObject; }
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
                this.testMethod.MethodInfo,
                this.actualMethod.MethodInfo,
                testObject,
                this.actualObject);
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
                this.testMethod.MethodInfo,
                this.actualMethod.MethodInfo,
                null,
                this.actualObject);
        }
    }
}
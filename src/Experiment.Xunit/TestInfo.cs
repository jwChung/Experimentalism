﻿namespace Jwc.Experiment.Xunit
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
        private readonly bool isSameObject;
        private readonly MethodInfo testMethod;
        private readonly MethodInfo actualMethod;
        private readonly object testObj;
        private readonly object actualObj;
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
            this.isSameObject = true;
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
        /// <param name="testObject">
        /// The test object.
        /// </param>
        /// <param name="factory">
        /// The factory of test fixture.
        /// </param>
        /// <param name="arguments">
        /// The arguments.
        /// </param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1720:IdentifiersShouldNotContainTypeNames", MessageId = "object", Justification = "This rule is suppressed because the 'Object' term is appropriate to represent an object of a test class.")]
        public TestInfo(
            MethodInfo testMethod,
            MethodInfo actualMethod,
            object testObject,
            ITestFixtureFactory factory,
            IEnumerable<object> arguments)
        {
            if (testMethod == null)
                throw new ArgumentNullException("testMethod");

            if (actualMethod == null)
                throw new ArgumentNullException("actualMethod");

            if (factory == null)
                throw new ArgumentNullException("factory");

            if (arguments == null)
                throw new ArgumentNullException("arguments");

            this.testMethod = testMethod;
            this.actualMethod = actualMethod;
            this.testObj = testObject;
            this.factory = factory;
            this.arguments = arguments.ToArray();
            this.isSameObject = false;
        }

        private TestInfo(object actualObject, TestInfo other)
        {
            this.testMethod = other.testMethod;
            this.actualMethod = other.actualMethod;
            this.testObj = other.testObj ?? (other.isSameObject ? actualObject : null);
            this.actualObj = actualObject;
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
            get { return this.testObj; }
        }

        /// <summary>
        /// Gets the test object declaring a actual test method.
        /// </summary>
        public object ActualObject
        {
            get { return this.actualObj; }
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
        public IEnumerable<object> ExplicitArguments
        {
            get { return this.arguments; }
        }

        IMethodInfo ITestCommandInfo.TestMethod
        {
            get { return Reflector.Wrap(this.testMethod); }
        }

        IMethodInfo ITestCommandInfo.ActualMethod
        {
            get { return Reflector.Wrap(this.actualMethod); }
        }

        /// <summary>
        /// Gets test arguments.
        /// </summary>
        /// <param name="actualObject">
        /// An actual object.
        /// </param>
        /// <returns>
        /// The test arguments.
        /// </returns>
        public IEnumerable<object> GetArguments(object actualObject)
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

            return this.arguments.Concat(this.GetAutoData(actualObject));
        }

        private IEnumerable<object> GetAutoData(object actualObject)
        {
            var fixture = this.factory.Create(new TestInfo(actualObject, this));
            return this.actualMethod.GetParameters()
                .Skip(this.arguments.Length)
                .Select(p => fixture.Create(p.ParameterType));
        }
    }
}

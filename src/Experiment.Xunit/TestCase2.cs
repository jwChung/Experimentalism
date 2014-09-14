namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    /// <summary>
    /// Represents test case.
    /// </summary>
    public partial class TestCase2 : ITestCase2
    {
        private readonly MethodInfo testMethod;
        private readonly object[] arguments;
        private readonly Delegate delegator;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase2"/> class.
        /// </summary>
        /// <param name="delegator">
        /// A delegate representing actual test-case.
        /// </param>
        /// <param name="arguments">
        /// Test arguments.
        /// </param>
        public TestCase2(Delegate delegator, params object[] arguments)
        {
            if (delegator == null)
                throw new ArgumentNullException("delegator");

            if (arguments == null)
                throw new ArgumentNullException("arguments");

            this.delegator = delegator;
            this.arguments = arguments;
            this.testMethod = delegator.Method;
        }

        /// <summary>
        /// Gets the delegate representing actual test-case.
        /// </summary>
        public Delegate Delegator
        {
            get { return this.delegator; }
        }

        /// <summary>
        /// Gets the test object.
        /// </summary>
        public object Target
        {
            get { return this.delegator.Target; }
        }

        /// <summary>
        /// Gets the test method to be actually executed.
        /// </summary>
        public MethodInfo TestMethod
        {
            get { return this.testMethod; }
        }

        /// <summary>
        /// Gets the arguments specified with explicit values.
        /// </summary>
        public IEnumerable<object> Arguments
        {
            get { return this.arguments; }
        }

        /// <summary>
        /// Creates a test case with no arguments.
        /// </summary>
        /// <param name="delegator">
        /// A delegator representing the actual test method.
        /// </param>
        /// <returns>
        /// The new test case.
        /// </returns>
        public static ITestCase2 Create(Action delegator)
        {
            if (delegator == null)
                throw new ArgumentNullException("delegator");

            return new TestCase2(delegator, new object[0]);
        }

        /// <summary>
        /// Returns a test case with arguments.
        /// </summary>
        /// <typeparam name="T">
        /// A type of the first argument.
        /// </typeparam>
        /// <param name="arg">
        /// The first argument.
        /// </param>
        /// <returns>
        /// The new test case with arguments.
        /// </returns>
        public static ITestCaseWithArgs<T> WithArgs<T>(T arg)
        {
            return new TestCaseWithArgs<T>(arg);
        }

        /// <summary>
        /// Returns a test case with arguments.
        /// </summary>
        /// <typeparam name="T">
        /// A type of the first auto argument.
        /// </typeparam>
        /// <returns>
        /// The new test case with arguments.
        /// </returns>
        public static ITestCaseWithAuto<T> WithAuto<T>()
        {
            return new TestCaseWithAuto<T>();
        }

        /// <summary>
        /// Returns a test case with arguments.
        /// </summary>
        /// <typeparam name="T1">
        /// A type of the first argument.
        /// </typeparam>
        /// <typeparam name="T2">
        /// A type of the second argument.
        /// </typeparam>
        /// <param name="arg1">
        /// The first argument.
        /// </param>
        /// <param name="arg2">
        /// The second argument.
        /// </param>
        /// <returns>
        /// The new test case with arguments.
        /// </returns>
        public static ITestCaseWithArgs<T1, T2> WithArgs<T1, T2>(T1 arg1, T2 arg2)
        {
            return new TestCaseWithArgs<T1, T2>(arg1, arg2);
        }

        /// <summary>
        /// Returns a test case with arguments.
        /// </summary>
        /// <typeparam name="T1">
        /// A type of the first argument.
        /// </typeparam>
        /// <typeparam name="T2">
        /// A type of the second argument.
        /// </typeparam>
        /// <returns>
        /// The new test case with arguments.
        /// </returns>
        public static ITestCaseWithAuto<T1, T2> WithAuto<T1, T2>()
        {
            return new TestCaseWithAuto<T1, T2>();
        }
    }
}

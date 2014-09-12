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
        private readonly Delegate delegator;
        private readonly object[] arguments;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase2"/> class.
        /// </summary>
        /// <param name="delegator">
        /// A delegate representing the actual test method.
        /// </param>
        /// <param name="arguments">
        /// Explicit arguments.
        /// </param>
        public TestCase2(Delegate delegator, params object[] arguments)
        {
            this.arguments = arguments;
            this.delegator = delegator;
        }

        /// <summary>
        /// Gets the arguments specified with explicit values.
        /// </summary>
        public IEnumerable<object> Arguments
        {
            get { return this.arguments; }
        }

        /// <summary>
        /// Gets the test method to be actually executed.
        /// </summary>
        public MethodInfo TestMethod
        {
            get { return this.delegator.Method; }
        }

        /// <summary>
        /// Gets the test object declaring a actual test method.
        /// </summary>
        public object Target
        {
            get { return this.delegator.Target; }
        }

        /// <summary>
        /// Gets the delegate.
        /// </summary>
        public Delegate Delegate
        {
            get { return this.delegator; }
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

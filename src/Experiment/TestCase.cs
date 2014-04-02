using System;
using Xunit.Sdk;

namespace Jwc.Experiment
{
    /// <summary>
    /// Represents a weakly-typed test case that can be turned into an
    /// xUnit.net ITestCommand when returned from a test method adorned with
    /// the <see cref="DefaultFirstClassTheoremAttribute" />.
    /// </summary>
    public class TestCase : ITestCase
    {
        private readonly Delegate _delegate;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase"/> class.
        /// </summary>
        /// <param name="action">The test action.</param>
        public TestCase(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            if (action.GetInvocationList().Length != 1)
            {
                throw new ArgumentException(
                    "Composite actions are not supported, set only one action operation.",
                    "action");
            }

            _delegate = action;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase"/> class.
        /// </summary>
        /// <param name="func">The test function.</param>
        public TestCase(Func<object> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            if (func.GetInvocationList().Length != 1)
            {
                throw new ArgumentException(
                    "Composite functions are not supported, set only one function operation.",
                    "func");
            }

            _delegate = func;
        }

        /// <summary>
        /// Gets the test delegate.
        /// </summary>
        public Delegate Delegate
        {
            get
            {
                return _delegate;
            }
        }

        /// <summary>
        /// Converts the instance to an xUnit.net ITestCommand instance.
        /// </summary>
        /// <param name="method">
        /// The method adorned by a <see cref="DefaultFirstClassTheoremAttribute" />.
        /// </param>
        /// <param name="fixtureFactory">
        /// A test fixture factory to provide auto data.
        /// </param>
        /// <returns>
        /// An xUnit.net ITestCommand that represents the executable test case.
        /// </returns>
        public ITestCommand ConvertToTestCommand(IMethodInfo method, ITestFixtureFactory fixtureFactory)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }

            return new FirstClassCommand(method, Delegate.Method, new object[0]);
        }
    }

    /// <summary>
    /// Represents a weakly-typed test case that can be turned into an
    /// xUnit.net ITestCommand when returned from a test method adorned with
    /// the <see cref="DefaultFirstClassTheoremAttribute" />.
    /// </summary>
    public class TestCase<T> : ITestCase
    {
        private readonly Delegate _delegate;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase"/> class.
        /// </summary>
        /// <param name="action">The test action.</param>
        public TestCase(Action<T> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("action");
            }

            if (action.GetInvocationList().Length != 1)
            {
                throw new ArgumentException(
                    "Composite actions are not supported, set only one action operation.",
                    "action");
            }

            _delegate = action;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCase"/> class.
        /// </summary>
        /// <param name="func">The test function.</param>
        public TestCase(Func<T, object> func)
        {
            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            if (func.GetInvocationList().Length != 1)
            {
                throw new ArgumentException(
                    "Composite functions are not supported, set only one function operation.",
                    "func");
            }

            _delegate = func;
        }

        /// <summary>
        /// Gets the test delegate.
        /// </summary>
        public Delegate Delegate
        {
            get
            {
                return _delegate;
            }
        }

        /// <summary>
        /// Converts the instance to an xUnit.net ITestCommand instance.
        /// </summary>
        /// <param name="method">
        /// The method adorned by a <see cref="DefaultFirstClassTheoremAttribute" />.
        /// </param>
        /// <param name="fixtureFactory">
        /// A test fixture factory to provide auto data.
        /// </param>
        /// <returns>
        /// An xUnit.net ITestCommand that represents the executable test case.
        /// </returns>
        public ITestCommand ConvertToTestCommand(IMethodInfo method, ITestFixtureFactory fixtureFactory)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }

            if (fixtureFactory == null)
            {
                throw new ArgumentNullException("fixtureFactory");
            }

            var fixture = fixtureFactory.Create(Delegate.Method);
            var arguments = new[]
            {
                fixture.Create(typeof(T))
            };
            return new FirstClassCommand(method, Delegate.Method, arguments);
        }
    }
}
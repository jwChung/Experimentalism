using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit.Extensions;
using Xunit.Sdk;

namespace Jwc.Experiment
{
    /// <summary>
    /// Represents a weakly-typed test case that can be turned into an
    /// xUnit.net ITestCommand when returned from a test method adorned with
    /// the <see cref="NaiveFirstClassTheoremAttribute" />.
    /// </summary>
    public partial class TestCase : ITestCase
    {
        private readonly Delegate _delegate;
        private readonly object[] _arguments;

        private TestCase(Delegate @delegate, object[] arguments)
        {
            if (@delegate == null)
            {
                throw new ArgumentNullException("delegate");
            }

            if (!@delegate.Method.IsStatic)
            {
                throw new ArgumentException(
                    string.Format(
                        "The supplied delegate '{0}' is non static. This is because the delegate uses objects " +
                        "from outer scope, which results in the problem as Shared Fixture.",
                        @delegate),
                    "delegate");
            }

            _delegate = @delegate;
            _arguments = arguments;
        }

        /// <summary>
        /// Gets a value indicating the test delegate.
        /// </summary>
        /// <value>
        /// The test delegate originally supplied as a constructor argument.
        /// </value>
        public Delegate Delegate
        {
            get
            {
                return _delegate;
            }
        }

        /// <summary>
        /// Gets a value indicating the the arguments of the test delegate.
        /// </summary>
        /// <value>
        /// The test arguments originally supplied as a constructor argument.
        /// </value>
        public IEnumerable<object> Arguments
        {
            get
            {
                return _arguments;
            }
        }

        /// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <param name="delegate">
        /// The delegate to be invoked when the test is executed.
        /// </param>
        /// <returns>
        /// The created instance.
        /// </returns>
        public static ITestCase New(Action @delegate)
        {
            return new TestCase(@delegate, new object[0]);
        }

        /// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <typeparam name="TArg">The type of the argument.</typeparam>
        /// <param name="arg">The argument of the test delegate.</param>
        /// <param name="delegate">
        /// The delegate to be invoked when the test is executed.
        /// </param>
        /// <returns>
        /// The created instance.
        /// </returns>
        public static ITestCase New<TArg>(TArg arg, Action<TArg> @delegate)
        {
            return new TestCase(@delegate, new object[] { arg });
        }

        /// <summary>
        /// Creates a new instance of <see cref="ITestCase" />.
        /// </summary>
        /// <typeparam name="TArg">The type of the argument.</typeparam>
        /// <param name="delegate">
        /// The delegate to be invoked with an auto argument when the test is executed.
        /// </param>
        /// <returns>
        /// The created instance.
        /// </returns>
        public static ITestCase New<TArg>(Action<TArg> @delegate)
        {
            return new TestCase(@delegate, new object[0]);
        }

        /// <summary>
        /// Converts the instance to an xUnit.net ITestCommand instance.
        /// </summary>
        /// <param name="method">
        /// The method adorned by a <see cref="NaiveFirstClassTheoremAttribute" />.
        /// </param>
        /// <param name="testFixture">A test fixture to provide auto data.</param>
        /// <returns>
        /// An xUnit.net ITestCommand that represents the executable test case.
        /// </returns>
        public ITestCommand ConvertToTestCommand(IMethodInfo method, ITestFixture testFixture)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }

            if (testFixture == null)
            {
                throw new ArgumentNullException("testFixture");
            }

            return new FirstClassCommand(
                Reflector.Wrap(Delegate.Method),
                GetFinalArguments(testFixture, Delegate.Method),
                method.TypeName + "." + method.Name);
        }

        private object[] GetFinalArguments(ITestFixture testFixture, MethodInfo methodInfo)
        {
            var specifiedArgument = Arguments.ToArray();
            var autoArguments = methodInfo.GetParameters()
                .Skip(specifiedArgument.Length)
                .Select(pi => testFixture.Create(pi.ParameterType));

            return specifiedArgument.Concat(autoArguments).ToArray();
        }

        private class FirstClassCommand : TheoryCommand
        {
            public FirstClassCommand(IMethodInfo testMethod, object[] parameters, string displayName)
                : base(testMethod, parameters)
            {
                DisplayName = displayName;
            }
        }
    }
}
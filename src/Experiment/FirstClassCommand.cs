using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Xunit.Sdk;

namespace Jwc.Experiment
{
    /// <summary>
    /// Represents a test command for first class tests.
    /// </summary>
    public class FirstClassCommand : FactCommand
    {
        private readonly IMethodInfo _declaredMethod;
        private readonly MethodInfo _testCase;
        private readonly object[] _arguments;

        /// <summary>
        /// Initializes a new instance of the <see cref="FirstClassCommand"/> class.
        /// </summary>
        /// <param name="declaredMethod">
        /// The test method which this instance is associated. This will
        /// likely be the method adorned with an
        /// </param>
        /// <param name="testCase">
        /// The test case to be invoked when the test is executed.
        /// </param>
        /// <param name="arguments">
        /// The test arguments to be supplied to the test delegate.
        /// </param>
        public FirstClassCommand(IMethodInfo declaredMethod, MethodInfo testCase, object[] arguments)
            : base(EnsureIsNotNull(declaredMethod))
        {
            if (testCase == null)
            {
                throw new ArgumentNullException("testCase");
            }

            if (arguments == null)
            {
                throw new ArgumentNullException("arguments");
            }

            _declaredMethod = declaredMethod;
            _testCase = testCase;
            _arguments = arguments;

            SetWellFormattedDisplayName();
        }

        /// <summary>
        /// Gets the declared method.
        /// </summary>
        public IMethodInfo DeclaredMethod
        {
            get
            {
                return _declaredMethod;
            }
        }

        /// <summary>
        /// Gets the delegate.
        /// </summary>
        public MethodInfo TestCase
        {
            get
            {
                return _testCase;
            }
        }

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        public IEnumerable<object> Arguments
        {
            get
            {
                return _arguments;
            }
        }

        /// <summary>
        /// Execute the test delegate with the arguments.
        /// </summary>
        /// <param name="testClass">The test class object.</param>
        /// <returns>The result of the execution.</returns>
        public override MethodResult Execute(object testClass)
        {
            TestCase.Invoke(null, Arguments.ToArray());
            return new PassedResult(DeclaredMethod, DisplayName);
        }

        private static IMethodInfo EnsureIsNotNull(IMethodInfo declaredMethod)
        {
            if (declaredMethod == null)
            {
                throw new ArgumentNullException("declaredMethod");
            }

            return declaredMethod;
        }

        private void SetWellFormattedDisplayName()
        {
            DisplayName += "(" + string.Join(", ", GetArgumentValues()) + ")";
        }

        private IEnumerable<string> GetArgumentValues()
        {
            var arguments = Arguments.ToArray();
            return TestCase.GetParameters().Select(pi =>
                GetArgumentValue(pi.ParameterType.Name, arguments[pi.Position]));
        }

        private static string GetArgumentValue(string typeName, object argument)
        {
            return typeName + ": " + (argument != null ? "\"" + argument + "\"" : "NULL");
        }
    }
}
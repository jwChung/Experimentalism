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
    public class FirstClassCommand : TestCommand
    {
        private readonly IMethodInfo _declaredMethod;
        private readonly MethodInfo _testMethod;
        private readonly object[] _arguments;

        /// <summary>
        /// Initializes a new instance of the <see cref="FirstClassCommand"/> class.
        /// </summary>
        /// <param name="declaredMethod">
        /// The test method which this instance is associated. This will
        /// likely be the method adorned with an
        /// </param>
        /// <param name="testMethod">
        /// The test case to be invoked when the test is executed.
        /// </param>
        /// <param name="arguments">
        /// The test arguments to be supplied to the test delegate.
        /// </param>
        public FirstClassCommand(IMethodInfo declaredMethod, MethodInfo testMethod, object[] arguments)
            : base(EnsureIsNotNull(declaredMethod), null, 0)
        {
            if (testMethod == null)
            {
                throw new ArgumentNullException("testMethod");
            }

            if (arguments == null)
            {
                throw new ArgumentNullException("arguments");
            }

            _declaredMethod = declaredMethod;
            _testMethod = testMethod;
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
        /// Gets the actual test method.
        /// </summary>
        [CLSCompliant(false)]
        public MethodInfo TestMethod
        {
            get
            {
                return _testMethod;
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
        /// Gets a value indicating whether a test-case instance is created.
        /// </summary>
        /// <value>
        /// <c>true</c> if a test-case instance is created; otherwise, <c>false</c>.
        /// </value>
        public override bool ShouldCreateInstance
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Execute the test delegate with the arguments.
        /// </summary>
        /// <param name="testClass">The test class object.</param>
        /// <returns>The result of the execution.</returns>
        public override MethodResult Execute(object testClass)
        {
            TestMethod.Invoke(null, Arguments.ToArray());
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
            return TestMethod.GetParameters().Select(pi =>
                GetArgumentValue(pi.ParameterType.Name, arguments[pi.Position]));
        }

        private static string GetArgumentValue(string typeName, object argument)
        {
            return typeName + ": " + (argument != null ? "\"" + argument + "\"" : "NULL");
        }
    }
}
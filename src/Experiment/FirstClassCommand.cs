using System;
using System.Collections.Generic;
using System.Linq;
using Xunit.Sdk;

namespace Jwc.Experiment
{
    /// <summary>
    /// Represents a test command for first class tests.
    /// </summary>
    public class FirstClassCommand : FactCommand
    {
        private readonly IMethodInfo _method;
        private readonly Delegate _delegate;
        private readonly object[] _arguments;

        /// <summary>
        /// Initializes a new instance of the <see cref="FirstClassCommand"/> class.
        /// </summary>
        /// <param name="method">
        /// The test method with which this instance is associated. This will
        /// likely be the method adorned with an
        /// <see cref="DefaultFirstClassTheoremAttribute" />.
        /// </param>
        /// <param name="delegate">
        /// The test delegate to be invoked when the test is executed.
        /// </param>
        /// <param name="arguments">
        /// The test arguments to be supplied to the test delegate.
        /// </param>
        public FirstClassCommand(IMethodInfo method, Delegate @delegate, object[] arguments)
            : base(EnsureIsNotNull(method))
        {
            if (@delegate == null)
            {
                throw new ArgumentNullException("delegate");
            }

            if (arguments == null)
            {
                throw new ArgumentNullException("arguments");
            }

            _method = method;
            _delegate = @delegate;
            _arguments = arguments;

            SetWellFormattedDisplayName();
        }

        /// <summary>
        /// Gets the method.
        /// </summary>
        public IMethodInfo Method
        {
            get
            {
                return _method;
            }
        }

        /// <summary>
        /// Gets the delegate.
        /// </summary>
        public Delegate Delegate
        {
            get
            {
                return _delegate;
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
            Delegate.Method.Invoke(null, Arguments.ToArray());
            return new PassedResult(Method, DisplayName);
        }

        private static IMethodInfo EnsureIsNotNull(IMethodInfo method)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }

            return method;
        }

        private void SetWellFormattedDisplayName()
        {
            DisplayName += "(" + string.Join(", ", GetArgumentValues()) + ")";
        }

        private IEnumerable<string> GetArgumentValues()
        {
            var arguments = Arguments.ToArray();
            return Delegate.Method.GetParameters().Select(pi =>
                GetArgumentValue(pi.ParameterType.Name, arguments[pi.Position]));
        }

        private static string GetArgumentValue(string typeName, object argument)
        {
            return typeName + ": " + (argument != null ? "\"" + argument + "\"" : "NULL");
        }
    }
}
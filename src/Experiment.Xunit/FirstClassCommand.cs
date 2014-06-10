﻿using System;
using System.Collections.Generic;
using Xunit.Sdk;

namespace Jwc.Experiment.Xunit
{
    /// <summary>
    ///     Represents a test command for first class tests.
    /// </summary>
    public class FirstClassCommand : TestCommand
    {
        private readonly IMethodInfo _method;
        private readonly string _displayParameterName;
        private readonly Delegate _delegate;
        private readonly object[] _arguments;

        /// <summary>
        ///     Initializes a new instance of the <see cref="FirstClassCommand" /> class.
        /// </summary>
        /// <param name="method">
        ///     The test method which this instance is associated. This will likely be the method
        ///     adorned with an <see cref="FirstClassTestAttribute" />.
        /// </param>
        /// <param name="displayParameterName">
        ///     A string to show parameters of a test method in test result.
        /// </param>
        /// <param name="delegate">
        ///     The test case to be invoked when the test is executed.
        /// </param>
        /// <param name="arguments">
        ///     The test arguments to be supplied to the test delegate.
        /// </param>
        public FirstClassCommand(
            IMethodInfo method, string displayParameterName, Delegate @delegate, object[] arguments) : base(
                EnsureIsNotNull(method),
                MethodUtility.GetDisplayName(method),
                MethodUtility.GetTimeoutParameter(method))
        {
            if (displayParameterName == null)
                throw new ArgumentNullException("displayParameterName");

            if (@delegate == null)
                throw new ArgumentNullException("delegate");

            if (arguments == null)
                throw new ArgumentNullException("arguments");

            _method = method;
            _displayParameterName = displayParameterName;
            _delegate = @delegate;
            _arguments = arguments;

            SetDisplayName();
        }

        /// <summary>
        ///     Gets the test method.
        /// </summary>
        public IMethodInfo Method
        {
            get
            {
                return _method;
            }
        }

        /// <summary>
        ///     Gets a value indicating the string to show parameters of a test method in test
        ///     result.
        /// </summary>
        public string DisplayParameterName
        {
            get
            {
                return _displayParameterName;
            }
        }

        /// <summary>
        ///     Gets the test delegate.
        /// </summary>
        public Delegate Delegate
        {
            get
            {
                return _delegate;
            }
        }

        /// <summary>
        ///     Gets the arguments.
        /// </summary>
        public IEnumerable<object> Arguments
        {
            get
            {
                return _arguments;
            }
        }

        /// <summary>
        ///     Gets a value indicating whether a test-case instance is created.
        /// </summary>
        /// <value>
        ///     <c>true</c> if a test-case instance is created; otherwise, <c>false</c>.
        /// </value>
        public override bool ShouldCreateInstance
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        ///     Execute the test delegate with the arguments.
        /// </summary>
        /// <param name="testClass">
        ///     The test class object.
        /// </param>
        /// <returns>
        ///     The result of the execution.
        /// </returns>
        public override MethodResult Execute(object testClass)
        {
            Delegate.GetType().GetMethod("Invoke").Invoke(Delegate, _arguments);
            return new PassedResult(Method, DisplayName);
        }

        private static IMethodInfo EnsureIsNotNull(IMethodInfo method)
        {
            if (method == null)
                throw new ArgumentNullException("method");

            return method;
        }

        private void SetDisplayName()
        {
            DisplayName += "(" + DisplayParameterName + ")";
        }
    }
}
﻿using System;
using Xunit.Sdk;

namespace Jwc.Experiment.Xunit
{
    /// <summary>
    ///     Exception 테스트커멘드를 나타냄.
    /// </summary>
    public class ExceptionCommand : TestCommand
    {
        private readonly Exception _exception;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ExceptionCommand" /> class.
        /// </summary>
        /// <param name="method">
        ///     The method under test.
        /// </param>
        /// <param name="exception">
        ///     The exception to be expressed.
        /// </param>
        public ExceptionCommand(IMethodInfo method, Exception exception)
            : base(EnsureIsNotNull(method), null, 0)
        {
            if (exception == null)
            {
                throw new ArgumentNullException("exception");
            }

            _exception = exception;
        }

        /// <summary>
        ///     Gets a value indicating the exception.
        /// </summary>
        public Exception Exception
        {
            get
            {
                return _exception;
            }
        }

        /// <summary>
        ///     이 테스트 실행 결과는 항상 <see cref="FailedResult" />를 리턴함.
        /// </summary>
        /// <param name="testClass">
        ///     테스트 메소드의 owner.
        /// </param>
        /// <returns>
        ///     특정 exception을 표현하는 <see cref="FailedResult" /> 결과.
        /// </returns>
        public override MethodResult Execute(object testClass)
        {
            return new FailedResult(testMethod, Exception, DisplayName);
        }

        private static IMethodInfo EnsureIsNotNull(IMethodInfo method)
        {
            if (method == null)
            {
                throw new ArgumentNullException("method");
            }

            return method;
        }
    }
}
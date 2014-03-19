using System;
using Xunit.Sdk;

namespace Jwc.Experiment
{
    /// <summary>
    /// Exception 테스트 케이스를 나타냄.
    /// </summary>
    public class ExceptionCommand : TestCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionCommand"/> class.
        /// </summary>
        /// <param name="method">The method under test.</param>
        public ExceptionCommand(IMethodInfo method)
            : base(EnsureIsNotNull(method), null, 0)
        {
        }

        /// <summary>
        /// 테스트 메소드 실행.
        /// </summary>
        /// <param name="testClass">테스트 메소드의 owner.</param>
        /// <returns>테스트 결과.</returns>
        public override MethodResult Execute(object testClass)
        {
            throw new System.NotImplementedException();
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
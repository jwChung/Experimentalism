using System;
using Xunit.Sdk;

namespace Jwc.Experiment
{
    /// <summary>
    /// 
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
        /// </summary>
        /// <param name="testClass"></param>
        /// <returns></returns>
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
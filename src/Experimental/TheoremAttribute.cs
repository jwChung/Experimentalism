using System;
using System.Collections.Generic;
using Xunit;
using Xunit.Extensions;
using Xunit.Sdk;

namespace Jwc.Experimental
{
    /// <summary>
    /// 테스트 메소드를 지칭하는 어트리뷰트로써, test runner에 의해 실행된다.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TheoremAttribute : FactAttribute
    {
        /// <summary>
        /// Enumerates the test commands represented by this test method.
        /// Derived classes should override this method to return instances of 
        /// <see cref="ITestCommand" />, one per execution of a test method.
        /// </summary>
        /// <param name="method">The test method</param>
        /// <returns>
        /// The test commands which will execute the test runs for the given method
        /// </returns>
        protected override IEnumerable<ITestCommand> EnumerateTestCommands(IMethodInfo method)
        {
            return !method.MethodInfo.IsDefined(typeof(DataAttribute), false) 
                ? base.EnumerateTestCommands(method)
                : new TheoryAttribute().CreateTestCommands(method);
        }
    }
}
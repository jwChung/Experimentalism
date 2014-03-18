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
        protected override IEnumerable<ITestCommand> EnumerateTestCommands(IMethodInfo method)
        {
            return !method.MethodInfo.IsDefined(typeof(DataAttribute), false) 
                ? base.EnumerateTestCommands(method)
                : new TheoryAttribute().CreateTestCommands(method);
        }
    }
}
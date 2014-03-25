using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Xunit.Sdk;

namespace Jwc.Experiment
{
    /// <summary>
    /// A test attribute used to adorn methods that creates first-class 
    /// executable test cases.
    /// </summary>
    public class NaiveFirstClassTheoremAttribute : FactAttribute
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
            var methodInfo = method.MethodInfo;
            var declaringObject = IsStatic(methodInfo.DeclaringType) 
                ? null
                : Activator.CreateInstance(methodInfo.DeclaringType);
            
            var testCases = (IEnumerable<ITestCase>)methodInfo.Invoke(declaringObject, null);
            return testCases.Select(tc => tc.ConvertToTestCommand(method));
        }

        private static bool IsStatic(Type type)
        {
            return type.IsAbstract && type.IsSealed;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
            return CreateTestCases(method).Select(tc => tc.ConvertToTestCommand(method));
        }

        private static IEnumerable<ITestCase> CreateTestCases(IMethodInfo method)
        {
            var methodInfo = method.MethodInfo;
            return (IEnumerable<ITestCase>)methodInfo.Invoke(CreateDeclaringObject(methodInfo), null);
        }

        private static object CreateDeclaringObject(MethodInfo methodInfo)
        {
            return IsStatic(methodInfo.DeclaringType)
                ? null
                : Activator.CreateInstance(methodInfo.DeclaringType);
        }

        private static bool IsStatic(Type type)
        {
            return type.IsAbstract && type.IsSealed;
        }
    }
}
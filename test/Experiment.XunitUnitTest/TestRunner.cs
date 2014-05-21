using System;
using System.Reflection;

namespace Jwc.Experiment.Xunit
{
    public class TestRunner : MarshalByRefObject
    {
        public void Run(MethodInfo testMethod)
        {
            var target = Activator.CreateInstance(testMethod.ReflectedType);
            testMethod.Invoke(target, new object[0]);
        }
    }
}
using System;
using System.Reflection;

namespace Jwc.Experiment.Xunit
{
    public class StaticFactInvoker : MarshalByRefObject
    {
        public void Invoke(MethodInfo testMethod)
        {
            var target = Activator.CreateInstance(testMethod.ReflectedType);
            testMethod.Invoke(target, new object[0]);
        }
    }
}
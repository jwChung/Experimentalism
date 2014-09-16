namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;
    using global::Xunit.Sdk;

    public abstract class TestCommandContext2 : ITestCommandContext
    {
        public abstract IMethodInfo TestMethod { get; }

        public abstract ITestMethodContext GetMethodContext(object testObject);

        public abstract ITestMethodContext GetStaticMethodContext();

        public IEnumerable<object> GetArguments(ITestMethodContext context)
        {
            throw new NotImplementedException();
        }
    }
}
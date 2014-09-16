namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;
    using global::Xunit.Sdk;

    public class StaticTestCaseCommandContext : TestCommandContext2
    {
        public StaticTestCaseCommandContext(
            IMethodInfo testMethod,
            IMethodInfo actualMethod,
            ITestFixtureFactory factory,
            IEnumerable<object> arguments)
            : base(factory, arguments)
        {
        }

        public override IMethodInfo TestMethod
        {
            get { throw new NotImplementedException(); }
        }

        public override ITestMethodContext GetMethodContext(object testObject)
        {
            throw new NotImplementedException();
        }

        public override ITestMethodContext GetStaticMethodContext()
        {
            throw new NotImplementedException();
        }
    }
}
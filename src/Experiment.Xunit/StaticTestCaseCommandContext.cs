namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;
    using global::Xunit.Sdk;

    public class StaticTestCaseCommandContext : TestCommandContext2
    {
        private readonly IMethodInfo testMethod;
        private readonly IMethodInfo actualMethod;

        public StaticTestCaseCommandContext(
            IMethodInfo testMethod,
            IMethodInfo actualMethod,
            ITestFixtureFactory factory,
            IEnumerable<object> arguments)
            : base(factory, arguments)
        {
            if (testMethod == null)
                throw new ArgumentNullException("testMethod");

            if (actualMethod == null)
                throw new ArgumentNullException("actualMethod");

            this.testMethod = testMethod;
            this.actualMethod = actualMethod;
        }

        public override IMethodInfo TestMethod
        {
            get { return this.testMethod; }
        }

        public IMethodInfo ActualMethod
        {
            get { return this.actualMethod; }
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
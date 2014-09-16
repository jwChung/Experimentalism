namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;
    using global::Xunit.Sdk;

    public class TestCaseCommandContext : TestCommandContext2
    {
        private readonly IMethodInfo testMethod;
        private readonly IMethodInfo actualMethod;
        private readonly object actualObject;

        public TestCaseCommandContext(
            IMethodInfo testMethod,
            IMethodInfo actualMethod,
            object actualObject,
            ITestFixtureFactory factory,
            IEnumerable<object> arguments)
            : base(factory, arguments)
        {
            if (testMethod == null)
                throw new ArgumentNullException("testMethod");

            if (actualMethod == null)
                throw new ArgumentNullException("actualMethod");

            if (actualObject == null)
                throw new ArgumentNullException("actualObject");

            this.testMethod = testMethod;
            this.actualMethod = actualMethod;
            this.actualObject = actualObject;
        }

        public override IMethodInfo TestMethod
        {
            get { return this.testMethod; }
        }

        public IMethodInfo ActualMethod
        {
            get { return this.actualMethod; }
        }

        public object ActualObject
        {
            get { return this.actualObject; }
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
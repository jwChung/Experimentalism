﻿namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Collections.Generic;
    using global::Xunit.Sdk;

    public class ParameterizedCommandContext : TestCommandContext2
    {
        private readonly IMethodInfo testMethod;

        public ParameterizedCommandContext(
            IMethodInfo testMethod, ITestFixtureFactory factory, IEnumerable<object> arguments)
            : base(factory, arguments)
        {
            if (testMethod == null)
                throw new ArgumentNullException("testMethod");

            this.testMethod = testMethod;
        }

        public override IMethodInfo TestMethod
        {
            get { return this.testMethod; }
        }

        public override ITestMethodContext GetMethodContext(object testObject)
        {
            if (testObject == null)
                throw new ArgumentNullException("testObject");

            return new TestMethodContext(
                this.testMethod.MethodInfo, this.testMethod.MethodInfo, testObject, testObject);
        }

        public override ITestMethodContext GetStaticMethodContext()
        {
            return new TestMethodContext(
               this.testMethod.MethodInfo, this.testMethod.MethodInfo, null, null);
        }
    }
}
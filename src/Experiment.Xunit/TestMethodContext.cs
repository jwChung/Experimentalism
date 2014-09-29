namespace Jwc.Experiment.Xunit
{
    using System.Reflection;

    internal class TestMethodContext : ITestMethodContext
    {
        private readonly MethodInfo testMethod;
        private readonly MethodInfo actualMethod;
        private readonly object testObject;
        private readonly object actualObject;

        public TestMethodContext(
            MethodInfo testMethod,
            MethodInfo actualMethod,
            object testObject,
            object actualObject)
        {
            this.testMethod = testMethod;
            this.actualMethod = actualMethod;
            this.testObject = testObject;
            this.actualObject = actualObject;
        }

        public MethodInfo TestMethod
        {
            get { return this.testMethod; }
        }

        public MethodInfo ActualMethod
        {
            get { return this.actualMethod; }
        }

        public object TestObject
        {
            get { return this.testObject; }
        }

        public object ActualObject
        {
            get { return this.actualObject; }
        }
    }
}
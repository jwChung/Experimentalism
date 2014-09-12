namespace Jwc.Experiment.Xunit
{
    using System;
    using System.Reflection;
    using global::Xunit.Sdk;

    /// <summary>
    /// Represents information of a test.
    /// </summary>
    public class TestInfo : ITestMethodInfo, ITestCommandInfo
    {
        /// <summary>
        /// Gets the test method adorned with a test attribute.
        /// </summary>
        public MethodInfo TestMethod
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the test method to be actually executed.
        /// </summary>
        public MethodInfo ActualMethod
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the test object declaring a adorned test method.
        /// </summary>
        public object TestObject
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the test object declaring a actual test method.
        /// </summary>
        public object ActualObject
        {
            get { throw new NotImplementedException(); }
        }

        IMethodInfo ITestCommandInfo.TestMethod
        {
            get { throw new NotImplementedException(); }
        }

        object[] ITestCommandInfo.GetArguments(object testObject)
        {
            throw new NotImplementedException();
        }
    }
}

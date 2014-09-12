namespace Jwc.Experiment.Xunit
{
    using System;
    using global::Xunit.Sdk;

    /// <summary>
    /// Represents a parameterized test command.
    /// </summary>
    public class ParameterizedCommand : TestCommand
    {
        private readonly ITestCommandInfo testCommandInfo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterizedCommand" /> class.
        /// </summary>
        /// <param name="testCommandInfo">
        /// Information about this test command.
        /// </param>
        public ParameterizedCommand(ITestCommandInfo testCommandInfo)
            : base(GuardNull(testCommandInfo).TestMethod, null, 0)
        {
            this.testCommandInfo = testCommandInfo;
        }

        /// <summary>
        /// Gets the information of the test command.
        /// </summary>
        public ITestCommandInfo TestCommandInfo
        {
            get { return this.testCommandInfo; }
        } 
        
        /// <summary>
        /// Executes this test command with a test object.
        /// </summary>
        /// <param name="testClass">
        /// The type of the test object.
        /// </param>
        /// <returns>
        /// The result of this test command.
        /// </returns>
        public override MethodResult Execute(object testClass)
        {
            throw new NotImplementedException();
        }

        private static ITestCommandInfo GuardNull(ITestCommandInfo testCommandInfo)
        {
            if (testCommandInfo == null)
                throw new ArgumentNullException("testCommandInfo");

            return testCommandInfo;
        }
    }
}

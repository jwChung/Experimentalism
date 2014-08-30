namespace Jwc.Experiment
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Represents a test fixture created by a function delegate.
    /// </summary>
    public class FuncTestFixtureFactory : ITestFixtureFactory
    {
        private readonly Func<MethodInfo, ITestFixture> func;

        /// <summary>
        /// Initializes a new instance of the <see cref="FuncTestFixtureFactory" /> class.
        /// </summary>
        /// <param name="func">
        /// The function to create the test fixture.
        /// </param>
        public FuncTestFixtureFactory(Func<MethodInfo, ITestFixture> func)
        {
            if (func == null)
                throw new ArgumentNullException("func");

            this.func = func;
        }

        /// <summary>
        /// Gets a value indicating the function supplied by constructor.
        /// </summary>
        public Func<MethodInfo, ITestFixture> Func
        {
            get { return this.func; }
        }

        /// <summary>
        /// Creates a test fixture.
        /// </summary>
        /// <param name="testMethod">
        /// The test method in which the test fixture will be used.
        /// </param>
        /// <returns>
        /// The test fixture.
        /// </returns>
        public ITestFixture Create(MethodInfo testMethod)
        {
            if (testMethod == null)
                throw new ArgumentNullException("testMethod");

            return this.Func(testMethod);
        }
    }
}
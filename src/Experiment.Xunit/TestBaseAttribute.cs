namespace Jwc.Experiment.Xunit
{
    using System;
    using global::Xunit;

    /// <summary>
    /// Represents base attribute to indicate that a given method is a test method.
    /// </summary>
    public abstract class TestBaseAttribute : FactAttribute, ITestFixtureFactory
    {
        ITestFixture ITestFixtureFactory.Create(ITestMethodInfo context)
        {
            return this.Create(context);
        }

        /// <summary>
        /// Creates a test fixture.
        /// </summary>
        /// <param name="testMethod">
        /// The test information about a test method.
        /// </param>
        /// <returns>
        /// The test fixture.
        /// </returns>
        protected abstract ITestFixture Create(ITestMethodInfo testMethod);
    }
}

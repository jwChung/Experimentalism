using System.Reflection;

namespace Jwc.Experiment
{
    /// <summary>
    /// A test attribute declared on a test method to indicate a test case.
    /// This attribute can be used for non-parameterized test as well as
    /// parameterized test, and supports to generate auto data using
    /// the AutoFixture library.
    /// </summary>
    public abstract class AutoFixtureTheoremAttribute : BaseTheoremAttribute
    {
        /// <summary>
        /// Creates an instance of <see cref="ITestFixture"/>.
        /// </summary>
        /// <param name="testMethod">
        /// The test method
        /// </param>
        /// <returns>
        /// The created fixture.
        /// </returns>
        public override ITestFixture CreateTestFixture(MethodInfo testMethod)
        {
            throw new System.NotImplementedException();
        }
    }
}
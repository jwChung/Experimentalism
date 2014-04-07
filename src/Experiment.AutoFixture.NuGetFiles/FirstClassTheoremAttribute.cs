using System.Reflection;

namespace Jwc.Experiment
{
    /// <summary>
    /// A test attribute used to adorn methods that creates first-class 
    /// executable test cases. This attribute supports to generate auto data
    /// using the AutoFixture library.
    /// </summary>
    public class FirstClassTheoremAttribute : BaseFirstClassTheoremAttribute
    {
        /// <summary>
        /// Creates an instance of <see cref="ITestFixture" />.
        /// </summary>
        /// <param name="testMethod">The test method</param>
        /// <returns>
        /// The created fixture.
        /// </returns>
        public override ITestFixture CreateTestFixture(MethodInfo testMethod)
        {
            throw new System.NotImplementedException();
        }
    }
}
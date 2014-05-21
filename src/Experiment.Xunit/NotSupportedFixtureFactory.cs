using System;
using System.Reflection;

namespace Jwc.Experiment.Xunit
{
    /// <summary>
    /// Represents test fixture factory which throws
    /// <see cref="NotSupportedException" /> when the <code>Create</code>
    /// method is called.
    /// </summary>
    public class NotSupportedFixtureFactory : ITestFixtureFactory
    {
        /// <summary>
        /// Throws <see cref="NotSupportedException" />.
        /// </summary>
        /// <param name="testMethod">
        /// The test method in which the test fixture will be used.
        /// </param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "CreateTestFixture")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "SetCurrent")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "TestFixtureFactory")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "ITestFixtureFactory")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "ITestFixture")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "AssemblyFixtureConfigAttribute")]
        public ITestFixture Create(MethodInfo testMethod)
        {
            throw new NotSupportedException(
                "To create auto data, set valid 'ITestFixtureFactory' with the 'TestFixtureFactory.SetCurrent' " +
                "method through 'AssemblyFixtureConfigAttribute', or override the 'CreateTestFixture' method " +
                "of the test attribute to create an instance of 'ITestFixture'.");
        }
    }
}
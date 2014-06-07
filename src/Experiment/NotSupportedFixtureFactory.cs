using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Jwc.Experiment
{
    /// <summary>
    ///     Represents test fixture factory which throws
    ///     <see cref="NotSupportedException" /> when the <code>Create</code>
    ///     method is called.
    /// </summary>
    public class NotSupportedFixtureFactory : ITestFixtureFactory
    {
        /// <summary>
        ///     Throws <see cref="NotSupportedException" />.
        /// </summary>
        /// <param name="testMethod">
        ///     The test method in which the test fixture will be used.
        /// </param>
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "CreateTestFixture")]
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "SetCurrent")]
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "TestFixtureFactory")]
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "ITestFixtureFactory")]
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "ITestFixture")]
        [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "AssemblyCustomizationAttribute")]
        public ITestFixture Create(MethodInfo testMethod)
        {
            // TODO: clarify this exception message.
            throw new NotSupportedException(
                "To create auto data, set valid 'ITestFixtureFactory' with the 'TestFixtureFactory.SetCurrent' " +
                "method through 'AssemblyCustomizationAttribute', or override the 'CreateTestFixture' method " +
                "of the test attribute to create an instance of 'ITestFixture'.");
        }
    }
}
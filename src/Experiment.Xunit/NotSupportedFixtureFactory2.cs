using System;
using System.Globalization;
using System.Reflection;

namespace Jwc.Experiment.Xunit
{
    /// <summary>
    /// Represents test fixture factory which throws
    /// <see cref="NotSupportedException" /> when the <code>Create</code>
    /// method is called.
    /// </summary>
    public class NotSupportedFixtureFactory2 : ITestFixtureFactory
    {
        /// <summary>
        /// Throws <see cref="NotSupportedException" />.
        /// </summary>
        /// <param name="testMethod">
        /// The test method in which the test fixture will be used.
        /// </param>
        public ITestFixture Create(MethodInfo testMethod)
        {
            if (testMethod == null)
                throw new ArgumentNullException("testMethod");

            throw new NotSupportedException(
                String.Format(
                    CultureInfo.CurrentCulture,
                    "To create auto data, explicitly declare TestFixtureFactoryTypeAttribute on the test " +
                    "assembly '{0}' or override the CreateTestFixture method of the test attribute " +
                    "to create an instance of ITestFixture.",
                    testMethod.ReflectedType.Assembly));
        }
    }
}
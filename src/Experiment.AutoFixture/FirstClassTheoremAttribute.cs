using System;
using System.Reflection;
using Jwc.Experiment;
using Ploeh.AutoFixture;

namespace NuGet.Jwc.Experiment
{
    /// <summary>
    /// A test attribute used to adorn methods that creates first-class 
    /// executable test cases. This attribute supports to generate auto data
    /// using the AutoFixture library.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1813:AvoidUnsealedAttributes", Justification = "This attribute is part of an inheritance hierarchy and can be inherited in order to extend its behavior.")]
    [AttributeUsage(AttributeTargets.Method)]
    public class FirstClassTheoremAttribute : BaseFirstClassTheoremAttribute
    {
        /// <summary>
        /// Creates an instance of <see cref="ITestFixture" />.
        /// </summary>
        /// <param name="testMethod">The test method</param>
        /// <returns>
        /// The created fixture.
        /// </returns>
        protected override ITestFixture CreateTestFixture(MethodInfo testMethod)
        {
            return new AutoFixtureAdapter(CreateFixture());
        }

        /// <summary>
        /// Creates the fixture.
        /// </summary>
        /// <returns>The new fixture instance.</returns>
        protected virtual IFixture CreateFixture()
        {
            return new Fixture();
        }
    }
}
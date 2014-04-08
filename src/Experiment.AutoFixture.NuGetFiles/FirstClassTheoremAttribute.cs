using System;
using System.Reflection;
using Jwc.Experiment;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace Jwc.NuGetFiles
{
    /// <summary>
    /// A test attribute used to adorn methods that creates first-class 
    /// executable test cases. This attribute supports to generate auto data
    /// using the AutoFixture library.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class FirstClassTheoremAttribute : BaseFirstClassTheoremAttribute
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
            if (testMethod == null)
            {
                throw new ArgumentNullException("testMethod");
            }

            return new AutoFixtureAdapter(new SpecimenContext(new Fixture()));
        }
    }
}
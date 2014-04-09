using System;
using System.Reflection;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;

namespace Jwc.Experiment
{
    /// <summary>
    /// A test attribute used to adorn methods that creates first-class 
    /// executable test cases. This attribute supports to generate auto data
    /// using the AutoFixture library.
    /// </summary>
    public abstract class AutoFixtureFirstClassTheoremAttribute : BaseFirstClassTheoremAttribute
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
            return new AutoFixtureAdapter(new SpecimenContext(CreateFixture()));
        }

        /// <summary>
        /// Creates the fixture.
        /// </summary>
        /// <returns>The new fixture instance.</returns>
        protected abstract IFixture CreateFixture();
    }
}
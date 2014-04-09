using System;
using Jwc.Experiment;
using Ploeh.AutoFixture;

namespace Jwc.NuGetFiles
{
    /// <summary>
    /// A test attribute declared on a test method to indicate a test case.
    /// This attribute can be used for non-parameterized test as well as
    /// parameterized test, and supports to generate auto data using
    /// the AutoFixture library.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class TheoremAttribute : AutoFixtureTheoremAttribute
    {
        /// <summary>
        /// Creates the fixture.
        /// </summary>
        /// <returns>The new fixture instance.</returns>
        protected override IFixture CreateFixture()
        {
            return new Fixture();
        }
    }
}
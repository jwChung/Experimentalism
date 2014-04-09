using System;
using Jwc.Experiment;
using Ploeh.AutoFixture;

namespace Jwc.NuGetFiles
{
    /// <summary>
    /// A test attribute used to adorn methods that creates first-class 
    /// executable test cases. This attribute supports to generate auto data
    /// using the AutoFixture library.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class FirstClassTheoremAttribute : AutoFixtureFirstClassTheoremAttribute
    {
        /// <summary>
        /// Creates the fixture.
        /// </summary>
        /// <returns>
        /// The new fixture instance.
        /// </returns>
        protected override IFixture CreateFixture()
        {
            return new Fixture();
        }
    }
}
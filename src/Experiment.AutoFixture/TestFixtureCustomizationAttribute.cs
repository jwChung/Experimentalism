using System;

namespace Jwc.Experiment.AutoFixture
{
    /// <summary>
    ///     Attribute to customize test fixture with <see cref="TestFixtureFactory"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public sealed class TestFixtureCustomizationAttribute : DefaultFixtureCustomizationAttribute
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="TestFixtureCustomizationAttribute" /> class.
        /// </summary>
        public TestFixtureCustomizationAttribute() : base(new TestFixtureFactory())
        {
        }
    }
}
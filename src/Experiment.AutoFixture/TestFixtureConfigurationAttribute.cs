namespace Jwc.Experiment.AutoFixture
{
    using System;

    /// <summary>
    /// Attribute to configure test fixture factory.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public sealed class TestFixtureConfigurationAttribute : DefaultFixtureConfigurationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestFixtureConfigurationAttribute" />
        /// class.
        /// </summary>
        public TestFixtureConfigurationAttribute()
            : base(new TestFixtureFactory())
        {
        }
    }
}
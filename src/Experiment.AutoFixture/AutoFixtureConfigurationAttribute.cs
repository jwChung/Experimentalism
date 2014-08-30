namespace Jwc.Experiment.AutoFixture
{
    using System;

    /// <summary>
    /// Attribute to configure test fixture factory.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public sealed class AutoFixtureConfigurationAttribute : TestFixtureConfigurationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AutoFixtureConfigurationAttribute" />
        /// class.
        /// </summary>
        public AutoFixtureConfigurationAttribute() : base(new TestFixtureFactory())
        {
        }
    }
}
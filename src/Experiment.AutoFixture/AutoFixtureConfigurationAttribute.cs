namespace Jwc.Experiment.AutoFixture
{
    using System;

    /// <summary>
    /// Attribute to configure test fixture factory.
    /// </summary>
    [Obsolete("This attribute is renamed TestFixtureConfigurationAttribute being a new class, not the old one. The attribute will be removed on the next major release.")]
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public sealed class AutoFixtureConfigurationAttribute : Experiment.TestFixtureConfigurationAttribute
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
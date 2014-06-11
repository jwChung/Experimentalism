using System;

namespace Jwc.Experiment.AutoFixture
{
    /// <summary>
    ///     Attribute to configure test fixture factory.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public sealed class TestFixtureyConfigurationAttribute : DefaultFixtureConfigurationAttribute
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="TestFixtureyConfigurationAttribute" /> class.
        /// </summary>
        public TestFixtureyConfigurationAttribute() : base(new TestFixtureFactory())
        {
        }
    }
}
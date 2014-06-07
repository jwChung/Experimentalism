using System;

namespace Jwc.Experiment.AutoFixture
{
    /// <summary>
    ///     Attribute to configure test fixture factory.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public sealed class TestFixtureFactoryConfigurationAttribute : DefaultFixtureFactoryConfigurationAttribute
    {
        /// <summary>
        ///     Initializes a new instance of the
        ///     <see cref="TestFixtureFactoryConfigurationAttribute" /> class.
        /// </summary>
        public TestFixtureFactoryConfigurationAttribute() : base(new TestFixtureFactory())
        {
        }
    }
}
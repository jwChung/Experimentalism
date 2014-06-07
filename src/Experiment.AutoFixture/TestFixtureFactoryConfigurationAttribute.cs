namespace Jwc.Experiment.AutoFixture
{
    /// <summary>
    ///     Attribute to configure test fixture factory.
    /// </summary>
    public class TestFixtureFactoryConfigurationAttribute : DefaultFixtureFactoryConfigurationAttribute
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
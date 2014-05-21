namespace Jwc.Experiment.Xunit
{
    /// <summary>
    /// Supplies harbor of <see cref="ITestFixtureFactory" />.
    /// </summary>
    public static class DefaultFixtureFactory
    {
        private static ITestFixtureFactory _testFixtureFactory;

        /// <summary>
        /// Gets a value inicating the current
        /// <see cref="ITestFixtureFactory" />.
        /// </summary>
        public static ITestFixtureFactory Current
        {
            get
            {
                return _testFixtureFactory ?? new NotSupportedFixtureFactory();
            }
        }

        /// <summary>
        /// Sets a factory of test fixture.
        /// </summary>
        /// <param name="testFixtureFactory">
        /// The factory of test fixture.
        /// </param>
        public static void SetCurrent(ITestFixtureFactory testFixtureFactory)
        {
            _testFixtureFactory = testFixtureFactory;
        }
    }
}
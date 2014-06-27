namespace Jwc.Experiment
{
    /// <summary>
    /// Supplies harbor of <see cref="ITestFixtureFactory" />.
    /// </summary>
    public static class DefaultFixtureFactory
    {
        private static ITestFixtureFactory testFixtureFactory;

        /// <summary>
        /// Gets a value indicating the current <see cref="ITestFixtureFactory" />.
        /// </summary>
        public static ITestFixtureFactory Current
        {
            get
            {
                return testFixtureFactory ?? new NotSupportedFixtureFactory();
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
            DefaultFixtureFactory.testFixtureFactory = testFixtureFactory;
        }
    }
}
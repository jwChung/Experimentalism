using System;
using System.Linq;
using System.Reflection;

namespace Jwc.Experiment.Xunit
{
    /// <summary>
    /// Supplies harbor of <see cref="ITestFixtureFactory" />.
    /// </summary>
    public static class TestFixtureFactory
    {
        private static ITestFixtureFactory _testFixtureFactory;
        private static readonly object _syncLock = new object();

        /// <summary>
        /// Gets a value inicating the current
        /// <see cref="ITestFixtureFactory" />.
        /// </summary>
        public static ITestFixtureFactory Current
        {
            get
            {
                return _testFixtureFactory ?? new NotSupportedFixtureFactory2();
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

        internal static ITestFixture Create(MethodInfo testMethod)
        {
            if (_testFixtureFactory == null)
            {
                lock (_syncLock)
                {
                    if (_testFixtureFactory == null)
                        _testFixtureFactory = CreateTestFixtureFactory(testMethod.ReflectedType.Assembly);
                }
            }

            return _testFixtureFactory.Create(testMethod);
        }

        private static ITestFixtureFactory CreateTestFixtureFactory(Assembly testAssembly)
        {
            var attribute = testAssembly
                .GetCustomAttributes(typeof(TestFixtureFactoryTypeAttribute), false)
                .Cast<TestFixtureFactoryTypeAttribute>().SingleOrDefault();

            if (attribute == null)
                return new NotSupportedFixtureFactory2();

            return (ITestFixtureFactory)Activator.CreateInstance(attribute.Type);
        }
    }
}
using System;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Jwc.Experiment
{
    internal static class TestFixtureFactory
    {
        private static ITestFixtureFactory _testFixtureFactory;
        private static readonly object _lockObj = new object();

        public static ITestFixture Create(MethodInfo testMethod)
        {
            if (_testFixtureFactory == null)
            {
                lock (_lockObj)
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
                return new NotSupportedTestFixtureFactory();

            return (ITestFixtureFactory)Activator.CreateInstance(attribute.Type);
        }

        private class NotSupportedTestFixtureFactory : ITestFixtureFactory
        {
            ITestFixture ITestFixtureFactory.Create(MethodInfo testMethod)
            {
                if (testMethod == null)
                    throw new ArgumentNullException("testMethod");

                throw new NotSupportedException(
                    String.Format(
                        CultureInfo.CurrentCulture,
                        "To create auto data, explicitly declare TestFixtureFactoryTypeAttribute " +
                        "on the test assembly '{0}'.",
                        testMethod.ReflectedType.Assembly));
            }
        }
    }
}
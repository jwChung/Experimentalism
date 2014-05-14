using System;
using System.Globalization;

namespace Jwc.Experiment
{
    /// <summary>
    /// An attribute to declare a type implementing the
    /// <see cref="ITestFixtureFactory"/> interface. An instance of type will be
    /// only once intitalized on a certain test-assembly(dll) and will create
    /// every instance of <see cref="ITestFixture"/> for all the tests.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public class TestFixtureFactoryAttribute : Attribute
    {
        private readonly Type _type;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="TestFixtureFactoryAttribute"/> class.
        /// </summary>
        /// <param name="type">
        /// The type implementing <see cref="ITestFixtureFactory"/>.
        /// </param>
        public TestFixtureFactoryAttribute(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            if (!typeof(ITestFixtureFactory).IsAssignableFrom(type))
                throw new ArgumentException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "The type '{0}' does not implement ITestFixtureFacotry.",
                        type),
                    "type");

            _type = type;
        }

        /// <summary>
        /// Gets a value indicating the type implementing
        /// <see cref="ITestFixtureFactory"/>.
        /// </summary>
        public Type Type
        {
            get
            {
                return _type;
            }
        }
    }
}
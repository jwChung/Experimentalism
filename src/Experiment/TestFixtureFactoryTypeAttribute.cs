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
    public sealed class TestFixtureFactoryTypeAttribute : Attribute
    {
        private readonly Type _type;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="TestFixtureFactoryTypeAttribute"/> class.
        /// </summary>
        /// <param name="type">
        /// The type implementing <see cref="ITestFixtureFactory"/>.
        /// </param>
        public TestFixtureFactoryTypeAttribute(Type type)
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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "This name is desiable to express a ITestFixture type.")]
        public Type Type
        {
            get
            {
                return _type;
            }
        }
    }
}
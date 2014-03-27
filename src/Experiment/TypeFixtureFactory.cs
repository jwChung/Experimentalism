using System;
using System.Reflection;

namespace Jwc.Experiment
{
    /// <summary>
    /// Represents a test factory to create an instance base on its type.
    /// </summary>
    public class TypeFixtureFactory : ITestFixtureFactory
    {
        private readonly Type _fixtureType;

        /// <summary>
        /// Initializes a new instance of the <see cref="TypeFixtureFactory"/> class.
        /// </summary>
        /// <param name="fixtureType">Type of the fixture.</param>
        public TypeFixtureFactory(Type fixtureType)
        {
            if (fixtureType == null)
            {
                throw new ArgumentNullException("fixtureType");
            }

            _fixtureType = fixtureType;
        }

        /// <summary>
        /// Gets a value indicating the type of the fixture.
        /// </summary>
        public Type FixtureType
        {
            get
            {
                return _fixtureType;
            }
        }

        /// <summary>
        /// Creates an instance of <see cref="ITestFixture" />. This uses
        /// a default constructor of supplied fixture type.
        /// </summary>
        /// <param name="method">
        /// The method to be called when a test is executed.
        /// </param>
        /// <returns>
        /// The result instance.
        /// </returns>
        public ITestFixture Create(MethodInfo method)
        {
            throw new System.NotImplementedException();
        }
    }
}
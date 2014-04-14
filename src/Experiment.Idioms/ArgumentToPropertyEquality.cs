using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ploeh.Albedo;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represent comaprer to determine that an argument value equals to
    /// a property value.
    /// </summary>
    public class ArgumentToPropertyEquality : IEqualityComparer<IReflectionElement>
    {
        private readonly ITestFixture _testFixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentToPropertyEquality" /> class.
        /// </summary>
        /// <param name="testFixture">
        /// The test fixture to create an anonymous specimen.
        /// </param>
        public ArgumentToPropertyEquality(ITestFixture testFixture)
        {
            _testFixture = testFixture;
        }

        /// <summary>
        /// Gets a vlaue indicating the test fixture.
        /// </summary>
        public ITestFixture TestFixture
        {
            get
            {
                return _testFixture;
            }
        }

        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <returns>
        /// true if the specified objects are equal; otherwise, false.
        /// </returns>
        /// <param name="x">
        /// The first object of type <paramref name="x" /> to compare.
        /// </param>
        /// <param name="y">
        /// The second object of type <paramref name="y" /> to compare.
        /// </param>
        public bool Equals(IReflectionElement x, IReflectionElement y)
        {
            var paremeterInfoElement = x as ParameterInfoElement;
            if (paremeterInfoElement == null)
            {
                return false;
            }

            var propertyInfoElement = y as PropertyInfoElement;
            if (propertyInfoElement == null)
            {

                return false;
            }

            var parameterInfo = paremeterInfoElement.ParameterInfo;
            var propertyInfo = propertyInfoElement.PropertyInfo;

            var constructorInfo = parameterInfo.Member as ConstructorInfo;
            if (constructorInfo == null)
            {
                return false;
            }

            if (constructorInfo.ReflectedType != propertyInfo.ReflectedType)
            {
                return false;
            }

            var arguments = constructorInfo.GetParameters()
                .Select(pi => TestFixture.Create(pi.ParameterType))
                .ToArray();
            var target = constructorInfo.Invoke(arguments);

            var argumentValue = arguments[parameterInfo.Position];
            var propetyValue = propertyInfo.GetValue(target, null);

            return argumentValue.Equals(propetyValue);
        }

        /// <summary>
        /// Returns a hash code for the specified object.
        /// </summary>
        /// <returns>
        /// A hash code for the specified object.
        /// </returns>
        /// <param name="obj">
        /// The <see cref="object" /> for which a hash code is to be returned.
        /// </param>
        public int GetHashCode(IReflectionElement obj)
        {
            return 0;
        }
    }
}
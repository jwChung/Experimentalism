using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ploeh.Albedo;

namespace Jwc.Experiment
{
    /// <summary>
    ///     Represent comaprer to determine that a parameter value equals to a field value.
    /// </summary>
    public class ParameterToFieldComparer : IEqualityComparer<IReflectionElement>
    {
        private readonly ITestFixture _testFixture;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ParameterToFieldComparer" /> class.
        /// </summary>
        /// <param name="testFixture">
        ///     The test fixture to create an anonymous specimen.
        /// </param>
        public ParameterToFieldComparer(ITestFixture testFixture)
        {
            if (testFixture == null)
            {
                throw new ArgumentNullException("testFixture");
            }

            _testFixture = testFixture;
        }

        /// <summary>
        ///     Gets a vlaue indicating the test fixture.
        /// </summary>
        public ITestFixture TestFixture
        {
            get
            {
                return _testFixture;
            }
        }

        /// <summary>
        ///     Determines whether the specified objects are equal.
        /// </summary>
        /// <returns>
        ///     true if the specified objects are equal; otherwise, false.
        /// </returns>
        /// <param name="x">
        ///     The first object of type <paramref name="x" /> to compare.
        /// </param>
        /// <param name="y">
        ///     The second object of type <paramref name="y" /> to compare.
        /// </param>
        public bool Equals(IReflectionElement x, IReflectionElement y)
        {
            var parameterInfoElement = x as ParameterInfoElement;
            if (parameterInfoElement == null)
                return false;

            var fieldInfoElement = y as FieldInfoElement;
            if (fieldInfoElement == null)
                return false;

            var parameterInfo = parameterInfoElement.ParameterInfo;
            var constructorInfo = parameterInfo.Member as ConstructorInfo;
            if (constructorInfo == null)
                return false;

            var fieldInfo = fieldInfoElement.FieldInfo;
            if (constructorInfo.ReflectedType != fieldInfo.ReflectedType)
                return false;

            var arguments = constructorInfo.GetParameters()
                .Select(pi => TestFixture.Create(pi.ParameterType))
                .ToArray();
            var target = constructorInfo.Invoke(arguments);
            var argumentValue = arguments[parameterInfo.Position];
            var fieldValue = fieldInfo.GetValue(target);

            var enumerableArgument = argumentValue as IEnumerable;
            var enumerableField = fieldValue as IEnumerable;
            if (enumerableArgument != null && enumerableField != null)
            {
                return enumerableArgument.Cast<object>()
                    .SequenceEqual(enumerableField.Cast<object>());
            }

            return argumentValue.Equals(fieldValue);
        }

        /// <summary>
        ///     Returns a hash code for the specified object.
        /// </summary>
        /// <returns>
        ///     A hash code for the specified object.
        /// </returns>
        /// <param name="obj">
        ///     The <see cref="object" /> for which a hash code is to be returned.
        /// </param>
        public int GetHashCode(IReflectionElement obj)
        {
            return 0;
        }
    }
}
namespace Jwc.Experiment
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Ploeh.Albedo;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;

    /// <summary>
    /// Represent comparer to determine that a parameter value equals to a field value.
    /// </summary>
    public class ParameterToFieldComparer : IEqualityComparer<IReflectionElement>
    {
        private readonly ISpecimenBuilder builder;
       
        /// <summary>
        /// Initializes a new instance of the <see cref="ParameterToFieldComparer" /> class.
        /// </summary>
        /// <param name="builder">
        /// The builder to create an anonymous specimen.
        /// </param>
        public ParameterToFieldComparer(ISpecimenBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException("builder");

            this.builder = builder;
        }

        /// <summary>
        /// Gets a value indicating the builder.
        /// </summary>
        public ISpecimenBuilder Builder
        {
            get { return this.builder; }
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
                .Select(pi => this.builder.CreateAnonymous(pi))
                .ToArray();
            object target;
            try
            {
                target = constructorInfo.Invoke(arguments);
            }
            catch (TargetInvocationException)
            {
                return false;
            }

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
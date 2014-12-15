namespace Jwc.Experiment.Idioms
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using Ploeh.Albedo;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;

    /// <summary>
    /// Encapsulates a unit test that verifies that members (property or field) are correctly
    /// initialized by a constructor.
    /// </summary>
    public class MemberInitializationAssertion : IdiomaticAssertion
    {
        private readonly ISpecimenBuilder builder;
        private readonly IEqualityComparer<IReflectionElement> parameterToMemberComparer;
        private readonly IEqualityComparer<IReflectionElement> memberToParameterComparer;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberInitializationAssertion" /> class.
        /// </summary>
        /// <param name="builder">
        /// A fixture to crete auto-data.
        /// </param>
        public MemberInitializationAssertion(ISpecimenBuilder builder) : this(
            new OrEqualityComparer<IReflectionElement>(
                new ParameterToPropertyComparer(builder),
                new ParameterToFieldComparer(builder)),
            new OrEqualityComparer<IReflectionElement>(
                new PropertyToParameterComparer(builder),
                new FieldToParameterComparer(builder)))
        {
            this.builder = builder;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberInitializationAssertion" /> class.
        /// </summary>
        /// <param name="parameterToMemberComparer">
        /// A comparer to determine if a given parameter value of a constructor equals to a
        /// member(property or field) value.
        /// </param>
        /// <param name="memberToParameterComparer">
        /// A comparer to determine if a given member(property or field) value equals to a parameter
        /// value of a constructor.
        /// </param>
        public MemberInitializationAssertion(
            IEqualityComparer<IReflectionElement> parameterToMemberComparer,
            IEqualityComparer<IReflectionElement> memberToParameterComparer)
        {
            if (parameterToMemberComparer == null)
                throw new ArgumentNullException("parameterToMemberComparer");

            if (memberToParameterComparer == null)
                throw new ArgumentNullException("memberToParameterComparer");

            this.parameterToMemberComparer = parameterToMemberComparer;
            this.memberToParameterComparer = memberToParameterComparer;
        }

        /// <summary>
        /// Gets a value indicating the test fixture.
        /// </summary>
        public ISpecimenBuilder Builder
        {
            get { return this.builder; }
        }

        /// <summary>
        /// Gets a value to compare a parameter with a member(field or property).
        /// </summary>
        public IEqualityComparer<IReflectionElement> ParameterToMemberComparer
        {
            get
            {
                return this.parameterToMemberComparer;
            }
        }

        /// <summary>
        /// Gets a value to compare a member(field or property) with a parameter.
        /// </summary>
        public IEqualityComparer<IReflectionElement> MemberToParameterComparer
        {
            get
            {
                return this.memberToParameterComparer;
            }
        }

        /// <summary>
        /// Verifies that a field correctly exposes a certain parameter of a constructor.
        /// </summary>
        /// <param name="field">
        /// The field.
        /// </param>
        public override void Verify(FieldInfo field)
        {
            if (field == null)
                throw new ArgumentNullException("field");

            if (field.IsStatic || field.ReflectedType.IsEnum)
                return;

            var fieldInfoElement = field.ToElement();
            var parameterInfoElements = GetParameterInfoElements(field.ReflectedType);

            if (parameterInfoElements.Any(p => this.MemberToParameterComparer.Equals(fieldInfoElement, p)))
                return;

            var messageFormat = @"No constructors with an argument that matches the field were found.
Declaring type: {0}
Reflected type: {1}
Field         : {2}";

            throw new MemberInitializationException(
                string.Format(
                    CultureInfo.CurrentCulture,
                    messageFormat,
                    field.DeclaringType,
                    field.ReflectedType,
                    fieldInfoElement));
        }

        /// <summary>
        /// Verifies that all parameters of a constructor are correctly exposed through fields or
        /// properties.
        /// </summary>
        /// <param name="constructor">
        /// The constructor.
        /// </param>
        public override void Verify(ConstructorInfo constructor)
        {
            if (constructor == null)
                throw new ArgumentNullException("constructor");

            var parameterInfoElements = GetParameterInfoElements(constructor);
            var memberElements = GetMemberElements(constructor.ReflectedType).ToArray();

            foreach (var parameterInfoElement in parameterInfoElements)
            {
                if (memberElements.Any(m => this.ParameterToMemberComparer.Equals(parameterInfoElement, m)))
                    continue;

                var messageFormat = @"The constructor parameter was not exposed through any fields or properties.
Type       : {0}
Constructor: {1}
Parameter  : {2}";

                throw new MemberInitializationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        messageFormat,
                        constructor.DeclaringType,
                        constructor,
                        parameterInfoElement.ParameterInfo));
            }
        }

        /// <summary>
        /// Verifies that a field correctly exposes a certain parameter of a constructor.
        /// </summary>
        /// <param name="property">
        /// The property.
        /// </param>
        public override void Verify(PropertyInfo property)
        {
            if (property == null)
                throw new ArgumentNullException("property");

            if (property.GetGetMethod() == null
                || property.IsStatic()
                || property.IsAbstract()
                || property.GetIndexParameters().Length != 0)
                return;

            var propertyInfoElement = property.ToElement();
            var parameterInfoElements = GetParameterInfoElements(property.ReflectedType);

            if (parameterInfoElements.Any(p => this.MemberToParameterComparer.Equals(propertyInfoElement, p)))
                return;

            var messageFormat = @"No constructors with an argument that matches the property were found.
Declaring type: {0}
Reflected type: {1}
Property      : {2}";

            throw new MemberInitializationException(
                string.Format(
                    CultureInfo.CurrentCulture,
                    messageFormat,
                    property.DeclaringType,
                    property.ReflectedType,
                    propertyInfoElement));
        }

        private static IEnumerable<ParameterInfoElement> GetParameterInfoElements(Type reflectedType)
        {
            return reflectedType.GetConstructors().SelectMany(GetParameterInfoElements);
        }

        private static IEnumerable<ParameterInfoElement> GetParameterInfoElements(ConstructorInfo constructor)
        {
            return constructor.GetParameters().Select(x => x.ToElement());
        }

        private static IEnumerable<IReflectionElement> GetMemberElements(Type reflectedType)
        {
            return GetPropertyInfoElements(reflectedType)
                .Concat(GetFieldInfoElements(reflectedType));
        }

        private static IEnumerable<IReflectionElement> GetPropertyInfoElements(Type reflectedType)
        {
            return reflectedType.GetProperties().Select(x => x.ToElement());
        }

        private static IEnumerable<IReflectionElement> GetFieldInfoElements(Type reflectedType)
        {
            return reflectedType.GetFields().Select(x => x.ToElement());
        }
    }
}
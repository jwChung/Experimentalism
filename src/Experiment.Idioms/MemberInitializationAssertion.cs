using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Ploeh.Albedo;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Encapsulates a unit test that verifies that members (property or field)
    /// are correctly intialized by a constructor.
    /// </summary>
    public class MemberInitializationAssertion :
        IdiomaticMemberAssertion, IIdiomaticTypeAssertion, IIdiomaticAssemblyAssertion
    {
        private readonly ITestFixture _testFixture;
        private readonly IEqualityComparer<IReflectionElement> _parameterToMemberComparer;
        private readonly IEqualityComparer<IReflectionElement> _memberToParameterComparer;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberInitializationAssertion" /> class.
        /// </summary>
        /// <param name="testFixture">
        /// A test fixture to crete auto-data.
        /// </param>
        public MemberInitializationAssertion(ITestFixture testFixture) : this(
            new OrEqualityComparer<IReflectionElement>(
                new ParameterToPropertyComparer(testFixture),
                new ParameterToFieldComparer(testFixture)),
            new OrEqualityComparer<IReflectionElement>(
                new PropertyToParameterComparer(testFixture),
                new FieldToParameterComparer(testFixture)))
        {
            _testFixture = testFixture;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberInitializationAssertion" /> class.
        /// </summary>
        /// <param name="parameterToMemberComparer">
        /// A comparer to determine if a given parameter value of
        /// a constructor equals to a member(property or field) value.
        /// </param>
        /// <param name="memberToParameterComparer">
        /// A comparer to determine if a given member(property or field) value
        /// equals to a parameter value of a constructor.
        /// </param>
        public MemberInitializationAssertion(
            IEqualityComparer<IReflectionElement> parameterToMemberComparer,
            IEqualityComparer<IReflectionElement> memberToParameterComparer)
        {
            if (parameterToMemberComparer == null)
                throw new ArgumentNullException("parameterToMemberComparer");

            if (memberToParameterComparer == null)
                throw new ArgumentNullException("memberToParameterComparer");

            _parameterToMemberComparer = parameterToMemberComparer;
            _memberToParameterComparer = memberToParameterComparer;
        }

        /// <summary>
        /// Gets a value indicating the test fixture.
        /// </summary>
        public ITestFixture TestFixture
        {
            get
            {
                return _testFixture;
            }
        }

        /// <summary>
        /// Gets a value to compare a parameter with a member(field or propety).
        /// </summary>
        public IEqualityComparer<IReflectionElement> ParameterToMemberComparer
        {
            get
            {
                return _parameterToMemberComparer;
            }
        }

        /// <summary>
        /// Gets a value to compare a member(field or propety) with a parameter.
        /// </summary>
        public IEqualityComparer<IReflectionElement> MemberToParameterComparer
        {
            get
            {
                return _memberToParameterComparer;
            }
        }

        /// <summary>
        /// verifies that members (property or field) of all types of an
        /// specified assembly are correctly intialized by a constructor of
        /// a certain type in the assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        public void Verify(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");

            foreach (var type in assembly.GetExportedTypes())
                Verify(type);
        }

        /// <summary>
        /// verifies that members (property or field) of a specified type
        /// are correctly intialized by a constructor of the type.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        public virtual void Verify(Type type)
        {
            foreach (var member in type.GetIdiomaticMembers())
                Verify(member);
        }

        /// <summary>
        /// Verifies that a field correctly exposes a certain parameter of a
        /// constructor.
        /// </summary>
        /// <param name="field">The field.</param>
        public override void Verify(FieldInfo field)
        {
            if (field == null)
                throw new ArgumentNullException("field");

            var fieldInfoElement = field.ToElement();
            var parameterInfoElements = GetParameterInfoElements(field.ReflectedType);

            if (parameterInfoElements.Any(p => MemberToParameterComparer.Equals(fieldInfoElement, p)))
                return;

            throw new MemberInitializationException(
                string.Format(
                    CultureInfo.CurrentCulture,
                    "No constructors with an argument that matches the field were found.{0}" +
                    "Declaring type: {1}{0}" +
                    "Reflected type: {2}{0}" +
                    "Field: {3}",
                    Environment.NewLine,
                    field.DeclaringType,
                    field.ReflectedType,
                    fieldInfoElement));
        }

        /// <summary>
        /// Verifies that all parameters of a constructor are correctly exposed
        /// through fields or properties.
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
                if (memberElements.Any(m => ParameterToMemberComparer.Equals(parameterInfoElement, m)))
                    continue;

                throw new MemberInitializationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "The constructor parameter was not exposed through any fields or properties.{0}" +
                        "Type: {1}{0}" +
                        "Constructor: {2}{0}" +
                        "Parameter: {3}",
                        Environment.NewLine,
                        constructor.DeclaringType,
                        constructor,
                        parameterInfoElement.ParameterInfo));
            }
        }

        /// <summary>
        /// Verifies that a field correctly exposes a certain parameter of a
        /// constructor.
        /// </summary>
        /// <param name="property">The property.</param>
        public override void Verify(PropertyInfo property)
        {
            if (property == null)
                throw new ArgumentNullException("property");

            var propertyInfoElement = property.ToElement();
            var parameterInfoElements = GetParameterInfoElements(property.ReflectedType);

            if (parameterInfoElements.Any(p => MemberToParameterComparer.Equals(propertyInfoElement, p)))
                return;

            throw new MemberInitializationException(
                string.Format(
                    CultureInfo.CurrentCulture,
                    "No constructors with an argument that matches the property were found.{0}" +
                    "Declaring type: {1}{0}" +
                    "Reflected type: {2}{0}" +
                    "Property: {3}",
                    Environment.NewLine,
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
            return reflectedType.GetFields().Select(x => x.ToElement());
        }

        private static IEnumerable<IReflectionElement> GetFieldInfoElements(Type reflectedType)
        {
            return reflectedType.GetProperties().Select(x => x.ToElement());
        }
    }
}
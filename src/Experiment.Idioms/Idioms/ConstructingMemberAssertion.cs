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
    public class ConstructingMemberAssertion : ReflectionVisitor<object>
    {
        private readonly AccessibilityCollector _accessibilityCollector
            = new AccessibilityCollector();
        private readonly ITestFixture _testFixture;
        private readonly IEqualityComparer<IReflectionElement> _parameterToMemberComparer;
        private readonly IEqualityComparer<IReflectionElement> _memberToParameterComparer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstructingMemberAssertion" /> class.
        /// </summary>
        /// <param name="parameterToMemberComparer">
        /// The comparer to determine if a given parameter value of
        /// a constructor equals to a member(property or field) value.
        /// </param>
        /// <param name="memberToParameterComparer">
        /// The comparer to determine if a given member(property or field) value
        /// equals to a parameter value of a constructor.
        /// </param>
        public ConstructingMemberAssertion(
            IEqualityComparer<IReflectionElement> parameterToMemberComparer,
            IEqualityComparer<IReflectionElement> memberToParameterComparer)
        {
            if (parameterToMemberComparer == null)
            {
                throw new ArgumentNullException("parameterToMemberComparer");
            }

            if (memberToParameterComparer == null)
            {
                throw new ArgumentNullException("memberToParameterComparer");
            }

            _parameterToMemberComparer = parameterToMemberComparer;
            _memberToParameterComparer = memberToParameterComparer;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConstructingMemberAssertion"/> class.
        /// </summary>
        /// <param name="testFixture">
        /// The test fixture.
        /// </param>
        public ConstructingMemberAssertion(ITestFixture testFixture) : this(
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
        /// Gets the observation or value produced by this instance.
        /// </summary>
        public override object Value
        {
            get
            {
                throw new NotSupportedException(
                    "This Value property isn't supported because the main purpose of " +
                    "this class is to verify whether members (property or field) are correctly " +
                    "initialized by a constructor.");
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
        /// Allows <see cref="TypeElement" /> instances to be 'visited'.
        /// This method is called when the elements 'accepts' this visitor instance.
        /// </summary>
        /// <param name="typeElements">
        /// The <see cref="T:Ploeh.Albedo.TypeElement" /> instances being visited.
        /// </param>
        /// <returns>
        /// A (potentially) new <see cref="IReflectionVisitor{T}" /> instance which can be
        /// used to continue the visiting process with potentially updated observations.
        /// </returns>
        public override IReflectionVisitor<object> Visit(
            params TypeElement[] typeElements)
        {
            return base.Visit(GetPublicReflectionElements(typeElements.Where(e => !e.Type.IsInterface)));
        }

        /// <summary>
        /// Allows <see cref="FieldInfoElement" /> instances to be 'visited'.
        /// This method is called when the elements 'accepts' this visitor instance.
        /// </summary>
        /// <param name="fieldInfoElements">
        /// The <see cref="FieldInfoElement" /> instances being visited.
        /// </param>
        /// <returns>
        /// A (potentially) new <see cref="IReflectionVisitor{T}" /> instance which can be
        /// used to continue the visiting process with potentially updated observations.
        /// </returns>
        public override IReflectionVisitor<object> Visit(
            params FieldInfoElement[] fieldInfoElements)
        {
            return base.Visit(GetPublicReflectionElements(fieldInfoElements));
        }

        /// <summary>
        /// Allows <see cref="ConstructorInfoElement" /> instances to be 'visited'.
        /// This method is called when the elements 'accepts' this visitor instance.
        /// </summary>
        /// <param name="constructorInfoElements">
        /// The <see cref="ConstructorInfoElement" /> instances being visited.
        /// </param>
        /// <returns>
        /// A (potentially) new <see cref="IReflectionVisitor{T}" /> instance which can be
        /// used to continue the visiting process with potentially updated observations.
        /// </returns>
        public override IReflectionVisitor<object> Visit(
            params ConstructorInfoElement[] constructorInfoElements)
        {
            return base.Visit(GetPublicReflectionElements(constructorInfoElements));
        }

        /// <summary>
        /// Allows <see cref="PropertyInfoElement"/> instances to be 'visited'.
        /// This method is called when the elements 'accepts' this visitor instance.
        /// </summary>
        /// <param name="propertyInfoElements">
        /// The <see cref="PropertyInfoElement"/> instances being visited.
        /// </param>
        /// <returns>
        /// A (potentially) new <see cref="IReflectionVisitor{T}"/> instance which can be
        /// used to continue the visiting process with potentially updated observations.
        /// </returns>
        public override IReflectionVisitor<object> Visit(
            params PropertyInfoElement[] propertyInfoElements)
        {
            var elements = propertyInfoElements.Where(e => e.PropertyInfo.GetGetMethod() != null).ToArray();
            return base.Visit(elements);
        }

        /// <summary>
        /// Allows an <see cref="ConstructorInfoElement" /> to be visited.
        /// This method is called when the element accepts this visitor
        /// instance.
        /// </summary>
        /// <param name="constructorInfoElement">
        /// The <see cref="ConstructorInfoElement" /> being visited.
        /// </param>
        /// <returns>
        /// A <see cref="IReflectionVisitor{T}" /> instance which can be used
        /// to continue the visiting process with potentially updated
        /// observations.
        /// </returns>
        public override IReflectionVisitor<object> Visit(ConstructorInfoElement constructorInfoElement)
        {
            if (constructorInfoElement == null)
            {
                throw new ArgumentNullException("constructorInfoElement");
            }

            const BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public;

            var constructorInfo = constructorInfoElement.ConstructorInfo;
            var reflectedType = constructorInfo.ReflectedType;
            
            var propertyInfoElements = reflectedType.GetProperties(bindingFlags)
                .Where(pi => pi.GetGetMethod() != null)
                .Select(pi => pi.ToElement()).ToArray();

            var fieldInfoElements = reflectedType.GetFields(bindingFlags)
                .Select(fi => fi.ToElement()).ToArray();

            foreach (var parameterInfo in constructorInfo.GetParameters())
            {
                var parameterInfoElement = parameterInfo.ToElement();
                if (IsSatisfied(parameterInfoElement, propertyInfoElements, fieldInfoElements))
                {
                    continue;
                }

                throw new ConstructingMemberException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "The constructor parameter was not exposed through any fields or properties:{0}" +
                        "Reflected type: {1}{0}" +
                        "Constructor: {2}{0}" +
                        "Parameter: {3}",
                        Environment.NewLine,
                        reflectedType,
                        constructorInfoElement,
                        parameterInfoElement));
            }

            return this;
        }

        /// <summary>
        /// Allows an <see cref="FieldInfoElement" /> to be visited.
        /// This method is called when the element accepts this visitor
        /// instance.
        /// </summary>
        /// <param name="fieldInfoElement">The <see cref="FieldInfoElement" /> being visited.
        /// </param>
        /// <returns>
        /// A <see cref="IReflectionVisitor{T}" /> instance which can be used
        /// to continue the visiting process with potentially updated
        /// observations.
        /// </returns>
        public override IReflectionVisitor<object> Visit(FieldInfoElement fieldInfoElement)
        {
            if (fieldInfoElement == null)
            {
                throw new ArgumentNullException("fieldInfoElement");
            }

            var reflectedType = fieldInfoElement.FieldInfo.ReflectedType;
            if (GetParameterInfoElements(reflectedType).Any(
                e => MemberToParameterComparer.Equals(fieldInfoElement, e)))
            {
                return this;
            }

            throw new ConstructingMemberException(
                string.Format(
                    CultureInfo.CurrentCulture,
                    "No constructors with an argument that matches the field were found:{0}" +
                    "Reflected type: {1}{0}" +
                    "Field: {2}",
                    Environment.NewLine,
                    reflectedType,
                    fieldInfoElement));
        }

        /// <summary>
        /// Allows an <see cref="PropertyInfoElement" /> to be visited.
        /// This method is called when the element accepts this visitor
        /// instance.
        /// </summary>
        /// <param name="propertyInfoElement">The <see cref="PropertyInfoElement" /> being visited.
        /// </param>
        /// <returns>
        /// A <see cref="IReflectionVisitor{T}" /> instance which can be used
        /// to continue the visiting process with potentially updated
        /// observations.
        /// </returns>
        public override IReflectionVisitor<object> Visit(PropertyInfoElement propertyInfoElement)
        {
            if (propertyInfoElement == null)
            {
                throw new ArgumentNullException("propertyInfoElement");
            }

            var reflectedType = propertyInfoElement.PropertyInfo.ReflectedType;
            if (GetParameterInfoElements(reflectedType).Any(
                e => MemberToParameterComparer.Equals(propertyInfoElement, e)))
            {
                return this;
            }

            const string messageFormat =
                    "No constructors with an argument that matches the property were found:" +
                    "{0}Reflected type: {1}{0}Property: {2}";

            throw new ConstructingMemberException(
                string.Format(
                    CultureInfo.CurrentCulture,
                    messageFormat,
                    Environment.NewLine,
                    reflectedType,
                    propertyInfoElement));
        }

        private T[] GetPublicReflectionElements<T>(IEnumerable<T> reflectionElements)
            where T : IReflectionElement
        {
            return reflectionElements.Where(IsPublic).ToArray();
        }

        private bool IsPublic<T>(T reflectionElement) where T : IReflectionElement
        {
            var accessibilities = reflectionElement.Accept(_accessibilityCollector).Value.Single();
            return (accessibilities & Accessibilities.Public) == Accessibilities.Public;
        }

        private bool IsSatisfied(
            ParameterInfoElement parameterInfoElement,
            IEnumerable<PropertyInfoElement> properteis,
            IEnumerable<FieldInfoElement> fields)
        {
            return properteis.Any(
                property => ParameterToMemberComparer.Equals(
                    parameterInfoElement, property))
            || fields.Any(
                field => ParameterToMemberComparer.Equals(
                    parameterInfoElement, field));
        }

        private static IEnumerable<ParameterInfoElement> GetParameterInfoElements(Type reflectedType)
        {
            return reflectedType.GetConstructors()
                .SelectMany(ci => ci.GetParameters())
                .Select(pi => pi.ToElement());
        }
    }
}
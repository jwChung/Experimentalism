﻿using System;
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

                const string messageFormat =
                    "The constructor parameter was not exposed through any fields or properties:" +
                    "{0}Reflected type: {1}{0}Constructor: {2}{0}Parameter: {3}";

                throw new ConstructingMemberException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        messageFormat,
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

            const string messageFormat =
                    "No constructors with an argument that matches the field were found:" +
                    "{0}Reflected type: {1}{0}Field: {2}";

            throw new ConstructingMemberException(
                string.Format(
                    CultureInfo.CurrentCulture,
                    messageFormat,
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
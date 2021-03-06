﻿namespace Jwc.Experiment
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Ploeh.Albedo;

    /// <summary>
    /// Represents a class to collect <see cref="Accessibilities" />.
    /// </summary>
    public class AccessibilityCollector : ReflectionVisitor<IEnumerable<Accessibilities>>
    {
        private readonly IEnumerable<Accessibilities> values;

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessibilityCollector" /> class.
        /// </summary>
        public AccessibilityCollector() : this(new Accessibilities[0])
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccessibilityCollector" /> class.
        /// </summary>
        /// <param name="values">
        /// The value.
        /// </param>
        protected AccessibilityCollector(IEnumerable<Accessibilities> values)
        {
            this.values = values;
        }

        /// <summary>
        /// Gets the observation or value produced by this instance.
        /// </summary>
        public override IEnumerable<Accessibilities> Value
        {
            get
            {
                return this.values;
            }
        }

        /// <summary>
        /// Allows an <see cref="TypeElement" /> to be visited. This method is called when the
        /// element accepts this visitor instance.
        /// </summary>
        /// <param name="typeElement">
        /// The <see cref="TypeElement" /> being visited.
        /// </param>
        /// <returns>
        /// A <see cref="IReflectionVisitor{T}" /> instance which can be used to continue the
        /// visiting process with potentially updated observations.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<Accessibilities>> Visit(
            TypeElement typeElement)
        {
            if (typeElement == null)
            {
                throw new ArgumentNullException("typeElement");
            }

            var type = typeElement.Type;

            var accessibilities = Accessibilities.None;
            if (type.IsPublic) accessibilities = Accessibilities.Public;
            else if (type.IsNotPublic) accessibilities = Accessibilities.Internal;
            else if (type.IsNestedPublic) accessibilities = Accessibilities.Public;
            else if (type.IsNestedFamORAssem) accessibilities = Accessibilities.ProtectedInternal;
            else if (type.IsNestedFamily) accessibilities = Accessibilities.Protected;
            else if (type.IsNestedAssembly) accessibilities = Accessibilities.Internal;
            else if (type.IsNestedPrivate) accessibilities = Accessibilities.Private;

            return new AccessibilityCollector(this.Value.Concat(new[] { accessibilities }));
        }

        /// <summary>
        /// Allows an <see cref="FieldInfoElement" /> to be visited. This method is called when the
        /// element accepts this visitor instance.
        /// </summary>
        /// <param name="fieldInfoElement">
        /// The <see cref="FieldInfoElement" /> being visited.
        /// </param>
        /// <returns>
        /// A <see cref="IReflectionVisitor{T}" /> instance which can be used to continue the
        /// visiting process with potentially updated observations.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<Accessibilities>> Visit(
            FieldInfoElement fieldInfoElement)
        {
            if (fieldInfoElement == null)
            {
                throw new ArgumentNullException("fieldInfoElement");
            }

            var fieldInfo = fieldInfoElement.FieldInfo;

            var accessibilities = Accessibilities.None;
            if (fieldInfo.IsPublic) accessibilities = Accessibilities.Public;
            else if (fieldInfo.IsFamilyOrAssembly) accessibilities = Accessibilities.ProtectedInternal;
            else if (fieldInfo.IsFamily) accessibilities = Accessibilities.Protected;
            else if (fieldInfo.IsAssembly) accessibilities = Accessibilities.Internal;
            else if (fieldInfo.IsPrivate) accessibilities = Accessibilities.Private;

            return new AccessibilityCollector(this.Value.Concat(new[] { accessibilities }));
        }

        /// <summary>
        /// Allows an <see cref="ConstructorInfoElement" /> to be visited. This method is called
        /// when the element accepts this visitor instance.
        /// </summary>
        /// <param name="constructorInfoElement">
        /// The <see cref="ConstructorInfoElement" /> being visited.
        /// </param>
        /// <returns>
        /// A <see cref="IReflectionVisitor{T}" /> instance which can be used to continue the
        /// visiting process with potentially updated observations.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<Accessibilities>> Visit(
            ConstructorInfoElement constructorInfoElement)
        {
            if (constructorInfoElement == null)
            {
                throw new ArgumentNullException("constructorInfoElement");
            }

            return new AccessibilityCollector(
                this.Value.Concat(new[] { GetAccessibilities(constructorInfoElement.ConstructorInfo) }));
        }

        /// <summary>
        /// Allows an <see cref="PropertyInfoElement" /> to be visited. This method is called when
        /// the element accepts this visitor instance.
        /// </summary>
        /// <param name="propertyInfoElement">
        /// The <see cref="PropertyInfoElement" /> being visited.
        /// </param>
        /// <returns>
        /// A <see cref="IReflectionVisitor{T}" /> instance which can be used to continue the
        /// visiting process with potentially updated observations.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<Accessibilities>> Visit(
            PropertyInfoElement propertyInfoElement)
        {
            if (propertyInfoElement == null)
            {
                throw new ArgumentNullException("propertyInfoElement");
            }

            var propertyInfo = propertyInfoElement.PropertyInfo;

            var getMethod = propertyInfo.GetGetMethod(true);
            var setMethod = propertyInfo.GetSetMethod(true);

            IReflectionVisitor<IEnumerable<Accessibilities>> visitor;
            var accessibilities = Accessibilities.None;

            if (getMethod != null)
            {
                visitor = this.Visit(getMethod.ToElement());
                accessibilities |= visitor.Value.Last();
            }

            if (setMethod != null)
            {
                visitor = this.Visit(setMethod.ToElement());
                accessibilities |= visitor.Value.Last();
            }

            return new AccessibilityCollector(this.Value.Concat(new[] { accessibilities }));
        }

        /// <summary>
        /// Allows an <see cref="MethodInfoElement" /> to be visited. This method is called when
        /// the element accepts this visitor instance.
        /// </summary>
        /// <param name="methodInfoElement">
        /// The <see cref="MethodInfoElement" /> being visited.
        /// </param>
        /// <returns>
        /// A <see cref="IReflectionVisitor{T}" /> instance which can be used to continue the
        /// visiting process with potentially updated observations.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<Accessibilities>> Visit(
            MethodInfoElement methodInfoElement)
        {
            if (methodInfoElement == null)
            {
                throw new ArgumentNullException("methodInfoElement");
            }

            return new AccessibilityCollector(
                this.Value.Concat(new[] { GetAccessibilities(methodInfoElement.MethodInfo) }));
        }

        /// <summary>
        /// Allows an <see cref="EventInfoElement" /> to be visited. This method is called when
        /// the element accepts this visitor instance.
        /// </summary>
        /// <param name="eventInfoElement">
        /// The <see cref="EventInfoElement" /> being visited.
        /// </param>
        /// <returns>
        /// A <see cref="IReflectionVisitor{T}" /> instance which can be used to continue the
        /// visiting process with potentially updated observations.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<Accessibilities>> Visit(
            EventInfoElement eventInfoElement)
        {
            if (eventInfoElement == null)
            {
                throw new ArgumentNullException("eventInfoElement");
            }

            return this.Visit(eventInfoElement.EventInfo.GetAddMethod(true).ToElement());
        }

        private static Accessibilities GetAccessibilities(MethodBase constructorInfo)
        {
            var accessibilities = Accessibilities.None;
            if (constructorInfo.IsPublic) accessibilities = Accessibilities.Public;
            else if (constructorInfo.IsFamilyOrAssembly) accessibilities = Accessibilities.ProtectedInternal;
            else if (constructorInfo.IsFamily) accessibilities = Accessibilities.Protected;
            else if (constructorInfo.IsAssembly) accessibilities = Accessibilities.Internal;
            else if (constructorInfo.IsPrivate) accessibilities = Accessibilities.Private;
            return accessibilities;
        }
    }
}
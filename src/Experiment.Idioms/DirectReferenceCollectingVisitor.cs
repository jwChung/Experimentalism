﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ploeh.Albedo;

namespace Jwc.Experiment
{
    /// <summary>
    /// Represents <see cref="IReflectionVisitor{T}"/> to collect direct references.
    /// </summary>
    public class DirectReferenceCollectingVisitor : ReflectionVisitor<IEnumerable<Assembly>>
    {
        private readonly IEnumerable<Assembly> _assemblies;

        /// <summary>
        /// Initializes a new instance of the <see cref="DirectReferenceCollectingVisitor"/> class.
        /// </summary>
        public DirectReferenceCollectingVisitor()
            : this(new Assembly[0])
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DirectReferenceCollectingVisitor"/> class.
        /// </summary>
        /// <param name="assemblies">
        /// The assemblies.
        /// </param>
        protected DirectReferenceCollectingVisitor(IEnumerable<Assembly> assemblies)
        {
            _assemblies = assemblies;
        }

        /// <summary>
        /// Gets the observation or value produced by this instance.
        /// </summary>
        public override IEnumerable<Assembly> Value
        {
            get
            {
                return _assemblies;
            }
        }

        /// <summary>
        /// Allows an <see cref="TypeElement" /> to be visited. This method is
        /// called when the element accepts this visitor instance.
        /// </summary>
        /// <param name="typeElement">The <see cref="TypeElement" /> being visited.
        /// </param>
        /// <returns>
        /// A <see cref="IReflectionVisitor{T}" /> instance which can be used
        /// to continue the visiting process with potentially updated
        /// observations.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<Assembly>> Visit(TypeElement typeElement)
        {
            if (typeElement == null)
            {
                throw new ArgumentNullException("typeElement");
            }

            var type = typeElement.Type;
            var assemblies = Value
                .Concat(GetReferencedAssemblies(type))
                .Concat(base.Visit(typeElement).Value)
                .Distinct();

            return new DirectReferenceCollectingVisitor(assemblies);
        }

        /// <summary>
        /// Allows an <see cref="FieldInfoElement" /> to be visited.
        /// This method is called when the element accepts this visitor
        /// instance.
        /// </summary>
        /// <param name="fieldInfoElement">
        /// The <see cref="T:Ploeh.Albedo.FieldInfoElement" /> being visited.
        /// </param>
        /// <returns>
        /// A <see cref="IReflectionVisitor{T}" /> instance which can be used
        /// to continue the visiting process with potentially updated
        /// observations.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<Assembly>> Visit(
            FieldInfoElement fieldInfoElement)
        {
            if (fieldInfoElement == null)
            {
                throw new ArgumentNullException("fieldInfoElement");
            }

            Type fieldType = fieldInfoElement.FieldInfo.FieldType;
            var assemblies = Value
                .Concat(GetReferencedAssemblies(fieldType))
                .Distinct();

            return new DirectReferenceCollectingVisitor(assemblies);
        }

        /// <summary>
        /// Allows an <see cref="MethodInfoElement" /> to be visited.
        /// This method is called when the element accepts this visitor
        /// instance.
        /// </summary>
        /// <param name="methodInfoElement">
        /// The <see cref="MethodInfoElement" /> being visited.
        /// </param>
        /// <returns>
        /// A <see cref="IReflectionVisitor{T}" /> instance which can be used
        /// to continue the visiting process with potentially updated
        /// observations.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<Assembly>> Visit(
            MethodInfoElement methodInfoElement)
        {
            if (methodInfoElement == null)
            {
                throw new ArgumentNullException("methodInfoElement");
            }

            IEnumerable<Assembly> enumerable = base.Visit(methodInfoElement).Value;
            var assemblies = Value
                .Concat(GetReferencedAssemblies(methodInfoElement.MethodInfo.ReturnType))
                .Concat(enumerable)
                .Distinct();

            return new DirectReferenceCollectingVisitor(assemblies);
        }

        /// <summary>
        /// Allows <see cref="LocalVariableInfoElement" /> instances to be 'visited'.
        /// This method is called when the elements 'accepts' this visitor instance.
        /// </summary>
        /// <param name="localVariableInfoElements">
        /// The <see cref="LocalVariableInfoElement" /> instances being visited.
        /// </param>
        /// <returns>
        /// A (potentially) new <see cref="IReflectionVisitor{T}" /> instance which can be
        /// used to continue the visiting process with potentially updated observations.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<Assembly>> Visit(
            params LocalVariableInfoElement[] localVariableInfoElements)
        {
            return this;
        }

        /// <summary>
        /// Allows an <see cref="ParameterInfoElement" /> to be visited.
        /// This method is called when the element accepts this visitor
        /// instance.
        /// </summary>
        /// <param name="parameterInfoElement">
        /// The <see cref="ParameterInfoElement" /> being visited.
        /// </param>
        /// <returns>
        /// A <see cref="IReflectionVisitor{T}" /> instance which can be used
        /// to continue the visiting process with potentially updated
        /// observations.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<Assembly>> Visit(
            ParameterInfoElement parameterInfoElement)
        {
            if (parameterInfoElement == null)
            {
                throw new ArgumentNullException("parameterInfoElement");
            }

            Type parameterType = parameterInfoElement.ParameterInfo.ParameterType;
            var assemblies = Value
                .Concat(GetReferencedAssemblies(parameterType))
                .Distinct();

            return new DirectReferenceCollectingVisitor(assemblies);
        }

        private static IEnumerable<Assembly> GetReferencedAssemblies(Type type)
        {
            return new[] { type }
                .Concat(type.GetGenericArguments())
                .SelectMany(GetReferenceAssembliesFromAncestors);
        }

        private static IEnumerable<Assembly> GetReferenceAssembliesFromAncestors(Type type)
        {
            return ReferencedAssembliesFromBaseTypes(type)
                .Concat(GetReferencedAssembliesFromInterfaces(type));
        }

        private static IEnumerable<Assembly> ReferencedAssembliesFromBaseTypes(Type type)
        {
            var assemblies = new[] { type.Assembly };
            return type.BaseType == null
                ? assemblies
                : assemblies.Concat(ReferencedAssembliesFromBaseTypes(type.BaseType));
        }

        private static IEnumerable<Assembly> GetReferencedAssembliesFromInterfaces(Type type)
        {
            return type.GetInterfaces().Select(i => i.Assembly);
        }
    }
}
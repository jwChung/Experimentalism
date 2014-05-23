using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ploeh.Albedo;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    ///     Represents <see cref="IReflectionVisitor{T}" /> to collect reference assemblies for
    ///     a given reflection meta-data on member level.
    /// </summary>
    public class MemberReferenceCollector : ReflectionVisitor<IEnumerable<Assembly>>
    {
        private readonly IEnumerable<Assembly> _references;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MemberReferenceCollector" /> class.
        /// </summary>
        public MemberReferenceCollector() : this(new Assembly[0])
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="MemberReferenceCollector" /> class.
        /// </summary>
        /// <param name="references">
        ///     The references.
        /// </param>
        public MemberReferenceCollector(IEnumerable<Assembly> references)
        {
            if (references == null)
                throw new ArgumentNullException("references");

            _references = references;
        }

        /// <summary>
        ///     Gets a value indicating references collected for a given reflection meta-data on
        ///     member level.
        /// </summary>
        public override IEnumerable<Assembly> Value
        {
            get
            {
                return _references.Distinct();
            }
        }

        /// <summary>
        ///     Collects references of a specified type element.
        /// </summary>
        /// <param name="typeElement">
        ///     The type element.
        /// </param>
        /// <returns>
        ///     The result visitor which collected assemblies.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<Assembly>> Visit(TypeElement typeElement)
        {
            if (typeElement == null)
                throw new ArgumentNullException("typeElement");

            var references = Value.Concat(GetReferencedAssemblies(typeElement.Type));
            return new MemberReferenceCollector(references);
        }

        /// <summary>
        ///     Collects references of a specified field element.
        /// </summary>
        /// <param name="fieldInfoElement">
        ///     The field element.
        /// </param>
        /// <returns>
        ///     The result visitor which collected assemblies.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<Assembly>> Visit(FieldInfoElement fieldInfoElement)
        {
            if (fieldInfoElement == null)
                throw new ArgumentNullException("fieldInfoElement");

            var references = Value.Concat(GetReferencedAssemblies(fieldInfoElement.FieldInfo.FieldType));
            return new MemberReferenceCollector(references);
        }

        /// <summary>
        ///     Collects references of a specified method element.
        /// </summary>
        /// <param name="methodInfoElement">
        ///     The metod element.
        /// </param>
        /// <returns>
        ///     The result visitor which collected assemblies.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<Assembly>> Visit(MethodInfoElement methodInfoElement)
        {
            if (methodInfoElement == null)
                throw new ArgumentNullException("methodInfoElement");

            var references = Value
                .Concat(base.Visit(methodInfoElement).Value)
                .Concat(GetReferencedAssemblies(methodInfoElement.MethodInfo.ReturnType));

            return new MemberReferenceCollector(references);
        }

        /// <summary>
        ///     Ignores refernces of local variable elements.
        /// </summary>
        /// <param name="localVariableInfoElements">
        ///     The local variable elements.
        /// </param>
        /// <returns>
        ///     This instance
        /// </returns>
        public override IReflectionVisitor<IEnumerable<Assembly>> Visit(
            params LocalVariableInfoElement[] localVariableInfoElements)
        {
            return this;
        }

        /// <summary>
        ///     Collects references of a specified parameter element.
        /// </summary>
        /// <param name="parameterInfoElement">
        ///     The parameter element.
        /// </param>
        /// <returns>
        ///     The result visitor which collected assemblies.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<Assembly>> Visit(
            ParameterInfoElement parameterInfoElement)
        {
            if (parameterInfoElement == null)
                throw new ArgumentNullException("parameterInfoElement");

            var references = Value.Concat(GetReferencedAssemblies(parameterInfoElement.ParameterInfo.ParameterType));
            return new MemberReferenceCollector(references);
        }

        private static IEnumerable<Assembly> GetReferencedAssemblies(Type type)
        {
            return new[] { type }
                .Concat(type.GetGenericArguments())
                .SelectMany(GetReferencedAssembliesFromAncestors);
        }

        private static IEnumerable<Assembly> GetReferencedAssembliesFromAncestors(Type type)
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
using System;
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
        private readonly HashSet<Assembly> _assemblies = new HashSet<Assembly>();
        private readonly HashSet<Type> _types = new HashSet<Type>();
        
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

            AddReferencedAssemblies(typeElement.Type);
            base.Visit(typeElement);
            return this;
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
        public override IReflectionVisitor<IEnumerable<Assembly>> Visit(
            params FieldInfoElement[] fieldInfoElements)
        {
            if (fieldInfoElements == null)
            {
                throw new ArgumentNullException("fieldInfoElements");
            }
            
            foreach (var fieldInfoElement in fieldInfoElements)
            {
                var fieldInfo = fieldInfoElement.FieldInfo;
                if (fieldInfo.ReflectedType == fieldInfo.DeclaringType)
                {
                    Visit(fieldInfoElement);
                }
            }

            return this;
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
        public override IReflectionVisitor<IEnumerable<Assembly>> Visit(
            params PropertyInfoElement[] propertyInfoElements)
        {
            if (propertyInfoElements == null)
            {
                throw new ArgumentNullException("propertyInfoElements");
            }

            foreach (var propertyInfoElement in propertyInfoElements)
            {
                var propertyInfo = propertyInfoElement.PropertyInfo;
                if (propertyInfo.ReflectedType == propertyInfo.DeclaringType)
                {
                    Visit(propertyInfoElement);
                }
            }

            return this;
        }

        /// <summary>
        /// Allows an <see cref="FieldInfoElement" /> to be visited.
        /// This method is called when the element accepts this visitor
        /// instance.
        /// </summary>
        /// <param name="fieldInfoElement">
        /// The <see cref="FieldInfoElement" /> being visited.
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

            AddReferencedAssemblies(fieldInfoElement.FieldInfo.FieldType);
            return this;
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

            AddReferencedAssemblies(methodInfoElement.MethodInfo.ReturnType);
            base.Visit(methodInfoElement);
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

            AddReferencedAssemblies(parameterInfoElement.ParameterInfo.ParameterType);
            return this;
        }

        /// <summary>
        /// Allows an <see cref="LocalVariableInfoElement" /> to be visited.
        /// This method is called when the element accepts this visitor
        /// instance.
        /// </summary>
        /// <param name="localVariableInfoElement">
        /// The <see cref="LocalVariableInfoElement" /> being visited.
        /// </param>
        /// <returns>
        /// A <see cref="IReflectionVisitor{T}" /> instance which can be used
        /// to continue the visiting process with potentially updated
        /// observations.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<Assembly>> Visit(
            LocalVariableInfoElement localVariableInfoElement)
        {
            if (localVariableInfoElement == null)
            {
                throw new ArgumentNullException("localVariableInfoElement");
            }

            AddReferencedAssemblies(localVariableInfoElement.LocalVariableInfo.LocalType);
            return this;
        }

        private void AddReferencedAssemblies(Type type)
        {
            if (_types.Contains(type))
            {
                return;
            }

            foreach (var assembly in GetReferencedAssemblies(type))
            {
                if (!_assemblies.Contains(assembly))
                {
                    _assemblies.Add(assembly);    
                }
            }
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
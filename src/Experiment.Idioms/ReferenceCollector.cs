using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Mono.Reflection;
using Ploeh.Albedo;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents <see cref="IReflectionVisitor{T}"/> to collect reference
    /// assemblies for a given reflection meta-data.
    /// </summary>
    public class ReferenceCollector : ReflectionVisitor<IEnumerable<Assembly>>
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
            return base.Visit(typeElement);
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
            
            var elements = fieldInfoElements
                .Where(e => e.FieldInfo.ReflectedType == e.FieldInfo.DeclaringType)
                .ToArray();

            return base.Visit(elements);
        }

        /// <summary>
        /// Allows an <see cref="ConstructorInfoElement"/> to be visited. 
        /// This method is called when the element accepts this visitor
        /// instance.
        /// </summary>
        /// <param name="constructorInfoElement">
        /// The <see cref="ConstructorInfoElement"/> being visited.
        /// </param>
        /// <returns>
        /// A <see cref="IReflectionVisitor{T}" /> instance which can be used
        /// to continue the visiting process with potentially updated
        /// observations.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<Assembly>> Visit(
            ConstructorInfoElement constructorInfoElement)
        {
            if (constructorInfoElement == null)
            {
                throw new ArgumentNullException("constructorInfoElement");
            }

            VisitMethodBody(constructorInfoElement.ConstructorInfo);
            return base.Visit(constructorInfoElement);
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

            var elements = propertyInfoElements
                .Where(e => e.PropertyInfo.ReflectedType == e.PropertyInfo.DeclaringType)
                .ToArray();

            return base.Visit(elements);
        }

        /// <summary>
        /// Allows <see cref="MethodInfoElement"/> instances to be 'visited'.
        /// This method is called when the elements 'accepts' this visitor instance.
        /// </summary>
        /// <param name="methodInfoElements">
        /// The <see cref="MethodInfoElement"/> instances being visited.
        /// </param>
        /// <returns>
        /// A (potentially) new <see cref="IReflectionVisitor{T}"/> instance which can be
        /// used to continue the visiting process with potentially updated observations.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<Assembly>> Visit(
            params MethodInfoElement[] methodInfoElements)
        {
            if (methodInfoElements == null)
            {
                throw new ArgumentNullException("methodInfoElements");
            }

            var elements = methodInfoElements
                .Where(e => e.MethodInfo.ReflectedType == e.MethodInfo.DeclaringType)
                .ToArray();

            return base.Visit(elements);
        }

        /// <summary>
        /// Allows <see cref="EventInfoElement" /> instances to be 'visited'.
        /// This method is called when the elements 'accepts' this visitor instance.
        /// </summary>
        /// <param name="eventInfoElements">
        /// The <see cref="EventInfoElement" /> instances being visited.
        /// </param>
        /// <returns>
        /// A (potentially) new <see cref="IReflectionVisitor{T}" /> instance which can be
        /// used to continue the visiting process with potentially updated observations.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<Assembly>> Visit(
            params EventInfoElement[] eventInfoElements)
        {
            if (eventInfoElements == null)
            {
                throw new ArgumentNullException("eventInfoElements");
            }

            var elements = eventInfoElements
                .Where(e => e.EventInfo.ReflectedType == e.EventInfo.DeclaringType)
                .ToArray();

            return base.Visit(elements);
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

            MethodInfo methodInfo = methodInfoElement.MethodInfo;
            AddReferencedAssemblies(methodInfo.ReturnType);
            
            VisitMethodBody(methodInfo);
            return base.Visit(methodInfoElement);
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

        /// <summary>
        /// Visits the method body of a method-base.
        /// </summary>
        /// <param name="methodBase">The method-base.</param>
        protected virtual void VisitMethodBody(MethodBase methodBase)
        {
            if (methodBase == null)
                throw new ArgumentNullException("methodBase");

            if (methodBase.GetMethodBody() == null)
                return;

            var methodBases = methodBase.GetInstructions()
                .Select(i => i.Operand).OfType<MethodBase>();

            foreach (var methodBaseInMethodBody in methodBases)
            {
                AddReferencedAssemblies(methodBaseInMethodBody.ReflectedType);
                var method = methodBaseInMethodBody as MethodInfo;

                if (method != null)
                    AddReferencedAssemblies(method.ReturnType);

                foreach (var parameter in methodBaseInMethodBody.GetParameters())
                    AddReferencedAssemblies(parameter.ParameterType);
            }
        }
        
        private void AddReferencedAssemblies(Type type)
        {
            lock (_types)
            {
                if (_types.Contains(type))
                    return;
                _types.Add(type);
            }

            lock (_assemblies)
            {
                foreach (var assembly in GetReferencedAssemblies(type))
                    _assemblies.Add(assembly);
            }
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
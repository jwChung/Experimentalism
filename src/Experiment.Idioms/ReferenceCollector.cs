using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Mono.Reflection;
using Ploeh.Albedo;

namespace Jwc.Experiment
{
    /// <summary>
    /// Represents <see cref="IReflectionVisitor{T}" /> to collect reference assemblies for a given
    /// reflection meta-data.
    /// </summary>
    public class ReferenceCollector : ReflectionVisitor<IEnumerable<Assembly>>
    {
        private readonly HashSet<Assembly> assemblies = new HashSet<Assembly>();
        private readonly HashSet<Type> types = new HashSet<Type>();
        private readonly MemberReferenceCollector memberReferenceCollector = new MemberReferenceCollector();

        /// <summary>
        /// Gets the observation or value produced by this instance.
        /// </summary>
        public override IEnumerable<Assembly> Value
        {
            get
            {
                foreach (var assembly in this.assemblies)
                    yield return assembly;
            }
        }

        /// <summary>
        /// Visits an assembly element to collect reference assemblies to attributes.
        /// </summary>
        /// <param name="assemblyElement">
        /// The assembly element.
        /// </param>
        /// <returns>
        /// The result visitor collecting reference assemblies.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<Assembly>> Visit(
            AssemblyElement assemblyElement)
        {
            if (assemblyElement == null)
                throw new ArgumentNullException("assemblyElement");

            this.AddReferencesToAttributes(assemblyElement.Assembly);
            return base.Visit(assemblyElement);
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
        public override IReflectionVisitor<IEnumerable<Assembly>> Visit(
            TypeElement typeElement)
        {
            if (typeElement == null)
                throw new ArgumentNullException("typeElement");

            this.AddReferencesToType(typeElement.Type);
            this.AddReferencesToAttributes(typeElement.Type);
            return base.Visit(typeElement);
        }

        /// <summary>
        /// Allows <see cref="FieldInfoElement" /> instances to be 'visited'. This method is called
        /// when the elements 'accepts' this visitor instance.
        /// </summary>
        /// <param name="fieldInfoElements">
        /// The <see cref="FieldInfoElement" /> instances being visited.
        /// </param>
        /// <returns>
        /// A (potentially) new <see cref="IReflectionVisitor{T}" /> instance which can be used to
        /// continue the visiting process with potentially updated observations.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<Assembly>> Visit(
            params FieldInfoElement[] fieldInfoElements)
        {
            if (fieldInfoElements == null)
                throw new ArgumentNullException("fieldInfoElements");

            var elements = fieldInfoElements
                .Where(e => e.FieldInfo.ReflectedType == e.FieldInfo.DeclaringType)
                .ToArray();
            return base.Visit(elements);
        }

        /// <summary>
        /// Allows <see cref="PropertyInfoElement" /> instances to be 'visited'. This method is
        /// called when the elements 'accepts' this visitor instance.
        /// </summary>
        /// <param name="propertyInfoElements">
        /// The <see cref="PropertyInfoElement" /> instances being visited.
        /// </param>
        /// <returns>
        /// A (potentially) new <see cref="IReflectionVisitor{T}" /> instance which can be used to
        /// continue the visiting process with potentially updated observations.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<Assembly>> Visit(
            params PropertyInfoElement[] propertyInfoElements)
        {
            if (propertyInfoElements == null)
                throw new ArgumentNullException("propertyInfoElements");

            var elements = propertyInfoElements
                .Where(e => e.PropertyInfo.ReflectedType == e.PropertyInfo.DeclaringType)
                .ToArray();
            return base.Visit(elements);
        }

        /// <summary>
        /// Allows <see cref="MethodInfoElement" /> instances to be 'visited'. This method is
        /// called when the elements 'accepts' this visitor instance.
        /// </summary>
        /// <param name="methodInfoElements">
        /// The <see cref="MethodInfoElement" /> instances being visited.
        /// </param>
        /// <returns>
        /// A (potentially) new <see cref="IReflectionVisitor{T}" /> instance which can be used to
        /// continue the visiting process with potentially updated observations.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<Assembly>> Visit(
            params MethodInfoElement[] methodInfoElements)
        {
            if (methodInfoElements == null)
                throw new ArgumentNullException("methodInfoElements");

            var elements = methodInfoElements
                .Where(e => e.MethodInfo.ReflectedType == e.MethodInfo.DeclaringType)
                .ToArray();
            return base.Visit(elements);
        }

        /// <summary>
        /// Allows <see cref="EventInfoElement" /> instances to be 'visited'. This method is
        /// called when the elements 'accepts' this visitor instance.
        /// </summary>
        /// <param name="eventInfoElements">
        /// The <see cref="EventInfoElement" /> instances being visited.
        /// </param>
        /// <returns>
        /// A (potentially) new <see cref="IReflectionVisitor{T}" /> instance which can be used to
        /// continue the visiting process with potentially updated observations.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<Assembly>> Visit(
            params EventInfoElement[] eventInfoElements)
        {
            if (eventInfoElements == null)
                throw new ArgumentNullException("eventInfoElements");

            var elements = eventInfoElements
                .Where(e => e.EventInfo.ReflectedType == e.EventInfo.DeclaringType)
                .ToArray();
            return base.Visit(elements);
        }

        /// <summary>
        /// Allows an <see cref="FieldInfoElement" /> to be visited. This method is called when
        /// the element accepts this visitor instance.
        /// </summary>
        /// <param name="fieldInfoElement">
        /// The <see cref="FieldInfoElement" /> being visited.
        /// </param>
        /// <returns>
        /// A <see cref="IReflectionVisitor{T}" /> instance which can be used to continue the
        /// visiting process with potentially updated observations.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<Assembly>> Visit(
            FieldInfoElement fieldInfoElement)
        {
            if (fieldInfoElement == null)
                throw new ArgumentNullException("fieldInfoElement");

            this.AddReferencesToType(fieldInfoElement.FieldInfo.FieldType);
            this.AddReferencesToAttributes(fieldInfoElement.FieldInfo);
            return this;
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
        public override IReflectionVisitor<IEnumerable<Assembly>> Visit(
            ConstructorInfoElement constructorInfoElement)
        {
            if (constructorInfoElement == null)
                throw new ArgumentNullException("constructorInfoElement");

            this.VisitMethodBody(constructorInfoElement.ConstructorInfo);
            this.AddReferencesToAttributes(constructorInfoElement.ConstructorInfo);

            return base.Visit(constructorInfoElement);
        }

        /// <summary>
        /// Visits a property element to collect reference assemblies to attributes.
        /// </summary>
        /// <param name="propertyInfoElement">
        /// The property element.
        /// </param>
        /// <returns>
        /// The result visitor collecting reference assemblies.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<Assembly>> Visit(
            PropertyInfoElement propertyInfoElement)
        {
            if (propertyInfoElement == null)
                throw new ArgumentNullException("propertyInfoElement");

            this.AddReferencesToAttributes(propertyInfoElement.PropertyInfo);
            return base.Visit(propertyInfoElement);
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
        public override IReflectionVisitor<IEnumerable<Assembly>> Visit(
            MethodInfoElement methodInfoElement)
        {
            if (methodInfoElement == null)
                throw new ArgumentNullException("methodInfoElement");

            MethodInfo methodInfo = methodInfoElement.MethodInfo;
            this.AddReferencesToType(methodInfo.ReturnType);
            this.AddReferencesToAttributes(methodInfo);
            this.VisitMethodBody(methodInfo);
            return base.Visit(methodInfoElement);
        }

        /// <summary>
        /// Visits an event element to collect reference assemblies to attributes.
        /// </summary>
        /// <param name="eventInfoElement">
        /// The event element.
        /// </param>
        /// <returns>
        /// The result visitor collecting reference assemblies.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<Assembly>> Visit(
            EventInfoElement eventInfoElement)
        {
            if (eventInfoElement == null)
                throw new ArgumentNullException("eventInfoElement");

            this.AddReferencesToAttributes(eventInfoElement.EventInfo);
            return base.Visit(eventInfoElement);
        }

        /// <summary>
        /// Allows an <see cref="ParameterInfoElement" /> to be visited. This method is called
        /// when the element accepts this visitor instance.
        /// </summary>
        /// <param name="parameterInfoElement">
        /// The <see cref="ParameterInfoElement" /> being visited.
        /// </param>
        /// <returns>
        /// A <see cref="IReflectionVisitor{T}" /> instance which can be used to continue the
        /// visiting process with potentially updated observations.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<Assembly>> Visit(
            ParameterInfoElement parameterInfoElement)
        {
            if (parameterInfoElement == null)
                throw new ArgumentNullException("parameterInfoElement");

            this.AddReferencesToType(parameterInfoElement.ParameterInfo.ParameterType);
            this.AddReferencesToAttributes(parameterInfoElement.ParameterInfo);
            return this;
        }

        /// <summary>
        /// Allows an <see cref="LocalVariableInfoElement" /> to be visited. This method is
        /// called when the element accepts this visitor instance.
        /// </summary>
        /// <param name="localVariableInfoElement">
        /// The <see cref="LocalVariableInfoElement" /> being visited.
        /// </param>
        /// <returns>
        /// A <see cref="IReflectionVisitor{T}" /> instance which can be used to continue the
        /// visiting process with potentially updated observations.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<Assembly>> Visit(
            LocalVariableInfoElement localVariableInfoElement)
        {
            if (localVariableInfoElement == null)
                throw new ArgumentNullException("localVariableInfoElement");

            this.AddReferencesToType(localVariableInfoElement.LocalVariableInfo.LocalType);
            return this;
        }

        private void VisitMethodBody(MethodBase method)
        {
            if (method.GetMethodBody() == null)
                return;
            
            var methodBases = method.GetInstructions()
                .Select(i => i.Operand).OfType<MethodBase>();

            foreach (var methodBase in methodBases)
                this.VisitMethodInMethodBody(methodBase);
        }

        private void VisitMethodInMethodBody(MethodBase methodBase)
        {
            this.AddReferencesToType(methodBase.ReflectedType);
            var method = methodBase as MethodInfo;

            if (method != null)
                this.AddReferencesToType(method.ReturnType);

            foreach (var parameter in methodBase.GetParameters())
                this.AddReferencesToType(parameter.ParameterType);
        }

        private void AddReferencesToAttributes(ICustomAttributeProvider attributeProvider)
        {
            foreach (var attribute in attributeProvider.GetCustomAttributes(false))
                this.AddReferencesToType(attribute.GetType());
        }

        private void AddReferencesToType(Type type)
        {
            if (this.types.Contains(type))
                return;

            var assemblies = type.ToElement().Accept(this.memberReferenceCollector).Value;
            foreach (var assembly in assemblies)
                this.assemblies.Add(assembly);

            this.types.Add(type);
        }
    }
}
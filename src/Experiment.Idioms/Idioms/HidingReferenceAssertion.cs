using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ploeh.Albedo;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents assertions to verify that specified assemblies are not
    /// directly referenced.
    /// </summary>
    public class HidingReferenceAssertion : ReflectionVisitor<object>
    {
        private readonly AccessibilityCollectingVisitor _accessibilityCollectingVisitor
            = new AccessibilityCollectingVisitor();
        private readonly ElementReferenceCollectingVisitor _elementReferenceCollectingVisitor
            = new ElementReferenceCollectingVisitor();
        private readonly Assembly[] _assemblies;

        /// <summary>
        /// Initializes a new instance of the <see cref="HidingReferenceAssertion"/> class.
        /// </summary>
        /// <param name="assemblies">
        /// The assemblies which are not referenced from elements.
        /// </param>
        public HidingReferenceAssertion(params Assembly[] assemblies)
        {
            if (assemblies == null)
            {
                throw new ArgumentNullException("assemblies");
            }

            _assemblies = assemblies;
        }

        /// <summary>
        /// Gets the observation or value produced by this instance.
        /// </summary>
        public override object Value
        {
            get
            {
                throw new System.NotImplementedException();
            }
        }

        /// <summary>
        /// Gets a value indicating the assemblies which are not referenced from
        /// elements.
        /// </summary>
        public IEnumerable<Assembly> Assemblies
        {
            get
            {
                return _assemblies;
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
            return base.Visit(GetVisibleReflectionElements(typeElements));
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
            return base.Visit(GetVisibleReflectionElements(fieldInfoElements));
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
            return base.Visit(GetVisibleReflectionElements(constructorInfoElements));
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
            return base.Visit(GetVisibleReflectionElements(propertyInfoElements));
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
        public override IReflectionVisitor<object> Visit(
            params MethodInfoElement[] methodInfoElements)
        {
            return base.Visit(GetVisibleReflectionElements(methodInfoElements));
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
        public override IReflectionVisitor<object> Visit(
            params EventInfoElement[] eventInfoElements)
        {
            return base.Visit(GetVisibleReflectionElements(eventInfoElements));
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
        public override IReflectionVisitor<object> Visit(
            params LocalVariableInfoElement[] localVariableInfoElements)
        {
            return base.Visit(new LocalVariableInfoElement[0]);
        }

        /// <summary>
        /// Allows an <see cref="TypeElement" /> to be visited. This method is
        /// called when the element accepts this visitor instance.
        /// </summary>
        /// <param name="typeElement">
        /// The <see cref="TypeElement" /> being visited.
        /// </param>
        /// <returns>
        /// A <see cref="IReflectionVisitor{T}" /> instance which can be used
        /// to continue the visiting process with potentially updated
        /// observations.
        /// </returns>
        public override IReflectionVisitor<object> Visit(TypeElement typeElement)
        {
            EnsureReferencesAreNotSpecified(typeElement);
            return base.Visit(typeElement);
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
            EnsureReferencesAreNotSpecified(fieldInfoElement);
            return base.Visit(fieldInfoElement);
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
            EnsureReferencesAreNotSpecified(constructorInfoElement);
            return base.Visit(constructorInfoElement);
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
        public override IReflectionVisitor<object> Visit(MethodInfoElement methodInfoElement)
        {
            EnsureReferencesAreNotSpecified(methodInfoElement);
            return base.Visit(methodInfoElement);
        }

        private void EnsureReferencesAreNotSpecified(IReflectionElement reflectionElement)
        {
            var asemblies = reflectionElement.Accept(_elementReferenceCollectingVisitor).Value;
            if (!Assemblies.Any(asemblies.Contains))
                return;

            throw new HidingReferenceException();
        }

        private T[] GetVisibleReflectionElements<T>(IEnumerable<T> typeElements)
            where T : IReflectionElement
        {
            return typeElements.Where(IsVisible).ToArray();
        }

        private bool IsVisible<T>(T e) where T : IReflectionElement
        {
            var accessibilities = e.Accept(_accessibilityCollectingVisitor).Value.Single();
            return (accessibilities & (Accessibilities.Public | Accessibilities.Protected)) != Accessibilities.None;
        }
    }
}
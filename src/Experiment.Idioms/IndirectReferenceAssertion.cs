using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ploeh.Albedo;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Encapsulates a unit test that verifies that certain assemblies are not
    /// exposed through API.
    /// </summary>
    public class IndirectReferenceAssertion
        : IdiomaticMemberAssertion, IIdiomaticTypeAssertion, IIdiomaticAssemblyAssertion
    {
        private readonly AccessibilityCollector _accessibilityCollector = new AccessibilityCollector();
        private readonly Assembly[] _indirectReferences;

        /// <summary>
        /// Initializes a new instance of the <see cref="IndirectReferenceAssertion"/> class.
        /// </summary>
        /// <param name="indirectReferences">
        /// The indirect references which should not be exposed though API.
        /// </param>
        public IndirectReferenceAssertion(params Assembly[] indirectReferences)
        {
            _indirectReferences = indirectReferences;
        }

        /// <summary>
        /// Gets a value indicating the indirect references.
        /// </summary>
        public IEnumerable<Assembly> IndirectReferences
        {
            get
            {
                return _indirectReferences;
            }
        }

        /// <summary>
        /// Verifies that an assembly does not expose the indirect references
        /// through API.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        public void Verify(Assembly assembly)
        {
            var types = assembly.GetTypes().Where(IsExposed);

            foreach (var type in types)
                Verify(type);
        }

        /// <summary>
        /// Verifies that a type does not expose the indirect references
        /// through API.
        /// </summary>
        /// <param name="type">The type.</param>
        public virtual void Verify(Type type)
        {
            var members = new TypeMembers(type, MemberKinds.All, Accessibilities.Public | Accessibilities.Protected);

            foreach (var member in members)
                Verify(member);
        }

        /// <summary>
        /// Verifies that a field does not expose the indirect references
        /// through API.
        /// </summary>
        /// <param name="field">The field.</param>
        public override void Verify(FieldInfo field)
        {
            EnsureNotExpose(field.ToElement());
        }

        /// <summary>
        /// Verifies that a constructor does not expose the indirect references
        /// through API.
        /// </summary>
        /// <param name="constructor">The constructor.</param>
        public override void Verify(ConstructorInfo constructor)
        {
            EnsureNotExpose(constructor.ToElement());
        }

        /// <summary>
        /// Verifies that a property does not expose the indirect references
        /// through API.
        /// </summary>
        /// <param name="property">The field.</param>
        public override void Verify(PropertyInfo property)
        {
            EnsureNotExpose(property.ToElement());
        }

        /// <summary>
        /// Verifies that a method does not expose the indirect references
        /// through API.
        /// </summary>
        /// <param name="method">The method.</param>
        public override void Verify(MethodInfo method)
        {
            EnsureNotExpose(method.ToElement());
        }

        /// <summary>
        /// Verifies that an event does not expose the indirect references
        /// through API.
        /// </summary>
        /// <param name="event">The event.</param>
        public override void Verify(EventInfo @event)
        {
            EnsureNotExpose(@event.ToElement());
        }

        private void EnsureNotExpose(IReflectionElement reflectionElement)
        {
            var reference = GetReferences(reflectionElement).FirstOrDefault(r => IndirectReferences.Contains(r));

            if (reference == null)
                return;

            throw new IndirectReferenceException(
                string.Format(
                "The indirect reference should not be exposed through the API.{0}" +
                "Indirect reference: {1}{0}" + 
                "API(exposing)     : {2}{0}",
                Environment.NewLine,
                reference,
                reflectionElement.Accept(new DisplayNameVisitor()).Value.Single()));
        }

        private bool IsExposed(Type type)
        {
            return (GetAccessibilities(type) & (Accessibilities.Public | Accessibilities.Protected))
                != Accessibilities.None;
        }

        private static IEnumerable<Assembly> GetReferences(IReflectionElement reflectionElement)
        {
            return reflectionElement.Accept(new ElementReferenceCollector()).Value;
        }

        private Accessibilities GetAccessibilities(Type type)
        {
            return type.ToElement().Accept(_accessibilityCollector).Value.Single();
        }

        private class ElementReferenceCollector : ReferenceCollector
        {
            protected override void VisitMethodBody(MethodBase methodBase)
            {
            }
        }
    }
}
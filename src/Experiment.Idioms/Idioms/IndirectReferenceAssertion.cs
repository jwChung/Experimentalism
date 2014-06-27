using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Ploeh.Albedo;
using Ploeh.Albedo.Refraction;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Encapsulates a unit test that verifies that certain assemblies are not exposed through API.
    /// </summary>
    public class IndirectReferenceAssertion
        : IdiomaticMemberAssertion, IIdiomaticAssemblyAssertion, IIdiomaticTypeAssertion
    {
        private readonly MemberReferenceCollector _memberReferenceCollector = new MemberReferenceCollector();
        private readonly AccessibilityCollector _accessibilityCollector = new AccessibilityCollector();
        private readonly Assembly[] _indirectReferences;

        /// <summary>
        /// Initializes a new instance of the <see cref="IndirectReferenceAssertion" /> class.
        /// </summary>
        /// <param name="indirectReferences">
        /// The indirect references which should not be exposed though API.
        /// </param>
        public IndirectReferenceAssertion(params Assembly[] indirectReferences)
        {
            if (indirectReferences == null)
                throw new ArgumentNullException("indirectReferences");

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
        /// Verifies that an assembly does not expose the indirect references through API.
        /// </summary>
        /// <param name="assembly">
        /// The assembly.
        /// </param>
        public void Verify(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");

            foreach (var type in assembly.GetTypes())
                Verify(type);
        }

        /// <summary>
        /// Verifies that a type does not expose the indirect references through API.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        public virtual void Verify(Type type)
        {
            if (!IsExposed(type))
                return;

            EnsureNotExpose(type.ToElement());

            foreach (var member in type.GetIdiomaticMembers())
                Verify(member);
        }

        /// <summary>
        /// Verifies that a field does not expose the indirect references through API.
        /// </summary>
        /// <param name="field">
        /// The field.
        /// </param>
        public override void Verify(FieldInfo field)
        {
            if (!IsExposed(field))
                return;

            EnsureNotExpose(field.ToElement());
        }

        /// <summary>
        /// Verifies that a constructor does not expose the indirect references through API.
        /// </summary>
        /// <param name="constructor">
        /// The constructor.
        /// </param>
        public override void Verify(ConstructorInfo constructor)
        {
            if (!IsExposed(constructor))
                return;

            EnsureNotExpose(constructor.ToElement());
        }

        /// <summary>
        /// Verifies that a property does not expose the indirect references through API.
        /// </summary>
        /// <param name="property">
        /// The field.
        /// </param>
        public override void Verify(PropertyInfo property)
        {
            if (!IsExposed(property))
                return;

            EnsureNotExpose(property.ToElement());
        }

        /// <summary>
        /// Verifies that a method does not expose the indirect references through API.
        /// </summary>
        /// <param name="method">
        /// The method.
        /// </param>
        public override void Verify(MethodInfo method)
        {
            if (!IsExposed(method))
                return;

            EnsureNotExpose(method.ToElement());
        }

        /// <summary>
        /// Verifies that an event does not expose the indirect references through API.
        /// </summary>
        /// <param name="event">
        /// The event.
        /// </param>
        public override void Verify(EventInfo @event)
        {
            if (!IsExposed(@event))
                return;

            EnsureNotExpose(@event.ToElement());
        }

        private void EnsureNotExpose(IReflectionElement reflectionElement)
        {
            var reference = GetReferences(reflectionElement)
                .FirstOrDefault(r => IndirectReferences.Contains(r));

            if (reference == null)
                return;

            var messageFormat = @"The indirect reference should not be exposed through API.
Indirect reference: {0}
API(exposing)     : {1}";

            throw new IndirectReferenceException(
                string.Format(
                    CultureInfo.CurrentCulture,
                    messageFormat,
                    reference,
                    reflectionElement.Accept(new DisplayNameCollector()).Value.Single()));
        }

        private bool IsExposed(MemberInfo member)
        {
            return (GetAccessibilities(member) & Accessibilities.Exposed) != Accessibilities.None;
        }

        private IEnumerable<Assembly> GetReferences(IReflectionElement reflectionElement)
        {
            return reflectionElement.Accept(_memberReferenceCollector).Value;
        }

        private Accessibilities GetAccessibilities(MemberInfo member)
        {
            return member.ToReflectionElement().Accept(_accessibilityCollector).Value.Single();
        }
    }
}
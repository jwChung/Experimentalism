namespace Jwc.Experiment.Idioms
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using Ploeh.Albedo;
    using Ploeh.Albedo.Refraction;

    /// <summary>
    /// Encapsulates a unit test that verifies that certain assemblies are not exposed through public API.
    /// </summary>
    public class IndirectReferenceAssertion : IdiomaticAssertion
    {
        private readonly MemberReferenceCollector memberReferenceCollector = new MemberReferenceCollector();
        private readonly AccessibilityCollector accessibilityCollector = new AccessibilityCollector();
        private readonly Assembly[] indirectReferences;

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

            this.indirectReferences = indirectReferences;
        }

        /// <summary>
        /// Gets a value indicating the indirect references.
        /// </summary>
        public IEnumerable<Assembly> IndirectReferences
        {
            get
            {
                return this.indirectReferences;
            }
        }

        /// <summary>
        /// Verifies that an assembly does not expose the indirect references through public APIs.
        /// </summary>
        /// <param name="assembly">
        /// The assembly.
        /// </param>
        public override void Verify(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException("assembly");

            foreach (var type in assembly.GetTypes())
                this.Verify(type);
        }

        /// <summary>
        /// Verifies that a type does not expose the indirect references through public APIs.
        /// </summary>
        /// <param name="type">
        /// A type to be verified.
        /// </param>
        public override void Verify(Type type)
        {
            if (!this.IsExposed(type))
                return;

            this.EnsureDostNotExposeIndirectReferences(type.ToElement());

            base.Verify(type);
        }

        /// <summary>
        /// Verifies that a field does not expose the indirect references through public APIs.
        /// </summary>
        /// <param name="field">
        /// The field.
        /// </param>
        public override void Verify(FieldInfo field)
        {
            if (!this.IsExposed(field))
                return;

            this.EnsureDostNotExposeIndirectReferences(field.ToElement());
        }

        /// <summary>
        /// Verifies that a constructor does not expose the indirect references through public APIs.
        /// </summary>
        /// <param name="constructor">
        /// The constructor.
        /// </param>
        public override void Verify(ConstructorInfo constructor)
        {
            if (!this.IsExposed(constructor))
                return;

            this.EnsureDostNotExposeIndirectReferences(constructor.ToElement());
        }

        /// <summary>
        /// Verifies that a property does not expose the indirect references through public APIs.
        /// </summary>
        /// <param name="property">
        /// The field.
        /// </param>
        public override void Verify(PropertyInfo property)
        {
            if (!this.IsExposed(property))
                return;

            this.EnsureDostNotExposeIndirectReferences(property.ToElement());
        }

        /// <summary>
        /// Verifies that a method does not expose the indirect references through public APIs.
        /// </summary>
        /// <param name="method">
        /// The method.
        /// </param>
        public override void Verify(MethodInfo method)
        {
            if (!this.IsExposed(method))
                return;

            this.EnsureDostNotExposeIndirectReferences(method.ToElement());
        }

        /// <summary>
        /// Verifies that an event does not expose the indirect references through public APIs.
        /// </summary>
        /// <param name="event">
        /// The event.
        /// </param>
        public override void Verify(EventInfo @event)
        {
            if (!this.IsExposed(@event))
                return;

            this.EnsureDostNotExposeIndirectReferences(@event.ToElement());
        }

        private void EnsureDostNotExposeIndirectReferences(IReflectionElement reflectionElement)
        {
            var reference = this.GetReferences(reflectionElement)
                .FirstOrDefault(r => this.IndirectReferences.Contains(r));

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
            return (this.GetAccessibilities(member) & Accessibilities.Exposed) != Accessibilities.None;
        }

        private IEnumerable<Assembly> GetReferences(IReflectionElement reflectionElement)
        {
            return reflectionElement.Accept(this.memberReferenceCollector).Value;
        }

        private Accessibilities GetAccessibilities(MemberInfo member)
        {
            return member.ToReflectionElement().Accept(this.accessibilityCollector).Value.Single();
        }
    }
}
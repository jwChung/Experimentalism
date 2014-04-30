using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ploeh.Albedo;

namespace Jwc.Experiment.Idioms
{
    /// <summary>
    /// Represents a class to collect <see cref="MemberKinds"/>.
    /// </summary>
    public class MemberKindCollector : ReflectionVisitor<IEnumerable<MemberKinds>>
    {
        private readonly IEnumerable<MemberKinds> _values;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberKindCollector"/> class.
        /// </summary>
        public MemberKindCollector()
            : this(new MemberKinds[0])
        {
        }

        private MemberKindCollector(IEnumerable<MemberKinds> values)
        {
            _values = values;
        }

        /// <summary>
        /// Gets a value indicating the collected <see cref="MemberKinds"/>(s).
        /// </summary>
        public override IEnumerable<MemberKinds> Value
        {
            get
            {
                return _values;
            }
        }

        /// <summary>
        /// Collects a <see cref="MemberKinds"/> value corresponding to
        /// the <paramref name="fieldInfoElement"/>.
        /// </summary>
        /// <param name="fieldInfoElement">
        /// A field element to collect <see cref="MemberKinds"/>.
        /// </param>
        /// <returns>
        /// A new instance of <see cref="MemberKindCollector"/> having collected
        /// <see cref="MemberKinds"/>.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<MemberKinds>> Visit(
            FieldInfoElement fieldInfoElement)
        {
            if (fieldInfoElement == null)
                throw new ArgumentNullException("fieldInfoElement");

            return new MemberKindCollector(Value.Concat(new[] { MemberKinds.Field }));
        }

        /// <summary>
        /// Collects a <see cref="MemberKinds"/> value corresponding to
        /// the <paramref name="constructorInfoElement"/>.
        /// </summary>
        /// <param name="constructorInfoElement">
        /// A constructor element to collect <see cref="MemberKinds"/>.
        /// </param>
        /// <returns>
        /// A new instance of <see cref="MemberKindCollector"/> having collected
        /// <see cref="MemberKinds"/>.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<MemberKinds>> Visit(
            ConstructorInfoElement constructorInfoElement)
        {
            if (constructorInfoElement == null)
                throw new ArgumentNullException("constructorInfoElement");

            return new MemberKindCollector(Value.Concat(new[] { MemberKinds.Constructor }));
        }

        /// <summary>
        /// Collects a <see cref="MemberKinds"/> value corresponding to
        /// the <paramref name="propertyInfoElement"/>.
        /// </summary>
        /// <param name="propertyInfoElement">
        /// A property element to collect <see cref="MemberKinds"/>.
        /// </param>
        /// <returns>
        /// A new instance of <see cref="MemberKindCollector"/> having collected
        /// <see cref="MemberKinds"/>.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<MemberKinds>> Visit(
            PropertyInfoElement propertyInfoElement)
        {
            if (propertyInfoElement == null)
                throw new ArgumentNullException("propertyInfoElement");

            return new MemberKindCollector(
                Value.Concat(new[] { GetPropertyType(propertyInfoElement.PropertyInfo) }));
        }

        /// <summary>
        /// Collects a <see cref="MemberKinds"/> value corresponding to
        /// the <paramref name="methodInfoElement"/>.
        /// </summary>
        /// <param name="methodInfoElement">
        /// A method element to collect <see cref="MemberKinds"/>.
        /// </param>
        /// <returns>
        /// A new instance of <see cref="MemberKindCollector"/> having collected
        /// <see cref="MemberKinds"/>.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<MemberKinds>> Visit(
            MethodInfoElement methodInfoElement)
        {
            if (methodInfoElement == null)
                throw new ArgumentNullException("methodInfoElement");

            return new MemberKindCollector(Value.Concat(new[] { MemberKinds.Method }));
        }

        /// <summary>
        /// Collects a <see cref="MemberKinds"/> value corresponding to
        /// the <paramref name="eventInfoElement"/>.
        /// </summary>
        /// <param name="eventInfoElement">
        /// An event element to collect <see cref="MemberKinds"/>.
        /// </param>
        /// <returns>
        /// A new instance of <see cref="MemberKindCollector"/> having collected
        /// <see cref="MemberKinds"/>.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<MemberKinds>> Visit(
            EventInfoElement eventInfoElement)
        {
            if (eventInfoElement == null)
                throw new ArgumentNullException("eventInfoElement");

            return new MemberKindCollector(Value.Concat(new[] { MemberKinds.Event }));
        }

        private static MemberKinds GetPropertyType(PropertyInfo property)
        {
            if (property.GetGetMethod() == null)
                return MemberKinds.SetProperty;

            if (property.GetSetMethod() == null)
                return MemberKinds.GetProperty;

            return MemberKinds.GetSetProperty;
        }
    }
}
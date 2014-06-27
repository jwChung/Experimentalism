using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Ploeh.Albedo;

namespace Jwc.Experiment
{
    /// <summary>
    /// Represents a class to collect <see cref="MemberKinds" />.
    /// </summary>
    public class MemberKindCollector : ReflectionVisitor<IEnumerable<MemberKinds>>
    {
        private readonly IEnumerable<MemberKinds> _memberKinds;

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberKindCollector" /> class.
        /// </summary>
        public MemberKindCollector()
            : this(new MemberKinds[0])
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MemberKindCollector" /> class.
        /// </summary>
        /// <param name="memberKinds">
        /// The initial member kinds.
        /// </param>
        public MemberKindCollector(IEnumerable<MemberKinds> memberKinds)
        {
            _memberKinds = memberKinds;
        }

        /// <summary>
        /// Gets a value indicating the collected <see cref="MemberKinds" />(s).
        /// </summary>
        public override IEnumerable<MemberKinds> Value
        {
            get
            {
                return _memberKinds;
            }
        }

        /// <summary>
        /// Collects a <see cref="MemberKinds" /> value corresponding to the
        /// <paramref name="fieldInfoElement" />.
        /// </summary>
        /// <param name="fieldInfoElement">
        /// A field element to collect <see cref="MemberKinds" />.
        /// </param>
        /// <returns>
        /// A new instance of <see cref="MemberKindCollector" /> having collected
        /// <see cref="MemberKinds" />.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<MemberKinds>> Visit(
            FieldInfoElement fieldInfoElement)
        {
            if (fieldInfoElement == null)
                throw new ArgumentNullException("fieldInfoElement");

            var memberKinds = fieldInfoElement.FieldInfo.IsStatic
                ? MemberKinds.StaticField
                : MemberKinds.InstanceField;

            return new MemberKindCollector(Value.Concat(new[] { memberKinds }));
        }

        /// <summary>
        /// Collects a <see cref="MemberKinds" /> value corresponding to the
        /// <paramref name="constructorInfoElement" />.
        /// </summary>
        /// <param name="constructorInfoElement">
        /// A constructor element to collect <see cref="MemberKinds" />.
        /// </param>
        /// <returns>
        /// A new instance of <see cref="MemberKindCollector" /> having collected
        /// <see cref="MemberKinds" />.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<MemberKinds>> Visit(
            ConstructorInfoElement constructorInfoElement)
        {
            if (constructorInfoElement == null)
                throw new ArgumentNullException("constructorInfoElement");

            var memberKinds = constructorInfoElement.ConstructorInfo.IsStatic
                ? MemberKinds.StaticConstructor
                : MemberKinds.InstanceConstructor;

            return new MemberKindCollector(Value.Concat(new[] { memberKinds }));
        }

        /// <summary>
        /// Collects a <see cref="MemberKinds" /> value corresponding to the
        /// <paramref name="propertyInfoElement" />.
        /// </summary>
        /// <param name="propertyInfoElement">
        /// A property element to collect <see cref="MemberKinds" />.
        /// </param>
        /// <returns>
        /// A new instance of <see cref="MemberKindCollector" /> having collected
        /// <see cref="MemberKinds" />.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<MemberKinds>> Visit(
            PropertyInfoElement propertyInfoElement)
        {
            if (propertyInfoElement == null)
                throw new ArgumentNullException("propertyInfoElement");

            return new MemberKindCollector(
                Value.Concat(new[] { GetPropertyKinds(propertyInfoElement.PropertyInfo) }));
        }

        /// <summary>
        /// Collects a <see cref="MemberKinds" /> value corresponding to the
        /// <paramref name="methodInfoElement" />.
        /// </summary>
        /// <param name="methodInfoElement">
        /// A method element to collect <see cref="MemberKinds" />.
        /// </param>
        /// <returns>
        /// A new instance of <see cref="MemberKindCollector" /> having collected
        /// <see cref="MemberKinds" />.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<MemberKinds>> Visit(
            MethodInfoElement methodInfoElement)
        {
            if (methodInfoElement == null)
                throw new ArgumentNullException("methodInfoElement");

            var memberKinds = methodInfoElement.MethodInfo.IsStatic
                ? MemberKinds.StaticMethod
                : MemberKinds.InstanceMethod;

            return new MemberKindCollector(Value.Concat(new[] { memberKinds }));
        }

        /// <summary>
        /// Collects a <see cref="MemberKinds" /> value corresponding to the
        /// <paramref name="eventInfoElement" />.
        /// </summary>
        /// <param name="eventInfoElement">
        /// An event element to collect <see cref="MemberKinds" />.
        /// </param>
        /// <returns>
        /// A new instance of <see cref="MemberKindCollector" /> having collected
        /// <see cref="MemberKinds" />.
        /// </returns>
        public override IReflectionVisitor<IEnumerable<MemberKinds>> Visit(
            EventInfoElement eventInfoElement)
        {
            if (eventInfoElement == null)
                throw new ArgumentNullException("eventInfoElement");

            var memberKinds = eventInfoElement.EventInfo.GetAddMethod(true).IsStatic
                ? MemberKinds.StaticEvent
                : MemberKinds.InstanceEvent;

            return new MemberKindCollector(Value.Concat(new[] { memberKinds }));
        }

        private static MemberKinds GetPropertyKinds(PropertyInfo property)
        {
            var memberKinds = MemberKinds.None;

            var getMethod = property.GetGetMethod(true);
            if (getMethod != null)
            {
                if (getMethod.IsStatic)
                    memberKinds |= MemberKinds.StaticGetProperty;
                else
                    memberKinds |= MemberKinds.InstanceGetProperty;
            }

            var setMethod = property.GetSetMethod(true);
            if (setMethod != null)
            {
                if (setMethod.IsStatic)
                    memberKinds |= MemberKinds.StaticSetProperty;
                else
                    memberKinds |= MemberKinds.InstanceSetProperty;
            }

            return memberKinds;
        }
    }
}